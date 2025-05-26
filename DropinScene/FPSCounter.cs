using Godot;
using System;
namespace MConsole;
public partial class FPSCounter : Label
{
    public override void _Ready()
    {
        ConsoleCommands.OnCommandReceived += WhenCommandReceived;
        Hide();
        ProcessMode = ProcessModeEnum.Disabled;
    }
    public override void _Process(double delta)
    {
        Text = Engine.GetFramesPerSecond().ToString();
    }
    private void WhenCommandReceived(object sender, OnCommandReceivedArguments e)
    {
        if (e.command == "showfps")
        {
            if (Visible)
            {
                Hide();
                ProcessMode = ProcessModeEnum.Disabled;
            }
            else
            {
                Text = Engine.GetFramesPerSecond().ToString();
                Show();
                ProcessMode = ProcessModeEnum.Inherit;
            }
        }
    }
}// EOF CLASS
