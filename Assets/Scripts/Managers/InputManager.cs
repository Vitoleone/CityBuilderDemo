using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public void UndoCommands()
    {
        CommandScheduler.UndoCommand();
    }
    public void RedoCommands()
    {
        CommandScheduler.RedoCommand();
    }

    public void RotateSelectedBuildings()
    {
        if (SelectManager.instance.selectedUnits.Count > 0)
        {
            foreach (GameObject building in SelectManager.instance.selectedUnits)
            {
                CommandScheduler.RunRotatingCommand(building.GetComponent<Rotating>());
            }
        }
    }
    public void ScaleSelectedBuildings()
    {
        if (SelectManager.instance.selectedUnits.Count > 0)
        {
            foreach (GameObject building in SelectManager.instance.selectedUnits)
            {
                CommandScheduler.RunScaleCommand(building.GetComponent<Scale>());
            }
        }
    }
}
