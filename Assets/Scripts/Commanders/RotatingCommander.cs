using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class RotatingCommander : MonoBehaviour
{
    public float rotateAmount;
    public void RotateBuilding()
    {
        GetComponent<Parent>().state = ParentState.Rotating;
        transform.Rotate(new Vector3(0, rotateAmount, 0));
        Parent.instance.AssignAndCheckChildPlacementValues();
    }
    
}
