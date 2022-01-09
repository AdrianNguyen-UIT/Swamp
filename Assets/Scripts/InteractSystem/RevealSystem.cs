using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class RevealSystem : UniqueID, ISaveable
{
    [System.Serializable]
    public class RevealNode
    {
        [SerializeField] private float idleTime = 1.0f;
        [SerializeField] private CinemachineVirtualCamera camera = null;
        [SerializeField] private RevealedTerrain[] revealedTerrains = null;

        public CinemachineVirtualCamera Camera => camera;
        public float IdleTime => idleTime;
        public RevealedTerrain[] RevealedTerrains => revealedTerrains;

    }

    #region Variable Fields
    //Configurations
    [SerializeField] private RevealNode[] revealNodes = null;
    [SerializeField] private int mainCameraPriority = 1;
    [SerializeField] private float cameraBlendTime = 1.0f;
    [Range(0, 10)] [SerializeField] private int keyCount = 0;

    [Header("Broadcast On Channel")]
    [SerializeField] private VoidEventChannel voidEventChannel = null;
    [SerializeField] private LevelEventChannel levelEventChannel = null;
    //Cached component references

    //States
    private int currentKey = 0;
    public bool Revealed { get; private set; } = false;
    //Data storages
    #endregion

    #region Unity Methods

    private void Start()
    {
        currentKey = 0;
        levelEventChannel.AddRevealSystem(this);
    }

    private void OnEnable()
    {
        voidEventChannel.OnEventRaised += OnKeyTouched;
    }

    private void OnDisable()
    {
        voidEventChannel.OnEventRaised -= OnKeyTouched;
    }

    private void OnLoadFromSaveData()
    {
        if (Revealed)
        {
            foreach (var node in revealNodes)
            {
                Parallel.ForEach(node.RevealedTerrains, item =>
                {
                    ThreadDispatcher.Instance.RunOnMainThread(item.Reveal);
                });
            }
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region Private Methods
    private async UniTask RevealHiddenTerrain()
    {
        Debug.Log("Begin Reveal");
        Player.Instance.LockPlayerMovement();
        foreach (var node in revealNodes)
        {
            node.Camera.Priority = mainCameraPriority + 1;
            await UniTask.Delay(System.TimeSpan.FromSeconds(cameraBlendTime));

            Parallel.ForEach(node.RevealedTerrains, item =>
            {
                ThreadDispatcher.Instance.RunOnMainThread(item.Reveal);
            });

            await UniTask.Delay(System.TimeSpan.FromSeconds(node.IdleTime));
            node.Camera.Priority = 0;
        }
        await UniTask.Delay(System.TimeSpan.FromSeconds(cameraBlendTime + 0.5f));
        Revealed = true;
        Player.Instance.UnlockPlayerMovement();
        gameObject.SetActive(false);
    }

    private async void OnKeyTouched()
    {
        ++currentKey;

        if (currentKey == keyCount && !Revealed)
            await RevealHiddenTerrain();
    }

    public void PopulateSaveData(SaveData saveData)
    {
        SaveData.RevealSystemData data = new SaveData.RevealSystemData
        {
            UID = UID,
            Revealed = Revealed
        };

        saveData.RevealSystems.Add(data);
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        SaveData.RevealSystemData data = new SaveData.RevealSystemData
        {
            UID = UID
        };

        int index = saveData.RevealSystems.BinarySearch(data);
        if (index < 0)
            Debug.LogError($"Reveal System (UID: {UID}) doesn't exist!");
        else
        {
            Revealed = saveData.RevealSystems[index].Revealed;
            OnLoadFromSaveData();
        }
    }
    #endregion

    #region Public Methods
    #endregion
}
