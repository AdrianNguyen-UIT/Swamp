using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/TweenUI Config", fileName = "New TweenUI Config")]
public class TweenUIConfig: ScriptableObject
{
    [SerializeField] private Vector3 transform = Vector3.zero;
    [SerializeField] private Vector3 rotate = Vector3.zero;
    [SerializeField] private Vector3 scale = Vector3.one;
    [SerializeField] private Color color = Color.black;
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private int loopCount = 0;
    [SerializeField] private float alpha = 0.0f;

    public Vector3 Transform => transform;
    public Vector3 Rotate => rotate;
    public Vector3 Scale => scale;
    public Color Color => color;
    public float Duration => duration;
    public int LoopCount => loopCount;
    public float Alpha => alpha;
}
