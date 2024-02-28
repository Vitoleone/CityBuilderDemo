using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    MovingCommander _building;
    Vector3 startPosition;
    Vector3 endPosition;
    public MoveCommand(MovingCommander building) 
    {
        _building = building;
        startPosition = building.transform.position;
    }
    public void Execute()
    {
        endPosition = _building.Move();
    }

    public void Redo()
    {
        _building.transform.position = endPosition;
    }

    public void Undo()
    {
        endPosition = _building.transform.position;
        _building.transform.position = startPosition;
    }

    
   
}
