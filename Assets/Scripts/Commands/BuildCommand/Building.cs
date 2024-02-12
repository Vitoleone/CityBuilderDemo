using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
    private void Start()
    {
        SelectManager.instance.allUnits.Add(gameObject);
    }
    private void OnMouseDown()
    {
        if (!SelectManager.instance.selectedUnits.Contains(gameObject))
            SelectManager.instance.SelectUnit(gameObject);
        else
        {
            SelectManager.instance.DeSelectUnit(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (SelectManager.instance.selectedUnits.Contains(gameObject))
        {
            SelectManager.instance.DeSelectUnit(gameObject);
        }
    }

    private void OnDisable()
    {
        if (SelectManager.instance.selectedUnits.Contains(gameObject))
        {
            SelectManager.instance.DeSelectUnit(gameObject);
        }
    }

}
