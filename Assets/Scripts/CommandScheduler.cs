using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandScheduler : MonoBehaviour
{
    public static Stack<ICommand> commands = new Stack<ICommand>();

    public static void ResetStacks()
    {
        commands.Clear();
        UIManager.instance.checkButtonsActiveness?.Invoke();
    }
    public static void ExecuteCommand(ICommand command)
    {
        commands.Push(command);
        command.Execute();
        UIManager.instance.checkButtonsActiveness?.Invoke();
    }
    public static void UndoCommand()
    {
        if (commands.Count <= 0)
            return;
        ICommand command = commands.Pop();
        command.Undo();
        UIManager.instance.checkButtonsActiveness?.Invoke();
    }
    public static void RunBuildingCommand(BuildCommander buildingToRun)
    {
        if (buildingToRun == null)
        {
            return;
        }

        ICommand command = new BuildCommand(buildingToRun);
        ExecuteCommand(command);
    }
    public static void RunRotatingCommand(RotatingCommander rotate)
    {
        if (rotate == null)
        {
            return;
        }
        ICommand command = new RotatingCommand(rotate);
        ExecuteCommand(command);
    }
    public static void RunMoveCommand(MovingCommander move)
    {
        if (move == null)
        {
            return;
        }
        ICommand command = new MoveCommand(move);
        ExecuteCommand(command);
    }
    public static void RunScaleCommand(ScaleCommander scale)
    {
        if (scale == null)
        {
            return;
        }
        ICommand command = new ScaleCommand(scale);
        ExecuteCommand(command);
    }
}
