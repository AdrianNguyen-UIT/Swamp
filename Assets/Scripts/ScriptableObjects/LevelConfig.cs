using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Level Config", fileName = "New Level Config")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private string uid = System.Guid.NewGuid().ToString();
    [SerializeField] private LevelConfig previousLevel = null;
    [SerializeField] private LevelConfig nextLevel = null;
    [SerializeField] private Vector3 startingPos = Vector3.zero;

    public string UID => uid;
    public LevelConfig PreviousLevel => previousLevel;
    public LevelConfig NextLevel => nextLevel;
    public Vector3 StartingPos => startingPos;

    [ContextMenu("Generate new ID")]
    private void RegenerateGUID() => uid = System.Guid.NewGuid().ToString();
}
