using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectManager : Singleton<SelectManager>
{
    public List<Building> selectedUnits;
  

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
                    if(hitInfo.collider.gameObject.TryGetComponent<SelectableObject>(out SelectableObject selectedObject) && selectedObject.building.isBuilded && Parent.instance.state == Parent.ParentState.Free)
                    {
                        selectedObject.Select();
                    }
                    else if(hitInfo.collider.gameObject.GetComponent<Terrain>() && CanAllBuild() && Parent.instance.state == Parent.ParentState.Free)
                    {
                        DeselectAllUnits();
                        CommandScheduler.ResetStacks();
                        UIManager.instance.SetFunctionalButtonsActivness(false);

                    }

                }
            }
        }
    }
   

    public void DeSelectUnit(Building unit)
    {
        if (unit != null)
        {
            selectedUnits.Remove(unit);
            unit.placementCircle.gameObject.SetActive(false);
        }
        CommandScheduler.ResetStacks();
    }
    public void DeselectAllUnits()
    {
        foreach (Building unit in selectedUnits)
        {
            unit.placementCircle.gameObject.SetActive(false);
        }
        Parent.instance.ClearChilds();
        selectedUnits.Clear();
        UIManager.instance.checkButtonsActiveness?.Invoke();
        UIManager.instance.buildings.SetActive(true);
        Parent.instance.state = Parent.ParentState.Free;
        Debug.Log("Gridi");
    }

  
    public bool CanAllBuild()
    {
        foreach(Building building in selectedUnits)
        {
            if (!building.canBuild)
            {
                return false;
            }
                
        }
        return true;
    }


}
