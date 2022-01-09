using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public enum State
    {
        Ready,
        Activating,
        Finished
    }

    public enum DisturbedBehavior
    {
        DoNothing,
        Cancel,
        DoNotDisturb
    }

    public enum OperateType
    {
        Active,
        Passive
    }

    public enum CancelType
    {
        Active,
        Auto
    }

    [SerializeField] protected new string name = "Skill Name";
    [SerializeField] protected float stamiaCost = 0.0f;
    [SerializeField] protected string description = "Skill Description";
    [SerializeField] protected Sprite artwork = null;
    [SerializeField] protected OperateType operateType = OperateType.Active;
    [SerializeField] protected DisturbedBehavior disturbedBehavior = DisturbedBehavior.DoNothing;
    [SerializeField] protected CancelType cancelType = CancelType.Auto;
    protected State state = State.Ready;

    protected GameObject player = null;

    public string Name => name;
    public float StaminaCost => stamiaCost;
    public string Description => description;
    public Sprite Artwork => artwork;
    public OperateType _OperationType => operateType;
    public State _State => state;
    public DisturbedBehavior _DisturbedBehavior => disturbedBehavior;
    public CancelType _CancelType => cancelType;

    public virtual void Init(GameObject _player)
    {
        player = _player;
        state = State.Ready;
    }

    public virtual void Activate()
    {
        state = State.Activating;
        StaminaSystem staminaSystem = player.GetComponent<StaminaSystem>();
        staminaSystem.ConsumeStamina(stamiaCost);
        staminaSystem.ForceStopRecoverStamina();
    }

    public virtual void Update()
    {

    }

    public virtual void Cancel()
    {
        AbilitySystem.Instance.RemoveAbility(this);
        player.GetComponent<StaminaSystem>().ForceStartRecoverStamina();
        player = null;
        state = State.Finished;
    }

    public virtual void Copy(Ability ability)
    {
        name = ability.Name;
        stamiaCost = ability.StaminaCost;
        description = ability.Description;
        artwork = ability.Artwork;
        operateType = ability._OperationType;
        disturbedBehavior = ability._DisturbedBehavior;
        cancelType = ability.cancelType;
    }
}
