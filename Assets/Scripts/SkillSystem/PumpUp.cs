using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Pump Up", fileName = "Pump Up Ability")]
public class PumpUp : Ability
{
    [SerializeField] private MovementConfig pumpUpMovementConfig = null;
    [SerializeField] private MovementConfig defaultMovementConfig = null;

    [SerializeField] private float pumpUpTime = 8.0f;

    public MovementConfig PumpUpMovementConfig => pumpUpMovementConfig;
    public MovementConfig DefaultMovementConfig => defaultMovementConfig;
    public float PumpUpTime => pumpUpTime;

    private float currentTime = 0.0f;
    public override void Activate()
    {
        base.Activate();
        player.GetComponent<PlayerMovement>().SetMovementConfig(pumpUpMovementConfig);
    }

    public override void Update()
    {
        base.Update();
        currentTime += Time.deltaTime;
        if (currentTime >= pumpUpTime)
        {
            Cancel();
        }
    }

    public override void Init(GameObject _player)
    {
        base.Init(_player);
        currentTime = 0.0f;
    }

    public override void Cancel()
    {
        player.GetComponent<PlayerMovement>().SetMovementConfig(defaultMovementConfig);
        base.Cancel();
    }

    public override void Copy(Ability ability)
    {
        base.Copy(ability);
        pumpUpMovementConfig = ((PumpUp)ability).PumpUpMovementConfig;
        defaultMovementConfig = ((PumpUp)ability).DefaultMovementConfig;
        pumpUpTime = ((PumpUp)ability).PumpUpTime;
    }
}
