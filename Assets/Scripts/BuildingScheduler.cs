using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class BuildingScheduler : MonoBehaviour
{
   private static Stack<ICommand> _undoCommands = new Stack<ICommand>();
   private static Stack<ICommand> _redoCommands = new Stack<ICommand>();
    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoCommands.Push(command);
        _redoCommands.Clear();
        Debug.Log(_undoCommands.Count);
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

}
