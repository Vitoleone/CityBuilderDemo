using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCommand : ICommand
{
    RotatingCommander _rotating;
    List<Vector3> prevRotations = new List<Vector3>();
    public RotatingCommand(RotatingCommander rotating)
    {
        _rotating = rotating;
    }
    public void Execute()
    {
        prevRotations.Add(_rotating.transform.eulerAngles);
        _rotating.RotateBuilding();
    }

    public void Undo()
    {
        if (prevRotations.Count > 0)
        {
            Debug.Log(prevRotations[prevRotations.Count - 1]);
            Parent.instance.state = ParentState.Rotating;
            Vector3 prevRotation = prevRotations[prevRotations.Count-1];
            prevRotations.RemoveAt(prevRotations.Count - 1);
            _rotating.transform.eulerAngles = (new Vector3(0, prevRotation.y, 0));
            Parent.instance.AssignAndCheckChildPlacementValues();
        }
    }
}
