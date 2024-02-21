using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public bool canMove, movementFinished;
    public int layerNumber = 6;
    public int layerMask;

    private void Start()
    {
        layerMask = 1 << layerNumber;
    }
    public Vector3 Move()
    {
        if(canMove)
        {

            movementFinished = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            ControlChildPlacement();
            SelectManager.instance.canMovementFinish = false;
            if (Physics.Raycast(ray, out hitInfo,Mathf.Infinity,layerMask)) //layer kullan
            {  
                 transform.position = new Vector3(hitInfo.point.x, transform.localScale.y / 2, hitInfo.point.z);

                if (Input.GetMouseButtonDown(0) && SelectManager.instance.AllCanBuild())
                {
                    movementFinished = true;
                    canMove = false;
                    return transform.position;
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
    public void ControlChildPlacement()
    {
        foreach(Building childBuilding in GetComponentsInChildren<Building>())
        {
            childBuilding.placementCircle.gameObject.SetActive(true);
            childBuilding.CheckCanBuild();
            Debug.Log(childBuilding.gameObject.name + " canPlaced: " + childBuilding.canBuild);
        }
    }
    private void Update()
    {
        Move();
    }
}
