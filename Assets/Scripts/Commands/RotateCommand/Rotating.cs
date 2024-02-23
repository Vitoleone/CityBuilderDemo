using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotating : MonoBehaviour
{
    public float rotateAmount;
    Parent parent;
    public Stack<Vector3> undoRotatations = new Stack<Vector3>();
    public Stack<Vector3> redoRotatations = new Stack<Vector3>();

    private void Start()
    {
        parent = GetComponent<Parent>();
    }

    public void RotateBuilding()
    {
        if (parent.state == Parent.ParentState.Free)
        {
            CommandScheduler.ResetStacks();
        }
        GetComponent<Parent>().state = Parent.ParentState.Rotating;
        undoRotatations.Push(transform.rotation.eulerAngles);
        transform.Rotate(new Vector3(0, rotateAmount, 0));
        parent.CheckPlacable();
        
    }
    public void Undo()
    {
        if(undoRotatations.Count > 0)
        {
            GetComponent<Parent>().state = Parent.ParentState.Rotating;
            undoRotatations.Pop();
            transform.Rotate(new Vector3(0, -rotateAmount, 0));
            redoRotatations.Push(transform.rotation.eulerAngles);
            parent.CheckPlacable();
        }
    }
    public void Redo()
    {
        if (redoRotatations.Count > 0)
        {
            GetComponent<Parent>().state = Parent.ParentState.Rotating;
            redoRotatations.Pop();
            transform.Rotate(new Vector3(0, rotateAmount, 0));
            undoRotatations.Push(transform.rotation.eulerAngles);
            parent.CheckPlacable();
        }
    }
    
}
