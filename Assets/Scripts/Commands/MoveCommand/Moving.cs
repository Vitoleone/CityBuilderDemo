using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    Stack<Vector3> _undoStack = new Stack<Vector3>();
    Stack<Vector3> _redoStack = new Stack<Vector3>();
    public bool canMove, movementFinished;
    float xOffsetValue = -1;


    public void Move()
    {
        if(canMove)
        {
            if(xOffsetValue < 0)
            {
                xOffsetValue = SelectManager.instance.xOffset;
                SelectManager.instance.xOffset += 40;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {       
                
                if(_undoStack.Count <= 0 )
                {
                    _undoStack.Push(transform.position);
                }
                transform.position = new Vector3(hitInfo.point.x + xOffsetValue, transform.localScale.y / 2, hitInfo.point.z);
                if (Input.GetMouseButtonDown(0))
                {
                    movementFinished = true;
                    canMove = false;
                    SelectManager.instance.DeselectAllUnits();
                }
            }
        }
        else if(movementFinished)
        {
            canMove = false;
        }
    }
    private void Update()
    {
        Move();
    }
    public void Undo()
    {
        if(_undoStack.Count > 0)
        {
            transform.position = _undoStack.Pop();
            _redoStack.Push(transform.position);
        }
    }
    public void Redo()
    {
        if (_redoStack.Count > 0)
        {
            _undoStack.Push(transform.position);
            transform.position = _redoStack.Pop();
        }
    }

    

}
