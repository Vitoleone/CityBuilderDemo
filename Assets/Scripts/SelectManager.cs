using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : Singleton<SelectManager>
{
    public List<GameObject> selectedUnits;
    
    public void SelectUnit(GameObject unit)
    {
        if (unit != null)
        {
            selectedUnits.Add(unit);
        }
    }

    public void DeSelectUnit(GameObject unit)
    {
        if(unit != null)
        {
            selectedUnits.Remove(unit);
        }
        
    }
    public void DeselectAllUnits()
    {
        selectedUnits.Clear();
    }

    
}
