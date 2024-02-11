using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCommand : ICommand
{
    Scale _scale;
    public ScaleCommand(Scale rotating)
    {
        _scale = rotating;
    }
    public void Execute()
    {
        _scale.ScaleBuilding();
    }

    public void Redo()
    {
        _scale.Redo();
    }

    public void Undo()
    {
        _scale.Undo();
    }
}
