using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject parent;
    public void UndoAllSelectedCommands()
    {
        if (SelectManager.instance.selectedUnits.Count > 0 && CommandScheduler.commands.Count > 0)
        {
            foreach (GameObject building in SelectManager.instance.selectedUnits)
            {
                CommandScheduler.UndoCommand();
            }
        }
        else if (SelectManager.instance.allUnits.Count > 0)
        {
            CommandScheduler.UndoCommand();
        }
    }
    public void RedoAllSelectedCommands()
    {
        if (SelectManager.instance.selectedUnits.Count > 0 && CommandScheduler.commands.Count > 0)
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
    public void MoveAllSelectedBuildings(bool canMove)
    {
        Vector3 midpoint = SelectManager.instance.GetMidpoint();
        parent.transform.position = midpoint;
        SelectManager.instance.ChildSelected(parent);
        Moving parentMoving = parent.GetComponent<Moving>();
        parentMoving.canMove = canMove;
        CommandScheduler.RunMoveCommand(parentMoving);
    }

    public void RotateSelectedBuildings(float rotateAmount)
    {
        Vector3 midpoint = SelectManager.instance.GetMidpoint();
        parent.transform.position = midpoint;
        SelectManager.instance.ChildSelected(parent);
        Rotating parentRotating = parent.GetComponent<Rotating>();
        parentRotating.rotateAmount = rotateAmount;
        CommandScheduler.RunRotatingCommand(parent.GetComponent<Rotating>());
    }
    public void ScaleSelectedBuildings(float scaleAmount)
    {
        Vector3 midpoint = SelectManager.instance.GetMidpoint();
        this.parent.transform.position = midpoint;
        SelectManager.instance.ChildSelected(this.parent);
        Scale parentScaling = parent.GetComponent<Scale>();
        parentScaling.scaleAmount = scaleAmount;
        CommandScheduler.RunScaleCommand(parent.GetComponent<Scale>());
    }
}
