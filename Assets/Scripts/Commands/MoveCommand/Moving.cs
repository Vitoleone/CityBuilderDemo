using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public bool canMove, movementFinished;
    public Vector3 Move()
    {
        if(canMove)
        {
            movementFinished = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {       
                transform.position = new Vector3(hitInfo.point.x, transform.localScale.y / 2, hitInfo.point.z);
                
                if (Input.GetMouseButtonDown(0))
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
            return transform.position;
        }
        
        return transform.position;
    }

    private void Update()
    {
        Move();
    }
}
