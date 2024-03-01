using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    MovingCommander _building;
    List<Vector3> prevPosition = new List<Vector3>();
    public MoveCommand(MovingCommander building) 
    {
        _building = building;
    }
    public void Execute()
    {
        prevPosition.Add(_building.transform.position);
        _building.Move();
    }

    public void Undo()
    {
        if(prevPosition.Count > 0)
        {
            Parent.instance.state = ParentState.Moving;
            _building.transform.position = prevPosition[prevPosition.Count - 1];
            prevPosition.RemoveAt(prevPosition.Count - 1);
            Parent.instance.AssignPlacementValueOnAllChilds();
            if (Parent.instance.CheckAllChildsCanBePlaced())
            {
                Parent.instance.state = ParentState.Free;
            }
        }
    }

    
   
}
