using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotating : MonoBehaviour
{
    public float rotateAmount;
    public Stack<Vector3> undoRotatations = new Stack<Vector3>();
    public Stack<Vector3> redoRotatations = new Stack<Vector3>();
    
    private void OnMouseDown()
    {
        if(!SelectManager.instance.selectedUnits.Contains(gameObject))
        SelectManager.instance.SelectUnit(gameObject);
    }
    public void RotateBuilding()
    {
        undoRotatations.Push(transform.rotation.eulerAngles);
       
        transform.Rotate(new Vector3(0, rotateAmount, 0));
    }
    public void Undo()
    {
        if(undoRotatations.Count > 0)
        {
            undoRotatations.Pop();
            transform.Rotate(new Vector3(0, -rotateAmount, 0));
            redoRotatations.Push(transform.rotation.eulerAngles);
        }
    }
    public void Redo()
    {
        if (redoRotatations.Count > 0)
        {
            redoRotatations.Pop();
            transform.Rotate(new Vector3(0, rotateAmount, 0));
            undoRotatations.Push(transform.rotation.eulerAngles);
        }
    }


    private void OnDestroy()
    {
        if(SelectManager.instance.selectedUnits.Contains(gameObject))
        {
            SelectManager.instance.selectedUnits.Remove(gameObject);
        }
    }


}
