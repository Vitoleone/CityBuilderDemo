using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public bool canMove, movementFinished;
    float xOffsetValue = -1;//kalkacak, tüm seçili nesnelerin ortasýndan hareket ettirecez
    public Vector3 Move()
    {
        if(canMove)
        {
            if(xOffsetValue < 0)
            {
                xOffsetValue = SelectManager.instance.xOffset;
                SelectManager.instance.xOffset += 40;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {       
                transform.position = new Vector3(hitInfo.point.x + xOffsetValue, transform.localScale.y / 2, hitInfo.point.z);
                
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
