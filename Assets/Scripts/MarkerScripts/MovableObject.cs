using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SelectableObject))]
public class MovableObject : MonoBehaviour
{
    public UnityEvent onMovementStart;
    public UnityEvent onMovementDone;
    public void StartMovement()
    {
        onMovementStart.Invoke();
    }
    public void StopMovement()
    {   
        onMovementDone.Invoke();
    }
}
