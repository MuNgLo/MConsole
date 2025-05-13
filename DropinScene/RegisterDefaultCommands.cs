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
    [Export] private bool showfps = true;

    public override void _Ready()
    {
        if(quitExitapp){ AddExitQuitCommand(); }
        if(maxfps){ AddMaxFPSCommand(); }
        if(showfps){ ShowFPSCommand(); }
    }

    private void ShowFPSCommand()
    {
        Command cmd = new Command("RegisterDefaultCommands"){ Name = "showfps", Tip = "show/hide frames per second counter", act = (a) => {return "";} };
        ConsoleCommands.RegisterCommand(cmd);
    }

    private void AddMaxFPSCommand()
    {
        Command cmd = new Command("RegisterDefaultCommands", 1){ 
            Name = "maxfps", 
            Tip = "Set maximum frames per second",
            Help = "Example: maxfps 60", 
             act = (a) => {SetMaxFPS(a[1]); return "";} };
        ConsoleCommands.RegisterCommand(cmd);
    }

    private void SetMaxFPS(string v)
    {
        if(int.TryParse(v, out int i)){Engine.MaxFps = Math.Clamp(i, 0, 1000); }
    }

    private void AddExitQuitCommand()
    {
        Command cmd1 = new Command("RegisterDefaultCommands"){ Name = "exit", Tip = "Closes application", act = (a) => {GetTree().Quit(); return "";} };
        Command cmd2 = new Command("RegisterDefaultCommands"){ Name = "quit", Tip = "Closes application", act = (a) => {GetTree().Quit(); return "";} };
        ConsoleCommands.RegisterCommand(cmd1);
        ConsoleCommands.RegisterCommand(cmd2);
    }
}// EOF CLASS

