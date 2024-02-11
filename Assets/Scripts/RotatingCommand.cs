using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCommand : ICommand
{
    Rotating _rotating;
    public RotatingCommand(Rotating rotating)
    {
        _rotating = rotating;
    }
    public void Execute()
    {
        _rotating.RotateBuilding();
    }

    public void Redo()
    {
        _rotating.Redo();
    }

    public void Undo()
    {
        _rotating.Undo();
    }
}
