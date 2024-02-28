using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectManager : Singleton<SelectManager>
{
    public List<Building> selectedUnits;
    bool canScale= true,canMove = true,canRotate = true;
  

    private void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Parent.instance.state == Parent.ParentState.Free)//Casts ray if mouse is not on a UI element
            {
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if(hitInfo.collider.gameObject.TryGetComponent<SelectableObject>(out SelectableObject selectedObject))
                    {
                        selectedObject.Select();
                        ControlButtons();
                    }
                    else if(hitInfo.collider.gameObject.GetComponent<Terrain>() && CanAllBuild())
                    {
                        DeselectAllUnits();
                        CommandScheduler.ResetStacks();
                        UIManager.instance.SetFunctionalButtonsActivness(false);
                        Parent.instance.state = Parent.ParentState.Free;
                    }

                }
            }
        }
    }
    public void ControlButtons()
    {
        canMove = true; canRotate = true; canScale = true;
        foreach (Building building in selectedUnits)
        {
            if(!building.TryGetComponent(out MovableObject movable))
            {
                canMove = false;
            }
            if (!building.TryGetComponent(out ScaleableObject scaleable))
            {
                canScale = false;
            }
            if (!building.TryGetComponent(out RotatableObject rotateatble))
            {
                canRotate = false;
            }
        }
        UIManager.instance.scaleDownButton.SetActive(canScale);
        UIManager.instance.scaleUpButton.SetActive(canScale);
        UIManager.instance.rotationLeftButton.SetActive(canRotate);
        UIManager.instance.rotationRightButton.SetActive(canRotate);
        UIManager.instance.movementButton.SetActive(canMove);
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
