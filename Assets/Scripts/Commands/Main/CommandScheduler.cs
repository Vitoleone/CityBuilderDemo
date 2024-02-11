using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandScheduler : MonoBehaviour
{
    private static Stack<ICommand> _undoCommands = new Stack<ICommand>();
    private static Stack<ICommand> _redoCommands = new Stack<ICommand>();
    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoCommands.Push(command);
        _redoCommands.Clear();
    }
    public static void UndoCommand()
    {
        if (_undoCommands.Count <= 0)
            return;
        ICommand command = _undoCommands.Pop();
        _redoCommands.Push(command);
        command.Undo();
    }

    public static void RedoCommand()
    {
        if (_redoCommands.Count <= 0)
            return;
        ICommand command = _redoCommands.Pop();
        _undoCommands.Push(command);
        command.Redo();
    }
    public static void RunBuildingCommand(Building buildingToRun)
    {
        if (buildingToRun == null)
        {
            return;
        }

        ICommand command = new BuildCommand(buildingToRun);
        ExecuteCommand(command);
    }
    public static void RunRotatingCommand(Rotating rotate)
    {
        if (rotate == null)
        {
            return;
        }
        ICommand command = new RotatingCommand(rotate);
        ExecuteCommand(command);
    }
    public static void RunMoveCommand(Moving move)
    {
        if (move == null)
        {
            return;
        }
        ICommand command = new MoveCommand(move);
        ExecuteCommand(command);
    }
}
