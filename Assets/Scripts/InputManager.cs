using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public void UndoCommands()
    {
        BuildingScheduler.UndoCommand();
    }
    public void RedoCommands()
    {
        BuildingScheduler.RedoCommand();
    }

    public void RotateSelectedBuildings()
    {
        if (SelectManager.instance.selectedUnits.Count > 0)
        {
            foreach (GameObject building in SelectManager.instance.selectedUnits)
            {
                BuildingScheduler.RunRotatingCommand(building.GetComponent<Rotating>());
            }
        }
        
    }
}
