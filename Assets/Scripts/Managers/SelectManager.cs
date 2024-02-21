using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectManager : Singleton<SelectManager>
{
    public List<Building> selectedUnits;
    public List<Building> allUnits;
    public delegate void OnSelectUnit(bool activeness);
    public OnSelectUnit onSelectUnit;
    public float xOffset = 0;
    public bool canMovementFinish;
    private void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())//Casts ray if mouse is not on a UI element
            {
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if(hitInfo.collider.gameObject.TryGetComponent<Building>(out Building building) && building.isBuilded)
                    {
                        SelectUnit(building);
                    }
                    else if(hitInfo.collider.gameObject.GetComponent<Terrain>() && SelectManager.instance.canMovementFinish)
                    {
                        DeselectAllUnits();
                        CommandScheduler.ResetStacks();
                        UIManager.instance.SetFunctionalButtonsActivness(false);

                    }

                }
            }
        }
    }
    public void SelectUnit(Building unit)
    {
        if (unit != null && !selectedUnits.Contains(unit))
        {
            selectedUnits.Add(unit);
            unit.GetComponent<Building>().selectedCircle.SetActive(true);
        }
        if (!UIManager.instance.functionalPanel.activeSelf)
        {
            onSelectUnit?.Invoke(true);
        }
        CommandScheduler.ResetStacks();
    }

    public void DeSelectUnit(Building unit)
    {

        Debug.Log("Deselect");
        if (unit != null)
        {
            selectedUnits.Remove(unit);
            unit.GetComponent<Building>().selectedCircle.SetActive(false);
            unit.GetComponent<Building>().placementCircle.gameObject.SetActive(false);
        }
        if(selectedUnits.Count <= 0)
        {
            onSelectUnit?.Invoke(false);
        }
        CommandScheduler.ResetStacks();
        ClearChilds();
    }
    public void DeselectAllUnits()
    {
        Debug.Log("DeselectAll");
        foreach (Building unit in selectedUnits)
        {
            unit.selectedCircle.SetActive(false);
            unit.placementCircle.gameObject.SetActive(false);
        }
        SelectManager.instance.ClearChilds();
        selectedUnits.Clear();
        UIManager.instance.checkButtonsActiveness?.Invoke();
        xOffset = -1;
    }

    public Vector3 GetMidpoint()
    {
        Vector3 midpoint = Vector2.zero;

        foreach(Building unit in selectedUnits)
        {
            if(midpoint != Vector3.zero)
            {
                midpoint = Vector3.Lerp(unit.transform.position, midpoint, 0.5f);
            }
            else
            {
                midpoint = unit.transform.position;
            }
        }
        return midpoint;
    }
    public void ChildSelected(GameObject parent)
    {
        foreach(Building child in selectedUnits)
        {
            child.transform.SetParent(parent.transform);
        }
    }
    public void ClearChilds()
    {
        foreach(Building child in selectedUnits)
        {
            child.transform.SetParent(null);
        }
    }
    public bool AllCanBuild()
    {
        foreach(Building building in selectedUnits)
        {
            if (!building.canBuild)
                return false;
        }
        return true;
    }


}
