using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]Building building;
    public void BuildBuilding()
    {
        RunBuildingCommand(building);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BuildBuilding();
    }

    public void RunBuildingCommand(Building buildingToRun)
    {
        if (building == null)
        {
            return;
        }

        ICommand command = new BuildCommand(buildingToRun);
        BuildingScheduler.ExecuteCommand(command);
    }
    public void UndoBuildingCommand()
    {
        BuildingScheduler.UndoCommand();
    }
    public void RedoBuildingCommand()
    {
        BuildingScheduler.RedoCommand();
    }
}
