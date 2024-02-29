using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{  
    public void UndoAllSelectedCommands()
    {
        CommandScheduler.UndoCommand();
        
    }
    public void MoveAllSelectedBuildings(bool canMove)
    {

        AssignParentAndMidpoint();
        MovingCommander parentMoving = Parent.instance.GetComponent<MovingCommander>();
        parentMoving.canMove = canMove;
        CommandScheduler.RunMoveCommand(parentMoving);
    }

    public void RotateSelectedBuildings(float rotateAmount)
    {

        AssignParentAndMidpoint();
        RotatingCommander parentRotating = Parent.instance.GetComponent<RotatingCommander>();
        parentRotating.rotateAmount = rotateAmount;
        CommandScheduler.RunRotatingCommand(Parent.instance.GetComponent<RotatingCommander>());
    }
    public void ScaleSelectedBuildings(float scaleAmount)
    {

        AssignParentAndMidpoint();
        ScaleCommander parentScaling = Parent.instance.GetComponent<ScaleCommander>();
        parentScaling.scaleAmount = scaleAmount;
        CommandScheduler.RunScaleCommand(Parent.instance.GetComponent<ScaleCommander>());
    }
    private void AssignParentAndMidpoint()
    {
        Vector3 midpoint = Parent.instance.GetMidpoint();
        Parent.instance.transform.position = midpoint;
        Parent.instance.ChildSelectedUnits();
        
    }

    public void PlacementAlertButton()
    {
        MovingCommander parentMoving = Parent.instance.GetComponent<MovingCommander>();
        ScaleCommander parentScale = Parent.instance.GetComponent<ScaleCommander>();
        RotatingCommander parentRotate = Parent.instance.GetComponent<RotatingCommander>();
        switch(Parent.instance.state)
        {
            case ParentState.Building:
                EventManager.instance.OnBuildEnded?.Invoke();
                break;
            case ParentState.Scaling:
                break;
            case ParentState.Moving:
                parentMoving.canMove = true;
                UIManager.instance.SetFunctionalButtonsActivness(true);
                break;
            case ParentState.Rotating:
                UIManager.instance.SetFunctionalButtonsActivness(true);
                Parent.instance.state = ParentState.Rotating;
                break;
            default:
                Parent.instance.state = ParentState.Free;
                break;
        }
        UIManager.instance.ControlPlacementAlertActiveness(false);
    }
}
