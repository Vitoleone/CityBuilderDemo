using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectManager : Singleton<SelectManager>
{
    public List<Building> selectedUnits;
    bool canScale= true,canMove = true,canRotate = true;

    private void Start()
    {
        EventManager.instance.OnDeselect += OnDeselect;
    }
    private void OnDestroy()
    {
        EventManager.instance.OnDeselect -= OnDeselect;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Selection();
        }
    }
    void Selection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (!EventSystem.current.IsPointerOverGameObject() && Parent.instance.state == ParentState.Free)//Casts ray if mouse is not on a UI element
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                //Select selectable object
                if (hitInfo.collider.gameObject.TryGetComponent<SelectableObject>(out SelectableObject selectedObject))
                {
                    selectedObject.Select();
                    ControlButtons();
                }
                //if ray hits terrain and all buildings can placed then deselect all buildings
                else if (hitInfo.collider.gameObject.GetComponent<Terrain>() && Parent.instance.CheckAllChildsCanBePlaced())
                {
                    DeselectAllBuildings();
                }
            }
        }
    }
    //Checks selected objects markers
    public void ControlButtons()
    {
        canMove = true; canRotate = true; canScale = true;
        foreach (Building building in selectedUnits)
        {
            if (!building.TryGetComponent(out MovableObject movable))
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
        UIManager.instance.ActivateMarkersButtons(canScale,canRotate,canMove);
    }

    public void DeselectBuilding(Building unit)
    {
        if (unit != null)
        {
            selectedUnits.Remove(unit);
            unit.placementCircle.gameObject.SetActive(false);
        }
        CommandScheduler.ResetStacks();
    }
    public void DeselectAllBuildings()
    {
        foreach (Building unit in selectedUnits)
        {
            unit.placementCircle.gameObject.SetActive(false);
        }
        Parent.instance.ClearAllChilds();
        selectedUnits.Clear();
        EventManager.instance.OnDeselect?.Invoke();
       
    }
    private void OnDeselect()
    {
        UIManager.instance.checkButtonsActiveness?.Invoke();
        UIManager.instance.buildings.SetActive(true);
        Parent.instance.state = ParentState.Free;
        CommandScheduler.ResetStacks();
        UIManager.instance.SetFunctionalButtonsActivness(false);
    }
}
