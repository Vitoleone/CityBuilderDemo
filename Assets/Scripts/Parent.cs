using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : Singleton<Parent>
{
    public enum ParentState
    {
        Free,
        Building,
        Scaling,
        Rotating,
        Moving,
        Error
    }
    public ParentState state = ParentState.Free;

    public void ControlChildPlacement()
    {
        foreach (Building childBuilding in GetComponentsInChildren<Building>())
        {
            childBuilding.placementCircle.gameObject.SetActive(true);
            childBuilding.CheckCanBuild();
        }
    }
    public Vector3 GetMidpoint()
    {
        Vector3 midpoint = Vector2.zero;

        foreach (Building unit in SelectManager.instance.selectedUnits)
        {
            if (midpoint != Vector3.zero)
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
    public void ChildSelectedUnits()
    {
        foreach (Building child in SelectManager.instance.selectedUnits)
        {
            child.transform.SetParent(transform);
        }
    }
    public void DetachChild(GameObject child)
    {
        child.transform.SetParent(null);
    }
    public void ClearChilds()
    {
        foreach (Building child in SelectManager.instance.selectedUnits)
        {
            child.transform.SetParent(null);
        }
    }
    public void CheckPlacable()
    {
        ControlChildPlacement();
        if (!SelectManager.instance.CanAllBuild())
        {
            UIManager.instance.SetFunctionalButtonsActivness(false);
            UIManager.instance.ShowOnlyRotationAndScaleButtons(true);
        }
        else
        {
            UIManager.instance.SetFunctionalButtonsActivness(true);
        }
    }
}
