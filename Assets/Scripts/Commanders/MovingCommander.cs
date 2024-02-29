using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingCommander : MonoBehaviour
{
    public bool canMove;
    private bool movementFinished;
    private bool canPlaceable;

    private void Update()
    {
        if (canMove)
        Move();
    }
    public void Move()
    {
        Parent.instance.state = ParentState.Moving;
        movementFinished = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            transform.position = new Vector3(hitInfo.point.x, 0.05f, hitInfo.point.z);
            canPlaceable = Parent.instance.CheckAllChildsCanBePlaced();
            Parent.instance.AssignPlacementValueOnAllChilds();
            if (Input.GetMouseButtonDown(0))
            {
                if (canPlaceable)
                {
                    PlaceMovingObject();
                }
                else
                {
                    canMove = false;
                    OnObjectCantPlacedPosition();
                }
            }
        }
    }

    void OnObjectCantPlacedPosition()
    {
        UIManager.instance.SetFunctionalButtonsActivness(false);
        UIManager.instance.ControlPlacementAlertActiveness(true);
        UIManager.instance.DeactivateUndoButton();
    }
    void PlaceMovingObject()
    {
        movementFinished = true;
        canMove = false;
        Parent.instance.state = ParentState.Free;
        UIManager.instance.SetFunctionalButtonsActivness(true);
    }
}
