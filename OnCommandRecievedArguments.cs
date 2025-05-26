using Godot;
using System;

namespace MConsole;

public class OnCommandReceivedArguments : EventArgs
{
    public readonly string command;
    public readonly string[] arguments;

    public OnCommandReceivedArguments(string cmd, string[]args)
    {
        command =cmd;
        arguments = args;
    }
}// EOF CLASS