using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Moving : MonoBehaviour
{
    public bool canMove, movementFinished = false;
    public int layerNumber = 6;
    public int layerMask;
    Parent parent;

    private void Start()
    {
        layerMask = 1 << layerNumber;
        parent = GetComponent<Parent>();
    }
    public Vector3 Move()
    {
        if(canMove)
        {
            if (parent.state == Parent.ParentState.Free)
            {
                CommandScheduler.ResetStacks();
            }
            parent.state = Parent.ParentState.Moving;
            movementFinished = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            parent.ControlChildPlacement();
            SelectManager.instance.canMovementFinish = false;
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
                {
                    transform.position = new Vector3(hitInfo.point.x, transform.localScale.y / 2, hitInfo.point.z);

                    if (Input.GetMouseButtonDown(0) && SelectManager.instance.CanAllBuild())
                    {
                        movementFinished = true;
                        canMove = false;
                        parent.state = Parent.ParentState.Free;
                        UIManager.instance.SetFunctionalButtonsActivness(true);
                        return transform.position;
                    }
                    else if(Input.GetMouseButtonDown(0) && !SelectManager.instance.CanAllBuild())
                    {
                        //hata ekraný popup olacak, canmove false olacak ve buttonlar görünmez hale gelecek.
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
            SelectManager.instance.canMovementFinish = true;
            return transform.position;
        }
        
        return transform.position;
    }
    private void Update()
    {
        Move();
    }
}
