using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCommand : ICommand
{
    Build _building;
    public BuildCommand(Build building)
    {
        _building = building;
    }
    public void Execute()
    {
        _building.BuildExecute();
    }

    public void Undo()
    {
        _building.Undo();
    }
    public void Redo()
    {
        _building.Redo();
    }

   
}
