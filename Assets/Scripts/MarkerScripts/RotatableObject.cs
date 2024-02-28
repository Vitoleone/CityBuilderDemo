using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SelectableObject))]
public class RotatableObject : MonoBehaviour
{
    public UnityEvent onRotateStart;
    public UnityEvent onRotateEnd;
    public void StartRotate()
    {
        onRotateStart.Invoke();
    }
    public void EndRotate()
    {
        onRotateEnd.Invoke();
    }
}
