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
    
    public void RotateBuilding()
    {
        GetComponent<Parent>().state = Parent.ParentState.Rotating;
       
        GetComponent<Parent>().ControlChildPlacement();
        if(!SelectManager.instance.AllCanBuild())
        {
            UIManager.instance.SetFunctionalButtonsActivness(false);
            UIManager.instance.ShowOnlyRotationButtons(true);
        }
        else
        {
            UIManager.instance.SetFunctionalButtonsActivness(true);
            GetComponent<Parent>().state = Parent.ParentState.Free;
        }
        undoRotatations.Push(transform.rotation.eulerAngles);
        transform.Rotate(new Vector3(0, rotateAmount, 0));
        
        
    }
    public void Undo()
    {
        if(undoRotatations.Count > 0)
        {
            GetComponent<Parent>().ControlChildPlacement();
            undoRotatations.Pop();
            transform.Rotate(new Vector3(0, -rotateAmount, 0));
            redoRotatations.Push(transform.rotation.eulerAngles);
        }
    }
    public void Redo()
    {
        if (redoRotatations.Count > 0)
        {
            GetComponent<Parent>().ControlChildPlacement();
            redoRotatations.Pop();
            transform.Rotate(new Vector3(0, rotateAmount, 0));
            undoRotatations.Push(transform.rotation.eulerAngles);
        }
    }
}
