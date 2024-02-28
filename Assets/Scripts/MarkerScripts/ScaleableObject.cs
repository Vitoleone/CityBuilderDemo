using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SelectableObject))]
public class ScaleableObject : MonoBehaviour
{
    public UnityEvent onScaleStart;
    public UnityEvent onScaleEnd;
    public void StartScale()
    {
        onScaleStart.Invoke();
    }
    public void EndScale()
    {
        onScaleEnd.Invoke();
    }
}
