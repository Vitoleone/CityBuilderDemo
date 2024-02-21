using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField]public GameObject selectedCircle;
    [SerializeField]public Image placementCircle;
    public bool isSelected;
    public bool isBuilded;
    public bool canBuild;
    int layerNumber = 6;
    int layerMask;
    private void Start()
    {
        SelectManager.instance.allUnits.Add(this);
        layerMask = 1 << layerNumber;
    }
    public void CheckCanBuild()
    {
        placementCircle.gameObject.SetActive(true);
        selectedCircle.gameObject.SetActive(false);
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, Vector3.down * 2, out hitInfo))
        {
            float angle = Vector3.Angle(hitInfo.normal, hitInfo.point);
            if(angle == 90)
            {
                canBuild = true;
                placementCircle.color = Color.green;
            }
            else
            {
                canBuild = false;
                placementCircle.color = Color.red;
            }
        }
    }
    private void OnDestroy()
    {
        if (SelectManager.instance.selectedUnits.Contains(this))
        {
            SelectManager.instance.DeSelectUnit(this);
            selectedCircle.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (SelectManager.instance.selectedUnits.Contains(this))
        {
            SelectManager.instance.DeSelectUnit(this);
            selectedCircle.SetActive(false);
        }
    }
}
