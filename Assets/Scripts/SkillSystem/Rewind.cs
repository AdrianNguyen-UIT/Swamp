using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Reverse Time", fileName = "Reverse Time Ability")]
public class Rewind : Ability
{
    private class TimeFrame
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Sprite Sprite;

        public TimeFrame(Transform transform, Sprite sprite)
        {
            Position = transform.position;
            Rotation = transform.rotation;
            Sprite = sprite;
        }
    }
    [SerializeField] private float rewindTimePeriod = 5.0f;
    [SerializeField] private float saveInterval = 0.2f;
    [SerializeField] private float rewindInterval = 0.1f;

    public float RewindTimePeriod => rewindTimePeriod;
    public float SaveInterval => saveInterval;
    public float RewindInterval => rewindInterval;

    private LinkedList<TimeFrame> timeFrames;
    private SpriteRenderer playerSpriteRenderer;

    private float currentTime = 0.0f;
    private bool isRewinding = false;
    private float defaultGravityScale = 0.0f;
    private void InitTimeFrameList()
    {
        int numberOfFrames = (int)(rewindTimePeriod / saveInterval);
        timeFrames = new LinkedList<TimeFrame>();

        for (int index = 0; index < numberOfFrames; index++)
        {
            timeFrames.AddLast(new TimeFrame(player.transform, playerSpriteRenderer.sprite));
            Debug.Log(player.transform.position);
        }
    }

    private void Record()
    {
        timeFrames.RemoveFirst();
        timeFrames.AddLast(new TimeFrame(player.transform, playerSpriteRenderer.sprite));
    }

    private void RewindTime()
    {
        if (timeFrames.Count == 0)
        {
            Cancel();
            return;
        }

        player.transform.localPosition = timeFrames.Last.Value.Position;
        player.transform.localRotation = timeFrames.Last.Value.Rotation;
        playerSpriteRenderer.sprite = timeFrames.Last.Value.Sprite;
        timeFrames.RemoveLast();
    }

    #region override methods
    public override void Init(GameObject _player)
    {
        base.Init(_player);
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        InitTimeFrameList();
        currentTime = 0.0f;
        isRewinding = false;
        defaultGravityScale = 0.0f;
    }

    public override void Activate()
    {
        base.Activate();
        isRewinding = true;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
        rb.gravityScale = 0.0f;

        player.GetComponent<Collider2D>().isTrigger = true;
        player.GetComponent<Player>().LockPlayerMovement();
    }

    public override void Update()
    {
        base.Update();
        currentTime += Time.deltaTime;

        if (!isRewinding)
        {
            if (currentTime >= saveInterval)
            {
                Record();
                currentTime -= saveInterval;
            }
        }
        else
        {
            if (currentTime >= rewindInterval)
            {
                RewindTime();
                currentTime -= rewindInterval;
            }
        }
    }

    public override void Cancel()
    {
        isRewinding = false;
        playerSpriteRenderer = null;
        player.GetComponent<Rigidbody2D>().gravityScale = defaultGravityScale;
        player.GetComponent<Collider2D>().isTrigger = false;
        player.GetComponent<Player>().UnlockPlayerMovement();
        base.Cancel();
    }

    public override void Copy(Ability ability)
    {
        base.Copy(ability);
        rewindTimePeriod = ((Rewind)ability).RewindTimePeriod;
        saveInterval = ((Rewind)ability).SaveInterval;
        rewindInterval = ((Rewind)ability).RewindInterval;
    }
    #endregion
}
