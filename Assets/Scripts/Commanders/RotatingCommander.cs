using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class RotatingCommander : MonoBehaviour
{
    public float rotateAmount;
    Parent parent;
    public Stack<Vector3> undoRotatations = new Stack<Vector3>();

    private void Start()
    {
        parent = GetComponent<Parent>();
    }

    public void RotateBuilding()
    {
        GetComponent<Parent>().state = Parent.ParentState.Rotating;
        undoRotatations.Push(transform.rotation.eulerAngles);
        transform.Rotate(new Vector3(0, rotateAmount, 0));
        parent.CheckPlacable();
    }
    public bool CheckRotatableButton()
    {
        bool shouldActive = true;
        foreach (Building building in SelectManager.instance.selectedUnits)
        {
            if (building.GetComponent<RotatableObject>() == null)
            {
                shouldActive = false;
                return shouldActive;
            }
        }
        return shouldActive;
    }
    public void Undo()
    {
        if(undoRotatations.Count > 0)
        {
            GetComponent<Parent>().state = Parent.ParentState.Rotating;
            undoRotatations.Pop();
            transform.Rotate(new Vector3(0, -rotateAmount, 0));
            parent.CheckPlacable();
        }
    }
    
}
