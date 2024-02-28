using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCommand : ICommand
{
    ScaleCommander _scale;
    public float scaleAmount;
    public ScaleCommand(ScaleCommander scaling)
    {
        _scale = scaling;
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
