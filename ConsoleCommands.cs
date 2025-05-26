using System;
using System.Collections.Generic;
using Godot;

namespace MConsole;
/// <summary>
/// Main class of the consolesystem. Make an instance of this where you think it fits into the project.
/// Then make an instance under it and expose it through a property like
/// Then use "ConsoleCommands.RegisterCommand" to register in your commands from _Ready or later
/// allowing this to have been setup first
/// </summary>
[GlobalClass]
public partial class ConsoleCommands : Node
{
    private static ConsoleCommands ins;
    public static EventHandler<OnCommandReceivedArguments> OnCommandReceived;
    private Dictionary<string, Command> commands;
    private int historyLength = 20;
    private int historyIndex = 0;
    private List<string> history;

    public override void _EnterTree()
    {
        if (ins is not null) { GD.PrintErr($"ConsoleCommands Singleton pattern cant be done. Instance already assigned. Do you have 2 ConsoleCommands Nodes?"); return; }
        ins = this;
        commands = new Dictionary<string, Command>();
        history = new List<string>();
        GameConsole.OnConsoleInputChanged += WhenConsoleInputChange;
        GameConsole.OnConsoleInputSubmitted += WhenConsoleInputSubmitted;
        AddDefaultCommands();
    }

    private void AddDefaultCommands()
    {
        // Source check a command
        RegisterCommand(new Command("ConsoleSystem Default")
        {
            Name = "cmd_source",
            Tip = "Check the source of a registered command",
            act = s => { return "Registered from: " + GetCommandSource(s[1]); },
            args = [typeof(string), typeof(string)]
        });
    }

    private string GetCommandSource(string cmdName)
    {
        if (commands.ContainsKey(cmdName))
        {
            return commands[cmdName].Source;
        }
        return $"The command \"{cmdName}\" isn't registered.";
    }

    /// <summary>
    /// When input is sent to console this is where it gets processed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="incomming"></param>
    private void WhenConsoleInputSubmitted(object sender, string incomming)
    {
        // add to history
        history.Add(incomming);
        // Clamp history
        if (history.Count > 20) { history.RemoveAt(0); }
        // Set history back to present
        historyIndex = history.Count;
        // Split the incomming string
        string[] args = incomming.Trim().Split(' ');
        // Check if there is a command registered
        if (commands.ContainsKey(args[0]))
        {
            // Get the commmand from the dictionary
            Command cmd = commands[args[0]];

            if (args.Length == 1)
            {
                // Check if the command was given withouth any params when it should. If so give tip
                if (cmd.ArgCount > 0)
                {
                    GameConsole.AddLine(commands[args[0]].Help);
                    return;

                }
                else
                {
                    // Run the command and push return string to console
                    GameConsole.AddLine(cmd.act.Invoke(args));
                    OnCommandReceived?.Invoke(this, new OnCommandReceivedArguments(args[0], args));
                    return;
                }

            }
            else if (args.Length == cmd.ArgCount + 1)
            {
                // Run the command and push return string to console
                GameConsole.AddLine(cmd.act.Invoke(args));
                OnCommandReceived?.Invoke(this, new OnCommandReceivedArguments(args[0], args));
                return;

            }
            GameConsole.AddLine($"Command failed! CMD[{cmd.Name}] ArgCount[{cmd.ArgCount}] args.Length[{args.Length}]");
        }
    }
    /// <summary>
    /// Register a command to the console. Does a check for preexisting command
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public static bool RegisterCommand(Command cmd)
    {
        if (ins.commands.ContainsKey(cmd.Name))
        {
            GameConsole.AddLine($"Registering command \"{cmd.Name}\" failed. It already registered!");
            return false;
        }
        ins.commands[cmd.Name] = cmd;
        return ins.commands.ContainsKey(cmd.Name);
    }
    /// <summary>
    /// Walk history pointer and set input area of console to that stored command
    /// </summary>
    public static void HistoryUp()
    {
        if (ins.history.Count < 1) { return; }
        ins.historyIndex = Math.Clamp(ins.historyIndex - 1, 0, Math.Min(ins.history.Count - 1, ins.historyLength));
        GameConsole.SetInputText(ins.history[ins.historyIndex]);
    }
    /// <summary>
    /// Walk history pointer and set input area of console to that stored command
    /// </summary>
    public static void HistoryDown()
    {
        if (ins.history.Count < 1) { return; }
        if (ins.history.Count - 1 == ins.historyIndex)
        {
            ins.historyIndex = ins.history.Count; GameConsole.ClearInput();
            return;
        }
        ins.historyIndex = Math.Clamp(ins.historyIndex + 1, 0, Math.Min(ins.history.Count - 1, ins.historyLength));
        GameConsole.SetInputText(ins.history[ins.historyIndex]);
    }
    /// <summary>
    /// TODO use this for autocompletion!
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void WhenConsoleInputChange(object sender, string e)
    {
        if (commands.ContainsKey(e))
        {
            GameConsole.SetTip(commands[e].Tip);
        }
        else
        {
            GameConsole.ClearTip();
        }
    }
}// EOF CLASS