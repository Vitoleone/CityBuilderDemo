using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public float scaleAmount;
    Stack<Vector3> undoList = new Stack<Vector3>();
    Stack<Vector3> redoList = new Stack<Vector3>();
    Parent parent;
    private void Start()
    {
        parent = GetComponent<Parent>();
    }

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
        if (parent.state == Parent.ParentState.Free)
        {
            CommandScheduler.ResetStacks(); 
            parent.state = Parent.ParentState.Scaling;
        }
        
        transform.localScale += Vector3.one * scaleAmount;
        undoList.Push(Vector3.one * scaleAmount);
        transform.position = new Vector3(transform.position.x, transform.position.y + scaleAmount / 2, transform.position.z);
        parent.CheckPlacable();
    }

    public void Undo()
    {
        if(undoList.Count > 0)
        {
            GetComponent<Parent>().state = Parent.ParentState.Scaling;
            Vector3 undoVector = undoList.Pop();
            transform.localScale -= undoVector;
            redoList.Push(-undoVector);
            transform.position = new Vector3(transform.position.x, transform.position.y - scaleAmount / 2, transform.position.z);
            parent.CheckPlacable();
        }
        
    }
    public void Redo()
    {
        if (redoList.Count > 0)
        {
            GetComponent<Parent>().state = Parent.ParentState.Scaling;
            Vector3 redoVector = redoList.Pop();
            transform.localScale -= redoVector;
            undoList.Push(redoVector);
            transform.position = new Vector3(transform.position.x, transform.position.y - scaleAmount / 2, transform.position.z);
            parent.CheckPlacable();
        }

    }
       
}
