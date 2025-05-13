using System;
using Godot;

namespace MConsole;

/// <summary>
/// A node to register in default commands
/// Each command is optional by bool flag
/// </summary>
[GlobalClass]
public partial class RegisterDefaultCommands : Node
{
    [Export] private bool quitExitapp = true;
    [Export] private bool maxfps = true;

    public override void _Ready()
    {
        if(quitExitapp){ AddExitQuitCommand(); }
    }

    private void AddExitQuitCommand()
    {
        Command cmd1 = new Command("RegisterDefaultCommands"){ Name = "exit", Tip = "Closes application", act = (a) => {GetTree().Quit(); return "";} };
        Command cmd2 = new Command("RegisterDefaultCommands"){ Name = "quit", Tip = "Closes application", act = (a) => {GetTree().Quit(); return "";} };
        ConsoleCommands.RegisterCommand(cmd1);
        ConsoleCommands.RegisterCommand(cmd2);
    }
}// EOF CLASS

