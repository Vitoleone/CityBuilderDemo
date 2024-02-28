using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCommand : ICommand
{
    BuildCommander _building;
    public BuildCommand(BuildCommander building)
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
   

   
}
