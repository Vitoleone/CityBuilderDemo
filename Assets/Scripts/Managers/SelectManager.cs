using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectManager : Singleton<SelectManager>
{
    public List<Building> selectedUnits;
    public delegate void OnSelectUnit(bool activeness);
    public OnSelectUnit onSelectUnit;
    public float xOffset = 0;
    public bool canMovementFinish;
    public Parent parent;
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
                    if(hitInfo.collider.gameObject.TryGetComponent<Building>(out Building building) && building.isBuilded && parent.state == Parent.ParentState.Free)
                    {
                        SelectUnit(building);
                    }
                    else if(hitInfo.collider.gameObject.GetComponent<Terrain>() && parent.state == Parent.ParentState.Free)
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
            UIManager.instance.SetFunctionalButtonsActivness(true);
            UIManager.instance.buildings.SetActive(false);
        }
        if (!UIManager.instance.allFunctionalActiveness)
        {
            onSelectUnit?.Invoke(true);
        }
        CommandScheduler.ResetStacks();
    }

    public void DeSelectUnit(Building unit)
    {
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
        parent.ClearChilds();
    }
    public void DeselectAllUnits()
    {
        foreach (Building unit in selectedUnits)
        {
            unit.selectedCircle.SetActive(false);
            unit.placementCircle.gameObject.SetActive(false);
        }
        parent.ClearChilds();
        selectedUnits.Clear();
        UIManager.instance.checkButtonsActiveness?.Invoke();
        xOffset = -1;
        UIManager.instance.buildings.SetActive(true);
    }

  
    public bool AllCanBuild()
    {
        foreach(Building building in selectedUnits)
        {
            if (!building.canBuild)
            {
                UIManager.instance.SetFunctionalButtonsActivness(false);
                return false;
            }
                
        }
        return true;
    }


}
