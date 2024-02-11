using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCommand : ICommand
{
    Building _building;
    public BuildCommand(Building building)
    {
        _building = building;
    }
    public void Execute()
    {
        _building.Build();
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
