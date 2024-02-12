using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public void UndoAllSelectedCommands()
    {
        if (SelectManager.instance.selectedUnits.Count > 0 && CommandScheduler._undoCommands.Count > 0)
        {
            foreach (GameObject building in SelectManager.instance.selectedUnits)
            {
                CommandScheduler.UndoCommand();
            }
        }
        else if(SelectManager.instance.allUnits.Count > 0)
        {
            CommandScheduler.UndoCommand();
        }
    }
    public void RedoAllSelectedCommands()
    {
        if (SelectManager.instance.selectedUnits.Count > 0 && CommandScheduler._redoCommands.Count > 0)
        {
            foreach (GameObject building in SelectManager.instance.selectedUnits)
            {
                CommandScheduler.RedoCommand();
            }
        }
        else if (SelectManager.instance.allUnits.Count > 0)
        {
            CommandScheduler.RedoCommand();
        }
    }

    public void RotateSelectedBuildings(float rotateAmount)
    {
        if (SelectManager.instance.selectedUnits.Count > 0)
        {
            foreach (GameObject building in SelectManager.instance.selectedUnits)
            {
                Rotating currentBuilding = building.GetComponent<Rotating>();
                currentBuilding.rotateAmount = rotateAmount;
                CommandScheduler.RunRotatingCommand(building.GetComponent<Rotating>());
            }
        }
    }
    public void ScaleSelectedBuildings(float scaleAmount)
    {
        if (SelectManager.instance.selectedUnits.Count > 0)
        {
            foreach (GameObject building in SelectManager.instance.selectedUnits)
            {
                Scale currentBuilding = building.GetComponent<Scale>();
                currentBuilding.scaleAmount = scaleAmount;
                CommandScheduler.RunScaleCommand(building.GetComponent<Scale>());
            }
        }
    }
}
