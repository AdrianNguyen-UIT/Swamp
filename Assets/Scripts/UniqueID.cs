using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UniqueID : MonoBehaviour
{
    public string UID = System.Guid.NewGuid().ToString();

    [ContextMenu("Generate new ID")]
    private void RegenerateGUID() => UID = System.Guid.NewGuid().ToString();
}
