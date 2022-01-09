using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class AbilitySystem : Singleton<AbilitySystem>, ISaveable
{
    [System.Serializable]
    public class CardUI
    {
        public RectTransform Transform = null;
        public Image Background = null;
        public Image Artwork = null;
        public TextMeshProUGUI Name = null;
        public TextMeshProUGUI OperateType = null;
        public TextMeshProUGUI StaminaCost = null;
        public TextMeshProUGUI Description = null;
        public void Init(Ability ability, Color passive, Color active)
        {
            Name.text = ability.Name;
            OperateType.text = ability._OperationType == Ability.OperateType.Active ? "Active" : "Passive";
            StaminaCost.text = ability.StaminaCost.ToString();
            Description.text = ability.Description;
            Background.color = ability._OperationType == Ability.OperateType.Active ? active : passive;
            Artwork.sprite = ability.Artwork;
        }

        public void Remove()
        {
            Name.text = string.Empty;
            OperateType.text = string.Empty;
            StaminaCost.text = string.Empty;
            Description.text = string.Empty;
            Background.color = Color.grey;
            Artwork.sprite = null;
        }
    }

    #region Variable Fields
    //Configurations
    [Header("UI")]
    [SerializeField] private Color passiveColor = Color.black;
    [SerializeField] private Color activeColor = Color.white;

    [SerializeField] private CardUI firstCard = null;
    [SerializeField] private CardUI secondCard = null;

    [SerializeField] private TweenUIConfig toRight = null;
    [SerializeField] private TweenUIConfig toLeft = null;

    [SerializeField] private Image firstAbilityIcon = null;
    [SerializeField] private Image secondAbilityIcon = null;


    [SerializeField] private Sprite emptyAbilityIcon = null;
    [Space]
    [SerializeField] private Ability[] abilityList = null;
    //Cached component references

    //States

    //Data storages
    private readonly static int abilityCount = 2;
    private Ability[] abilities;

    public Ability FirstAbility
    {
        get { return abilities[0]; }
        set { abilities[0] = value; }
    }

    public Ability SecondAbility
    {
        get { return abilities[1]; }
        set { abilities[1] = value; }
    }
    #endregion

    #region Unity Methods
    protected override void Awake()
    {
        base.Awake();
        abilities = new Ability[abilityCount];

        firstAbilityIcon.sprite = emptyAbilityIcon;
        firstAbilityIcon.color = Color.black;
        secondAbilityIcon.sprite = emptyAbilityIcon;
        secondAbilityIcon.color = Color.black;
        firstCard.Remove();
        secondCard.Remove();
    }

    private void Update()
    {
        if (FirstAbility != null)
            FirstAbility.Update();

        if (SecondAbility != null)
            SecondAbility.Update();

    }
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods

    public void OnSwapAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            firstCard.Transform.SetAsFirstSibling();
            Debug.Log("Swap");
            LeanTween
                .move(firstCard.Transform, toRight.Transform, toRight.Duration)
                .setIgnoreTimeScale(true);
            LeanTween
                .move(secondCard.Transform, toLeft.Transform, toLeft.Duration)
                .setIgnoreTimeScale(true)
                .setOnComplete(() =>
                {
                    secondCard.Transform.SetAsLastSibling();
                });

            RectTransform tempt = secondCard.Transform;
            secondCard.Transform = firstCard.Transform;
            firstCard.Transform = tempt;

            Ability temptAbility = FirstAbility;
            FirstAbility = SecondAbility;
            SecondAbility = temptAbility;

            Sprite artwork = firstAbilityIcon.sprite;
            Color tempColor = firstAbilityIcon.color;

            firstAbilityIcon.sprite = secondAbilityIcon.sprite;
            firstAbilityIcon.color = secondAbilityIcon.color;

            secondAbilityIcon.sprite = artwork;
            secondAbilityIcon.color = tempColor;

        }
    }

    public void OnActiveFirstAbility(InputAction.CallbackContext context)
    {
        if (FirstAbility == null)
            return;

        if (FirstAbility._OperationType == Ability.OperateType.Passive)
            return;

        if (context.performed)
        {
            if (SecondAbility != null)
            {
                if (SecondAbility._State == Ability.State.Activating)
                {
                    if (SecondAbility._DisturbedBehavior == Ability.DisturbedBehavior.Cancel)
                    {
                        SecondAbility.Cancel();
                        Debug.Log("Disturbing Second Ability");
                    }
                    else if (SecondAbility._DisturbedBehavior == Ability.DisturbedBehavior.DoNotDisturb)
                    {
                        return;
                    }
                }
            }

            FirstAbility.Activate();

        }
        else if (context.canceled && FirstAbility._CancelType == Ability.CancelType.Active)
        {
            FirstAbility.Cancel();
        }
    }

    public void OnActiveSecondAbility(InputAction.CallbackContext context)
    {
        if (SecondAbility == null)
            return;

        if (SecondAbility._OperationType == Ability.OperateType.Passive)
            return;

        if (context.performed)
        {
            if (FirstAbility != null)
            {
                if (FirstAbility._State == Ability.State.Activating)
                {
                    if (FirstAbility._DisturbedBehavior == Ability.DisturbedBehavior.Cancel)
                    {
                        FirstAbility.Cancel();
                        Debug.Log("Disturbing First Ability");
                    }
                    else if (FirstAbility._DisturbedBehavior == Ability.DisturbedBehavior.DoNotDisturb)
                    {
                        return;
                    }
                }
            }

            SecondAbility.Activate();
        }
        else if (context.canceled && SecondAbility._CancelType == Ability.CancelType.Active)
        {
            SecondAbility.Cancel();
        }
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        foreach (var ability in abilityList)
        {
            if (ability.Name == saveData.CurrentAbilities[0] ||
                ability.Name == saveData.CurrentAbilities[1])
            {
                PickUpAbility(ability);
            }
        }
    }

    public void PopulateSaveData(SaveData saveData)
    {
        if (FirstAbility == null)
            saveData.CurrentAbilities.Add(string.Empty);
        else
            saveData.CurrentAbilities.Add(FirstAbility.Name);

        if (SecondAbility == null)
            saveData.CurrentAbilities.Add(string.Empty);
        else
            saveData.CurrentAbilities.Add(SecondAbility.Name);


    }

    public bool PickUpAbility(Ability ability)
    {
        if (FirstAbility == null)
        {
            if (SecondAbility != null && SecondAbility == ability)
                return false;

            FirstAbility = ability;
            firstAbilityIcon.sprite = ability.Artwork;
            firstAbilityIcon.color = Color.white;
            firstCard.Init(ability, passiveColor, activeColor);
            ability.Init(gameObject);

            if (ability._OperationType == Ability.OperateType.Passive)
                ability.Activate();
            return true;
        }
        else if (SecondAbility == null)
        {
            if (FirstAbility != null && FirstAbility == ability)
                return false;

            SecondAbility = ability;
            secondAbilityIcon.sprite = ability.Artwork;
            secondAbilityIcon.color = Color.white;
            secondCard.Init(ability, passiveColor, activeColor);
            ability.Init(gameObject);

            if (ability._OperationType == Ability.OperateType.Passive)
                ability.Activate();
            return true;
        }
        return false;
    }

    public void RemoveAbility(Ability ability)
    {
        if (FirstAbility == ability)
        {
            FirstAbility = null;
            firstAbilityIcon.sprite = emptyAbilityIcon;
            firstAbilityIcon.color = Color.black;
            firstCard.Remove();
        }
        else if (SecondAbility == ability)
        {
            SecondAbility = null;
            secondAbilityIcon.sprite = emptyAbilityIcon;
            secondAbilityIcon.color = Color.black;
            secondCard.Remove();
        }
    }
    #endregion
}
