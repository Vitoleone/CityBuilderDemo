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
            parent.state = Parent.ParentState.Scaling;
            transform.localScale += Vector3.one * scaleAmount;
            undoList.Push(Vector3.one * scaleAmount);
            transform.position = new Vector3(transform.position.x, transform.position.y + scaleAmount / 2, transform.position.z);
            CheckPlacable();
        }
        else if((transform.localScale.y >= 2.5f || transform.localScale.y > .5) && scaleAmount < 0)
        {
            parent.state = Parent.ParentState.Scaling;
            transform.localScale += Vector3.one * scaleAmount;
            undoList.Push(Vector3.one * scaleAmount);
            transform.position = new Vector3(transform.position.x, transform.position.y + scaleAmount / 2, transform.position.z);
            CheckPlacable();
        }

    }

    private void CheckPlacable()
    {
        parent.ControlChildPlacement();
        if (SelectManager.instance.AllCanBuild())
        {
            parent.state = Parent.ParentState.Free;
            UIManager.instance.SetFunctionalButtonsActivness(true);
        }
        else
        {
            UIManager.instance.SetFunctionalButtonsActivness(false);
            UIManager.instance.ShowOnlyRotationAndScaleButtons(true);
        }
    }

    public void Undo()
    {
        if(undoList.Count > 0)
        {
            GetComponent<Parent>().state = Parent.ParentState.Scaling;
            Vector3 undoVector = undoList.Pop();
            transform.localScale -= undoVector;
            redoList.Push(undoVector);
            transform.position = new Vector3(transform.position.x, transform.position.y - scaleAmount / 2, transform.position.z);
            CheckPlacable();
        }
        
    }
    public void Redo()
    {
        if (redoList.Count > 0)
        {
            GetComponent<Parent>().state = Parent.ParentState.Scaling;
            Vector3 redoVector = redoList.Pop();
            transform.localScale += redoVector;
            undoList.Push(redoVector);
            transform.position = new Vector3(transform.position.x, transform.position.y - scaleAmount / 2, transform.position.z);
            CheckPlacable();
        }

    }
       
}
