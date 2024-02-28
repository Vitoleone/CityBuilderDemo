using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCommand : ICommand
{
    RotatingCommander _rotating;
    public RotatingCommand(RotatingCommander rotating)
    {
        _rotating = rotating;
    }
    public void Execute()
    {
        _rotating.RotateBuilding();
    }

    public void Undo()
    {
        _rotating.Undo();
    }
}
