using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : Singleton<SelectManager>
{
    public List<GameObject> selectedUnits;
    public List<GameObject> allUnits;
    public delegate void OnSelectUnit(bool activeness);
    public OnSelectUnit onSelectUnit;
    
    public void SelectUnit(GameObject unit)
    {
        if (unit != null)
        {
            selectedUnits.Add(unit);
        }
        if(!UIManager.instance.functionalPanel.activeSelf)
        {
            onSelectUnit?.Invoke(true);
        }
    }

    public void DeSelectUnit(GameObject unit)
    {
        if(unit != null)
        {
            selectedUnits.Remove(unit);
        }
        if(selectedUnits.Count <= 0)
        {
            onSelectUnit?.Invoke(false);
        }
        
    }
    public void DeselectAllUnits()
    {
        selectedUnits.Clear();
    }

    
}
