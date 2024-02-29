using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCommander : MonoBehaviour
{
    public float scaleAmount;
    Stack<Vector3> undoList = new Stack<Vector3>();
    public void ScaleBuilding()
    {
        if (transform.localScale.y < 2.5f && scaleAmount > 0)
        {
            DoScaling();
        }
        else if((transform.localScale.y >= 2.5f || transform.localScale.y > .5) && scaleAmount < 0)
        {
            DoScaling();
        }
    }

    private void DoScaling()
    {
        Parent.instance.state = ParentState.Scaling;
        transform.localScale += Vector3.one * scaleAmount;
        undoList.Push(Vector3.one * scaleAmount);
        transform.position = new Vector3(transform.position.x, 0.05f, transform.position.z);
        Parent.instance.AssignPlacementValueOnAllChilds();   
    }
       
}
