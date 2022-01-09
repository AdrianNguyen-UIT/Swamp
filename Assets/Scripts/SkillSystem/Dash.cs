using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dash", fileName = "Dash Ability")]
public class Dash : Ability
{
    [SerializeField] private float dashSpeed = 15.0f;
    [SerializeField] private float dashLength = 1.0f;

    public float DashSpeed => dashSpeed;
    public float DashLength => dashLength;

    private float timeStartDash = 0.0f;
    private bool isDashing = false;
    private Vector3 dashDir = Vector3.right;
    private float defaultGravityScale = 0.0f;
    private Rigidbody2D playerRb = null;

    public override void Activate()
    {
        base.Activate();
        player.GetComponent<Player>().LockPlayerMovement();
        dashDir = player.GetComponent<PlayerMovement>().GetInputDir();
        if (dashDir == Vector3.zero)
            dashDir = player.transform.rotation.y >= 0.0f ? Vector3.right : Vector3.left;
        isDashing = true;
        timeStartDash = Time.time;
        defaultGravityScale = playerRb.gravityScale;
        playerRb.gravityScale = 0.0f;
        Debug.Log(dashDir);
    }

    public override void Update()
    {
        base.Update();

        if (isDashing)
        {
            playerRb.velocity = dashDir * dashSpeed;

            if (Time.time >= timeStartDash + dashLength)
            {
                Cancel();
            }
        }
    }

    public override void Init(GameObject _player)
    {
        base.Init(_player);
        timeStartDash = 0.0f;
        isDashing = false;
        dashDir = Vector3.right;
        defaultGravityScale = 0.0f;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    public override void Cancel()
    {
        isDashing = false;
        playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y > 3.0f ? 3.0f : playerRb.velocity.y);
        playerRb.gravityScale = defaultGravityScale;
        playerRb = null;
        player.GetComponent<Player>().UnlockPlayerMovement();
        base.Cancel();
    }

    public override void Copy(Ability ability)
    {
        base.Copy(ability);
        dashSpeed = ((Dash)ability).DashSpeed;
        dashLength = ((Dash)ability).DashLength;
    }
}
