using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Configs/Stamina Config", fileName = "New Stamina Config")]
public class StaminaConfig : ScriptableObject
{
    [SerializeField] private float maxStamina = 100.0f;

    [Header("Recover")]
    [SerializeField] private float recoverRate = 50.0f;
    [SerializeField] private float recoverDelayTime = 0.4f;

    [Header("Stamina Consumption")]
    [SerializeField] private float sprintConsumeRate = 20.0f;
    [SerializeField] private float climbConsumeRate = 25.0f;
    [SerializeField] private float sprintClimbConsumeRate = 35.0f;
    [SerializeField] private float pushPullConsumeRate = 35.0f;
    [SerializeField] private float jumpConsumption = 25.0f;

    public float MaxStamina => maxStamina;
    public float RecoverRate => recoverRate;
    public float RecoverDelayTime => recoverDelayTime;
    public float SprintConsumeRate => sprintConsumeRate;
    public float ClimbConsumeRate => climbConsumeRate;
    public float SprintClimbConsumeRate => sprintClimbConsumeRate;
    public float PushPullConsumeRate => pushPullConsumeRate;
    public float JumpConsumption => jumpConsumption;
}
