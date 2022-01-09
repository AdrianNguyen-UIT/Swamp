using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Diagnostics;
using System;

public class ParallaxBackground : MonoBehaviour
{
    private class BackgroundChild
    {
        public Transform Parent = null;
        public LinkedList<Transform> Childs = null;
        public float ObjWidth = 0.0f;
        public float ParallaxValue = 0.0f;

        public BackgroundChild(Transform parent, float width)
        {
            Parent = parent;
            ObjWidth = width;
            Childs = new LinkedList<Transform>();
        }
    }

    #region Variable Fields
    //Configuration
    [SerializeField] private Camera cam = null;
    //Cached component references

    //State
    private Vector2 previousCamPosition = Vector2.zero;
    private Vector2 screenBound = Vector2.zero;
    private List<BackgroundChild> backgroundChilds = new List<BackgroundChild>();
    #endregion

    #region Unity Methods
    private void Start()
    {
        transform.position = Vector3.zero;
        previousCamPosition = cam.transform.position;
        screenBound = new Vector2(cam.orthographicSize * Screen.width / Screen.height, cam.orthographicSize);
        foreach (Transform child in transform)
        {
            GameObject clone = Instantiate(child.gameObject);

            float objWidth = child.GetComponent<SpriteRenderer>().bounds.size.x;
            int childNeededs = (int)Mathf.Ceil((screenBound.x * 2 / objWidth)) + 1;

            BackgroundChild backgroundChild = new BackgroundChild(child, objWidth);
            float clippingPlane = cam.transform.position.z + (child.transform.position.z > 0.0f ? cam.farClipPlane : cam.nearClipPlane);
            backgroundChild.ParallaxValue = Mathf.Abs(child.transform.position.z / clippingPlane);

            for (int i = 0; i < childNeededs; i++)
            {
                GameObject c = Instantiate(clone);
                c.transform.SetParent(child.transform);
                c.transform.localPosition = new Vector2(objWidth * i, 0.0f);
                c.name = child.gameObject.name + i;

                backgroundChild.Childs.AddLast(c.transform);
            }
            Destroy(clone);
            Destroy(child.GetComponent<SpriteRenderer>());
            backgroundChilds.Add(backgroundChild);
        }
    }

    private async void LateUpdate()
    {
        //Stopwatch stopwatch = Stopwatch.StartNew();

        //UniTask[] tasks = new UniTask[backgroundChilds.Count * 2];
        //int index = -1;
        //for (int it = 0; it < backgroundChilds.Count - 1; it++)
        //{
        //    var child = backgroundChilds[it];

        //    tasks[++index] = RepositionInnerChild(child);
        //    tasks[++index] = ApplyParallaxEffect(child);
        //}

        List<UniTask> tasks = new List<UniTask>();
        foreach (var child in backgroundChilds)
        {
            tasks.Add(RepositionInnerChild(child));
            tasks.Add(ApplyParallaxEffect(child));
        }

        await UniTask.WhenAll(tasks);

        //stopwatch.Stop();
        //UnityEngine.Debug.Log("Repos and Parallax -> Time elapsed: " + stopwatch.ElapsedTicks + "ticks");
        previousCamPosition = cam.transform.position;
    }

    #endregion

    #region Private Methods

    private async UniTask ApplyParallaxEffect(BackgroundChild child)
    {
        Vector2 difference = (Vector2)cam.transform.position - previousCamPosition;
        Vector2 travel = difference * child.ParallaxValue * Vector3.right;
        child.Parent.transform.Translate(travel);
        await UniTask.CompletedTask;
    }

    private async UniTask RepositionInnerChild(BackgroundChild child)
    {
        if (child.Childs.Count <= 1)
            await UniTask.CompletedTask;

        Transform firstChild = child.Childs.First.Value;
        Transform lastChild = child.Childs.Last.Value;
        float objWitdh = child.ObjWidth;

        if (cam.transform.position.x + screenBound.x >= lastChild.position.x + objWitdh / 2.0f)
        {
            firstChild.SetAsLastSibling();
            firstChild.transform.localPosition = new Vector2(lastChild.localPosition.x + objWitdh, 0.0f);
            child.Childs.RemoveFirst();
            child.Childs.AddLast(firstChild);
        }
        else if (cam.transform.position.x - screenBound.x <= firstChild.position.x - objWitdh / 2.0f)
        {
            lastChild.SetAsFirstSibling();
            lastChild.transform.localPosition = new Vector2(firstChild.localPosition.x - objWitdh, 0.0f);
            child.Childs.RemoveLast();
            child.Childs.AddFirst(lastChild);
        }
    }
    #endregion
}
