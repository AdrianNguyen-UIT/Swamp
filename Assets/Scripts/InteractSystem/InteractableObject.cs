using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : UniqueID, ISaveable
{
    #region Variable Fields
    //Configuration
    public InteractableObjectConfig IOConfig = null;

    [Header("Subcribe to LevelEventChannel")]
    [SerializeField] private LevelEventChannel levelEventChannel = null;
    //Cached component references
    private FixedJoint2D joint = null;
    private Rigidbody2D rb = null;
    //State
    public bool Interacted = false;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        joint = GetComponent<FixedJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        levelEventChannel.AddInteractableObject(this);

        if (IOConfig.InteractType == InteractType.Grab)
        {
            joint.enabled = false;
            rb.sharedMaterial = IOConfig.IdleFriction;
        }
    }

    #endregion

    #region Private Methods
    #endregion

    public virtual void OnGrabbed()
    {
        joint.enabled = true;
        joint.connectedBody = Player.Instance.GetComponent<Rigidbody2D>();
        rb.sharedMaterial = IOConfig.GrabbedFriction;
        Interacted = true;
        Debug.Log("Grabbing " + gameObject.name);
    }

    public virtual void OnReleased()
    {
        joint.connectedBody = null;
        joint.enabled = false;
        rb.sharedMaterial = IOConfig.IdleFriction;
        Interacted = false;
        Debug.Log("Releasing " + gameObject.name);
    }

    public virtual void OnTouched()
    {
        Interacted = true;
        levelEventChannel.RemoveInteractableObject(this);
    }

    protected virtual void OnLoadFromSaveData()
    {
    }

    protected virtual void OnLoadDestroyed()
    {
    }

    public void PopulateSaveData(SaveData saveData)
    {
        SaveData.InteractableObjectData data = new SaveData.InteractableObjectData
        {
            UID = UID
        };

        if (IOConfig.InteractType == InteractType.Grab)
        {
            data.Position = transform.position;
            data.Rotation = transform.rotation;
        }

        saveData.InteractableObjects.Add(data);
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        SaveData.InteractableObjectData data = new SaveData.InteractableObjectData
        {
            UID = UID
        };

        int index = saveData.InteractableObjects.BinarySearch(data);
        if (index < 0)
        {
            index = saveData.DestroyInteractableObjectUIDs.BinarySearch(UID);
            if (index < 0)
                Debug.LogError($"Interactable Object (UID: {UID}) doesn't exist!");
            else
            {
                OnLoadDestroyed();
                levelEventChannel.RemoveInteractableObject(this);
            }
        }
        else
        {
            if (IOConfig.InteractType == InteractType.Grab)
            {
                joint.enabled = false;
                rb.sharedMaterial = IOConfig.IdleFriction;
                transform.SetPositionAndRotation(
                    saveData.InteractableObjects[index].Position,
                    saveData.InteractableObjects[index].Rotation);
            }
            OnLoadFromSaveData();
        }
    }
}