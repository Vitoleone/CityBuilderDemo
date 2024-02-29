using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ParentState
{
    Free,
    Building,
    Scaling,
    Rotating,
    Moving,
    Error
}
public class Parent : Singleton<Parent>
{
    public ParentState state = ParentState.Free;

    public void AssignAndCheckChildPlacementValues()
    {
        AssignPlacementValueOnAllChilds();
        CheckAllChildsCanBePlaced();
    }
    public void AssignPlacementValueOnAllChilds()
    {
        foreach (Building childBuilding in GetComponentsInChildren<Building>())
        {
            childBuilding.placementCircle.gameObject.SetActive(true);
            childBuilding.CheckCanBuild();
        }
    }
    public bool CheckAllChildsCanBePlaced()
    {
        foreach (Building building in SelectManager.instance.selectedUnits)
        {
            if (!building.canBuild)
            {
                return false;
            }

        }
        //If all children can be placed then change parent state to free for click
        Parent.instance.state = ParentState.Free;
        return true;
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
    public void ClearAllChilds()
    {
        foreach (Building child in SelectManager.instance.selectedUnits)
        {
            child.transform.SetParent(null);
        }
    }
 
}
