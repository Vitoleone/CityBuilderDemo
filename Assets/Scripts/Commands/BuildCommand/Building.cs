using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
    [SerializeField] GameObject selectedCircle;
    private void Start()
    {
        SelectManager.instance.allUnits.Add(gameObject);
    }
    private void OnMouseDown()
    {
        if (!SelectManager.instance.selectedUnits.Contains(gameObject))
        {
            SelectManager.instance.SelectUnit(gameObject);
            selectedCircle.SetActive(true);
        }
        else
        {
            SelectManager.instance.DeSelectUnit(gameObject);
            selectedCircle.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (SelectManager.instance.selectedUnits.Contains(gameObject))
        {
            SelectManager.instance.DeSelectUnit(gameObject);
            selectedCircle.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (SelectManager.instance.selectedUnits.Contains(gameObject))
        {
            SelectManager.instance.DeSelectUnit(gameObject);
            selectedCircle.SetActive(false);
        }
    }

}
