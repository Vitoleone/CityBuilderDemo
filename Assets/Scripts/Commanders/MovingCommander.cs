using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingCommander : MonoBehaviour
{
    public bool canMove, movementFinished = false;
    public Vector3 Move()
    {
        if(canMove)
        {
            Parent.instance.state = Parent.ParentState.Moving;
            movementFinished = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Parent.instance.ControlChildPlacement();
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
                {
                    transform.position = new Vector3(hitInfo.point.x, 0.05f, hitInfo.point.z);

                    if (Input.GetMouseButtonDown(0) && SelectManager.instance.CanAllBuild())
                    {
                        movementFinished = true;
                        canMove = false;
                        Parent.instance.state = Parent.ParentState.Free;
                        UIManager.instance.SetFunctionalButtonsActivness(true);
                        return transform.position;
                    }
                    else if(Input.GetMouseButtonDown(0) && !SelectManager.instance.CanAllBuild())
                    {
                        canMove = false;
                        UIManager.instance.SetFunctionalButtonsActivness(false);
                        UIManager.instance.ControlPlacementAlertActiveness(true);
                    }
                }
            }
            
        }
        else if(movementFinished)
        {
            canMove = false;
            return transform.position;
        }
        
        return transform.position;
    }
    private void Update()
    {
        if(canMove)
        {
            Move();
        }
    }
}
