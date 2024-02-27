using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject parentObject;
    Parent parent;

    public int availableObjects = 0;
    private void Start()
    {
        parent = parentObject.GetComponent<Parent>();
    }
    public void UndoAllSelectedCommands()
    {
        CommandScheduler.UndoCommand();
        
    }
    public void RedoAllSelectedCommands()
    {
        CommandScheduler.RedoCommand();
    }
    public void MoveAllSelectedBuildings(bool canMove)
    {

        AssignParentAndMidpoint();
        Moving parentMoving = parentObject.GetComponent<Moving>();
        parentMoving.canMove = canMove;
        CommandScheduler.RunMoveCommand(parentMoving);
    }

    public void RotateSelectedBuildings(float rotateAmount)
    {

        AssignParentAndMidpoint();
        Rotating parentRotating = parentObject.GetComponent<Rotating>();
        parentRotating.rotateAmount = rotateAmount;
        CommandScheduler.RunRotatingCommand(parentObject.GetComponent<Rotating>());
    }
    public void ScaleSelectedBuildings(float scaleAmount)
    {

        AssignParentAndMidpoint();
        Scale parentScaling = parentObject.GetComponent<Scale>();
        parentScaling.scaleAmount = scaleAmount;
        CommandScheduler.RunScaleCommand(parentObject.GetComponent<Scale>());
    }
    private void AssignParentAndMidpoint()
    {
        Vector3 midpoint = parent.GetMidpoint();
        parentObject.transform.position = midpoint;
        parent.ChildSelectedUnits();
    }

    public void PlacementAlertButton()
    {
        Moving parentMoving = parentObject.GetComponent<Moving>();
        Scale parentScale = parentObject.GetComponent<Scale>();
        Rotating parentRotate = parentObject.GetComponent<Rotating>();
        switch(parent.state)
        {
            case Parent.ParentState.Building:
                EventManager.instance.OnBuildEnded?.Invoke();
                break;
            case Parent.ParentState.Scaling:
                break;
            case Parent.ParentState.Moving:
                parentMoving.canMove = true;
                UIManager.instance.SetFunctionalButtonsActivness(true);
                break;
            case Parent.ParentState.Rotating:
                UIManager.instance.SetFunctionalButtonsActivness(true);
                parent.state = Parent.ParentState.Rotating;
                break;
            default:
                break;
        }
        UIManager.instance.ControlPlacementAlertActiveness(false);
    }
}
