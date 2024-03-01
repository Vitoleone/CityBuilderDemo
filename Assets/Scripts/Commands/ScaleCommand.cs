using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCommand : ICommand
{
    ScaleCommander _scale;
    List<Vector3> prevScaleValues = new List<Vector3>();
    public ScaleCommand(ScaleCommander scaling)
    {
        _scale = scaling;
    }
    public void Execute()
    {
        prevScaleValues.Add(Vector3.one * _scale.scaleAmount);
        _scale.ScaleBuilding();

    }
    public void Undo()
    {
        if (prevScaleValues.Count > 0)
        {
            Parent.instance.state = ParentState.Scaling;
            Vector3 undoVector = prevScaleValues[prevScaleValues.Count - 1];
            prevScaleValues.RemoveAt(prevScaleValues.Count - 1);
            _scale.transform.localScale -= undoVector;
            _scale.transform.position = new Vector3(_scale.transform.position.x, 0.05f, _scale.transform.position.z);
            Parent.instance.AssignPlacementValueOnAllChilds();
            if (Parent.instance.CheckAllChildsCanBePlaced())
            {
                Parent.instance.state = ParentState.Free;
            }
        }
    }
}
