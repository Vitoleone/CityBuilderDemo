using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    Moving _building;
    public MoveCommand(Moving building) 
    {
        _building = building;
    }
    public void Execute()
    {
        _building.Move();   
    }

    public void Redo()
    {
        _building.Redo();
    }

    public void Undo()
    {
        _building.Undo();
    }

   
}
