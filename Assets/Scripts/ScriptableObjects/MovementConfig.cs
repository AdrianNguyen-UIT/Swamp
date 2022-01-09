using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Movement Config", fileName = "New Movement Config")]
public class MovementConfig : ScriptableObject
{
    [Header("Walk And Sprint")]
    [SerializeField] private float walkSpeed = 10.0f;
    [SerializeField] private float climbSpeed = 10.0f;
    [Range(1, 3)] [SerializeField] private float sprintMultiplier = 1.75f;
    [Range(1, 3)] [SerializeField] private float pushPullMultiplier = 1.75f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12.0f;
    [SerializeField] private float jumpRecoverTime = 0.5f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float fallGravityScale = 4.0f;
    [SerializeField] private float lowJumpGravityScale = 5.0f;

    [Header("Smoothing")]
    [Range(0, .3f)] [SerializeField] private float airSmoothing = .15f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    public float WalkSpeed => walkSpeed;
    public float ClimbSpeed => climbSpeed;
    public float SprintMultiplier => sprintMultiplier;
    public float PushPullMultiplier => pushPullMultiplier;
    public float JumpForce => jumpForce;
    public float JumpRecoverTime => jumpRecoverTime;
    public float CoyoteTime => coyoteTime;
    public float FallGravityScale => fallGravityScale;
    public float LowJumpGravityScale => lowJumpGravityScale;
    public float AirSmoothing => airSmoothing;
    public float MovementSmoothing => movementSmoothing;
}
