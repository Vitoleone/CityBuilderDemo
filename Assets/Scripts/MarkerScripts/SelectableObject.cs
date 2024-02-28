using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Building))]
public class SelectableObject : MonoBehaviour
{
    public UnityEvent OnSelected;
    public UnityEvent OnDeselect;
    public Building building;
    public bool isSelected;
    private void Awake()
    {
        building = GetComponent<Building>();
    }

    public void Select()
    {
        if(SelectManager.instance.selectedUnits.Count <= 0)
            CommandScheduler.ResetStacks();

        if (!SelectManager.instance.selectedUnits.Contains(building))
        {
            SelectManager.instance.selectedUnits.Add(building);
            isSelected = true;
            OnSelected?.Invoke();
        }
    }
    public void Deselect()
    {
        SelectManager.instance.selectedUnits.Remove(building);
        isSelected = false;
        Parent.instance.DetachChild(gameObject);
        OnDeselect?.Invoke();
    }

}
