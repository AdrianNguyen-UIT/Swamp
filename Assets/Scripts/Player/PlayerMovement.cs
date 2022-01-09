using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    #region Variable Fields
    //Configuration
    [Header("Speed")]
    [SerializeField] private MovementConfig movementConfig = null;

    [Header("Stamina")]
    [SerializeField] private StaminaConfig staminaConfig = null;

    [Header("InputReader")]
    [SerializeField] private InputReader inputReader = null;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;
    [SerializeField] private Vector2 jumpOffset = Vector2.zero;
    [SerializeField] private Vector2 climbOffset = Vector2.zero;

    [Header("MenuCanvasEventChannel")]
    [SerializeField] private MenuEventChannel pauseMenu = null;

    [Header("Others")]
    [SerializeField] private LayerMask whatIsOnewayPlatform = 0;
    [SerializeField] private Vector2 onewayDetectBoxCast = Vector2.zero;
    //Cached component references
    private Animator animator = null;
    private CharacterController2D controller = null;
    private StaminaSystem stamina = null;
    private InteractSystem interactSystem = null;
    private CinemachineFramingTransposer cinemachineFramingTransposer = null;
    private Collider2D col = null;
    private Collider2D onewayCol = null;
    private Vector2 currentMovement = Vector2.zero;
    private Vector2 submitMovement = Vector2.zero;

    //State
    private bool sprint = false;
    private bool sprintJump = false;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        stamina = GetComponent<StaminaSystem>();
        interactSystem = GetComponent<InteractSystem>();
        col = GetComponent<Collider2D>();
        cinemachineFramingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void OnEnable()
    {
        inputReader.EnableGameplayInput();
        inputReader.MoveEvent += OnMove;
        inputReader.JumpEvent += OnJump;
        inputReader.SprintEvent += OnSprint;
        inputReader.PauseEvent += OnPause;
        inputReader.GrabEvent += interactSystem.OnGrab;
        inputReader.ActiveFirstAbilityEvent += AbilitySystem.Instance.OnActiveFirstAbility;
        inputReader.ActiveSecondAbilityEvent += AbilitySystem.Instance.OnActiveSecondAbility;

        inputReader.CheatEvent += OnCheat;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMove;
        inputReader.JumpEvent -= OnJump;
        inputReader.SprintEvent -= OnSprint;
        inputReader.PauseEvent -= OnPause;
        inputReader.GrabEvent -= interactSystem.OnGrab;
        inputReader.ActiveFirstAbilityEvent -= AbilitySystem.Instance.OnActiveFirstAbility;
        inputReader.ActiveSecondAbilityEvent -= AbilitySystem.Instance.OnActiveSecondAbility;

        inputReader.CheatEvent -= OnCheat;
    }

    private void FixedUpdate()
    {
        controller.Move(submitMovement * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (controller.IsLockingMovement())
            return;

        if (onewayCol)
        {
            var onewayCast = Physics2D.OverlapBox(transform.position, onewayDetectBoxCast, 0.0f, whatIsOnewayPlatform);
            if (!onewayCast)
            {
                Physics2D.IgnoreCollision(onewayCol, col, false);
                onewayCol = null;
            }
        }

        CheckStaminaConsumption();

        if (controller.IsClimbing())
            cinemachineFramingTransposer.m_TrackedObjectOffset = currentMovement.y > 0.0f ? climbOffset : Vector2.zero;
        else
            cinemachineFramingTransposer.m_TrackedObjectOffset = Vector2.zero;

        animator.SetFloat("Speed", Mathf.Abs(submitMovement.x));
    }

    private void OnDrawGizmos()
    {
        if (onewayCol)
        {
            Gizmos.color = Color.blue;

            Vector3 bottomLeft = transform.position + new Vector3(-onewayDetectBoxCast.x / 2, -onewayDetectBoxCast.y / 2);
            Vector3 bottomRight = transform.position + new Vector3(onewayDetectBoxCast.x / 2, -onewayDetectBoxCast.y / 2);
            Vector3 topLeft = transform.position + new Vector3(-onewayDetectBoxCast.x / 2, onewayDetectBoxCast.y / 2);
            Vector3 topRight = transform.position + new Vector3(onewayDetectBoxCast.x / 2, onewayDetectBoxCast.y / 2);

            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(bottomRight, topRight);
            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.DrawLine(topLeft, bottomLeft);
        }
    }
    #endregion

    #region Private Methods

    private void CheckStaminaConsumption()
    {
        submitMovement = currentMovement;

        if (controller.IsClimbing())
        {
            if (stamina.OutOfStamina())
            {
                controller.DisableClimb();
                currentMovement.y = 0.0f;
                submitMovement = currentMovement;
            }
            else if (sprint && submitMovement.y != 0.0f)
            {
                submitMovement.y *= movementConfig.SprintMultiplier;
                stamina.ConsumeStamina(staminaConfig.SprintClimbConsumeRate * Time.deltaTime);
            }
            else if (submitMovement.y > 0.0f)
            {
                stamina.ConsumeStamina(staminaConfig.ClimbConsumeRate * Time.deltaTime);
            }
        }
        else if (controller.IsGrounded())
        {
            if (interactSystem.IsGrabbing())
            {
                if (stamina.OutOfStamina())
                {
                    submitMovement.x = 0.0f;
                }
                else if (submitMovement.x != 0.0f)
                {
                    submitMovement.x *= movementConfig.PushPullMultiplier;
                    stamina.ConsumeStamina(staminaConfig.PushPullConsumeRate * Time.deltaTime);
                }
            }
            else if (sprint)
            {
                if (stamina.OutOfStamina())
                {
                    sprint = false;
                }
                else if (submitMovement.x != 0.0f)
                {
                    submitMovement.x *= movementConfig.SprintMultiplier;
                    stamina.ConsumeStamina(staminaConfig.SprintConsumeRate * Time.deltaTime);
                }
            }
        }
        else if (sprintJump)
        {
            submitMovement.x *= movementConfig.SprintMultiplier;
        }
    }
    #endregion

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            currentMovement.x = context.ReadValue<Vector2>().x * movementConfig.WalkSpeed;
            currentMovement.y = context.ReadValue<Vector2>().y * movementConfig.ClimbSpeed;
            if (stamina.OutOfStamina())
            {
                currentMovement.y = 0.0f;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !interactSystem.IsGrabbing())
        {
            onewayCol = controller.IsStandingOn(whatIsOnewayPlatform);
            if (onewayCol && currentMovement.y < 0.0f)
            {
                Physics2D.IgnoreCollision(onewayCol, col);
            }
            else if (stamina.EnoughStamina(staminaConfig.JumpConsumption))
                controller.Jump();
        }
        else if (context.canceled)
        {
            controller.CancelJump();
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sprint = true;
        }
        else if (context.canceled)
        {
            sprint = false;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseMenu.OnAppear();
            Debug.Log("Pause");
        }
    }

    public void OnCheat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.EnableCheat();
        }
    }

    public void OnLand()
    {
        animator.SetBool("IsJumping", false);
        stamina.ForceStartRecoverStamina();
        cinemachineFramingTransposer.m_TrackedObjectOffset = Vector2.zero;
        sprintJump = false;

    }

    public void OnJumpEvent()
    {
        stamina.ForceStopRecoverStamina();
        stamina.ConsumeStamina(staminaConfig.JumpConsumption);
        cinemachineFramingTransposer.m_TrackedObjectOffset = jumpOffset;
        animator.SetBool("IsJumping", true);
        if (sprint)
            sprintJump = true;
    }

    public void OnHighJumpAbilityEvent()
    {
        cinemachineFramingTransposer.m_TrackedObjectOffset = jumpOffset;
        animator.SetBool("IsJumping", true);
        if (sprint)
            sprintJump = true;
    }


    public void OnStartClimbing()
    {
        stamina.ForceStopRecoverStamina();
    }

    public Vector3 GetInputDir()
    {
        if (currentMovement.x == 0.0f)
            return Vector3.zero;
        else
            return new Vector3(currentMovement.x / Mathf.Abs(currentMovement.x), 0.0f, 0.0f);
    }

    public void SetMovementConfig(MovementConfig config)
    {
        movementConfig = config;
        controller.SetMovementConfig(config);
    }
}