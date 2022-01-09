using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
public class CharacterController2D : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private bool lockMovement = false;
    [SerializeField] private bool allowFlip = true;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private bool allowLowJump = false;

    [Header("MovementConfig")]
    [SerializeField] private MovementConfig movementConfig = null;

    [Header("Detection")]
    [SerializeField] private LayerMask m_WhatIsGround = 0;
    [SerializeField] private Transform m_GroundCheck = null;
    [SerializeField] private Vector2 groundCheckBoxCastSize = new Vector2(0.8f, 0.6f);
    [Space]
    [SerializeField] private LayerMask m_WhatIsLadder = 0;
    [SerializeField] private Transform m_LadderCheck = null;
    [SerializeField] private Vector2 ladderCheckBoxCastSize = new Vector2(0.8f, 0.6f);
    [Space]
    [SerializeField] private PhysicsMaterial2D zeroFriction = null;
    [SerializeField] private PhysicsMaterial2D fullFriction = null;

    private float slopeDownAngle = 0.0f;
    private bool m_Grounded = false;
    private Rigidbody2D m_Rigidbody2D = null;
    private Vector3 m_Velocity = Vector3.zero;
    private bool isJumpReady = true;
    private float defautGravityScale = 0.0f;
    private bool climbing = false;
    private float coyoteTimeCounter = 0.0f;
    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    public UnityEvent OnJumpEvent;
    public UnityEvent OnStartClimbingEvent;

    //Debug
    private bool ladderDetect = false;
    private bool jumpCancel = false;
    private bool isCheating = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnJumpEvent == null)
            OnJumpEvent = new UnityEvent();

        if (OnStartClimbingEvent == null)
            OnStartClimbingEvent = new UnityEvent();
    }

    private void Start()
    {
        defautGravityScale = m_Rigidbody2D.gravityScale;
    }

    private void Update()
    {
        if (m_Grounded)
            coyoteTimeCounter = movementConfig.CoyoteTime;
        else if (climbing)
            coyoteTimeCounter = 0.0f;
        else
            coyoteTimeCounter -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (lockMovement)
            return;

        CheckGround();
        ApplyFallGravityScale();
    }

    private async void CheckGround()
    {
        if (isCheating)
            return;

        if (!m_GroundCheck)
            return;

        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        if (Physics2D.OverlapBox(m_GroundCheck.position, groundCheckBoxCastSize, 0.0f, m_WhatIsGround))
        {
            m_Grounded = true;
            if (!wasGrounded)
            {
                OnLandEvent.Invoke();

                if (!isJumpReady)
                    await RecoverAfterJumpLanding();

                if (climbing)
                    DisableClimb();
            }
        }
    }

    private void CheckSlope(float horizontalMove)
    {
        if (isCheating)
            return;

        //Check slope Vertical
        if (m_Grounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(m_GroundCheck.position, Vector2.down, groundCheckBoxCastSize.y / 2, m_WhatIsGround);
            if (hit)
            {
                slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (slopeDownAngle != 0.0f && horizontalMove == 0.0f)
                {
                    m_Rigidbody2D.sharedMaterial = fullFriction;
                    Debug.DrawRay(hit.point, hit.normal, Color.yellow);
                }
                else
                {
                    m_Rigidbody2D.sharedMaterial = zeroFriction;
                }
            }
        }
    }

    private void ApplyFallGravityScale()
    {
        if (isCheating)
            return;

        if (climbing)
            return;

        if (!m_Grounded)
        {
            if (m_Rigidbody2D.velocity.y < 0.0f)
                m_Rigidbody2D.gravityScale = movementConfig.FallGravityScale;
            else if (allowLowJump && m_Rigidbody2D.velocity.y > 0.0f && jumpCancel)
                m_Rigidbody2D.gravityScale = movementConfig.LowJumpGravityScale;
            else
                m_Rigidbody2D.gravityScale = defautGravityScale;
        }
        else
        {
            m_Rigidbody2D.gravityScale = defautGravityScale;
        }
    }

    private async UniTask RecoverAfterJumpLanding()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(movementConfig.JumpRecoverTime));
        isJumpReady = true;
    }

    private void Flip()
    {
        if (allowFlip)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        if (m_Grounded)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Vector3 bottomLeft = m_GroundCheck.position + new Vector3(-groundCheckBoxCastSize.x / 2, -groundCheckBoxCastSize.y / 2);
        Vector3 bottomRight = m_GroundCheck.position + new Vector3(groundCheckBoxCastSize.x / 2, -groundCheckBoxCastSize.y / 2);
        Vector3 topLeft = m_GroundCheck.position + new Vector3(-groundCheckBoxCastSize.x / 2, groundCheckBoxCastSize.y / 2);
        Vector3 topRight = m_GroundCheck.position + new Vector3(groundCheckBoxCastSize.x / 2, groundCheckBoxCastSize.y / 2);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);


        if (ladderDetect)
        {
            Gizmos.color = Color.blue;

            bottomLeft = m_LadderCheck.position + new Vector3(-ladderCheckBoxCastSize.x / 2, -ladderCheckBoxCastSize.y / 2);
            bottomRight = m_LadderCheck.position + new Vector3(ladderCheckBoxCastSize.x / 2, -ladderCheckBoxCastSize.y / 2);
            topLeft = m_LadderCheck.position + new Vector3(-ladderCheckBoxCastSize.x / 2, ladderCheckBoxCastSize.y / 2);
            topRight = m_LadderCheck.position + new Vector3(ladderCheckBoxCastSize.x / 2, ladderCheckBoxCastSize.y / 2);

            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(bottomRight, topRight);
            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.DrawLine(topLeft, bottomLeft);
        }
    }


    public void Move(Vector2 move)
    {
        if (lockMovement)
            return;

        CheckSlope(move.x);

        //vertical move
        MoveVerticalAxis(move.y);
        //horizontal move
        MoveHorizontalAxis(move.x);
    }

    private void MoveVerticalAxis(float move)
    {
        if (isCheating)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, move * 25f);
        }
        else if (m_LadderCheck)
        {
            if (Physics2D.OverlapBox(m_LadderCheck.position, ladderCheckBoxCastSize, 0.0f, m_WhatIsLadder))
            {
                ladderDetect = true;
                if (!climbing && move > 0.0f)
                {
                    EnableClimb();
                    OnStartClimbingEvent.Invoke();
                }

                if (climbing)
                {
                    Vector3 targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, move * 10f);
                    m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, movementConfig.MovementSmoothing);
                }
            }
            else if (climbing)
            {
                DisableClimb();
            }
            else
            {
                ladderDetect = false;
            }
        }
    }

    private void MoveHorizontalAxis(float move)
    {
        if (isCheating)
        {
            m_Rigidbody2D.velocity = new Vector2(move * 20f, m_Rigidbody2D.velocity.y);
            if (move > 0 && transform.rotation.y < 0)
                Flip();
            else if (move < 0 && transform.rotation.y >= 0)
                Flip();
        }
        else if (m_Grounded || climbing)
        {
            ApplyVelocity(move, movementConfig.MovementSmoothing);
        }
        else if (m_AirControl)
        {
            ApplyVelocity(move, movementConfig.AirSmoothing);
        }
    }

    private void ApplyVelocity(float move, float smooth)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, smooth);

        if (move > 0 && transform.rotation.y < 0)
            Flip();
        else if (move < 0 && transform.rotation.y >= 0)
            Flip();
    }

    public void EnableClimb()
    {
        climbing = true;
        m_Rigidbody2D.gravityScale = 0.0f;
    }

    public void DisableClimb()
    {
        climbing = false;
        m_Rigidbody2D.gravityScale = defautGravityScale;
    }

    public void Jump()
    {
        if (lockMovement)
            return;

        if (isCheating)
            return;

        if (climbing)
            return;

        if (coyoteTimeCounter > 0.0f && isJumpReady)
        {
            jumpCancel = false;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, movementConfig.JumpForce);
            OnJumpEvent.Invoke();
            isJumpReady = false;
        }
    }

    public void CancelJump()
    {
        coyoteTimeCounter = 0.0f;
        jumpCancel = true;
    }

    public void LockMovement(bool enable)
    {
        lockMovement = enable;
        m_Rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
    }

    public bool IsLockingMovement()
    {
        return lockMovement;
    }

    public bool IsClimbing()
    {
        return climbing;
    }

    public bool IsGrounded()
    {
        return m_Grounded;
    }

    public void SetAllowFlip(bool allow)
    {
        allowFlip = allow;
    }

    public Collider2D IsStandingOn(LayerMask layerMask)
    {
        return Physics2D.OverlapBox(m_GroundCheck.position, groundCheckBoxCastSize, 0.0f, layerMask);
    }

    public void SetMovementConfig(MovementConfig config)
    {
        movementConfig = config;
    }

    public void EnableCheat()
    {
        isCheating = !isCheating;
        Debug.Log(isCheating ? "Enable Cheat Mode" : "Disable Cheat Mode");
        m_Rigidbody2D.gravityScale = isCheating ? 0.0f : defautGravityScale;
    }
}