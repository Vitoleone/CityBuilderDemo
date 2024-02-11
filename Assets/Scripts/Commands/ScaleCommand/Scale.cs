using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public float scaleAmount;
    public Stack<Vector3> undoScale = new Stack<Vector3>();
    public Stack<Vector3> redoScale = new Stack<Vector3>();
    public void ScaleBuilding()
    {
        if(transform.localScale.y < 2.5f)
        {
            undoScale.Push(transform.localScale);
            transform.localScale += Vector3.one * scaleAmount;
            transform.position = new Vector3(transform.position.x, transform.position.y + scaleAmount / 2, transform.position.z); 
        }
        
    }
    public void Undo()
    {
        if (undoScale.Count > 0)
        {
            undoScale.Pop();
            transform.localScale -= Vector3.one * scaleAmount;
            transform.position = new Vector3(transform.position.x, transform.position.y - scaleAmount / 2, transform.position.z);
            redoScale.Push(transform.localScale);
        }
    }
    public void Redo()
    {
        if (redoScale.Count > 0)
        {
            redoScale.Pop();
            transform.localScale += Vector3.one * scaleAmount;
            transform.position = new Vector3(transform.position.x, transform.position.y + scaleAmount / 2, transform.position.z);
            undoScale.Push(transform.localScale);
        }
    }
    private void OnDestroy()
    {
        if (SelectManager.instance.selectedUnits.Contains(gameObject))
        {
            SelectManager.instance.selectedUnits.Remove(gameObject);
        }
    }


}
