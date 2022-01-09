using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/High Jump", fileName = "High Jump Ability")]
public class HighJump : Ability
{
    [SerializeField] private float jumpForce = 20.0f;
    public float JumpForce => jumpForce;
    public override void Activate()
    {
        base.Activate();
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        player.GetComponent<PlayerMovement>().OnHighJumpAbilityEvent();
        Debug.Log("High Jump");
        base.Cancel();
    }

    public override void Copy(Ability ability)
    {
        base.Copy(ability);
        jumpForce = ((HighJump)ability).JumpForce;
    }
}
