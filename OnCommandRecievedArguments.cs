using Godot;
using System;

namespace MConsole;

public class OnCommandRecievedArguments : EventArgs
{
    public readonly string command;
    public readonly string[] arguments;

    public OnCommandRecievedArguments(string cmd, string[]args)
    {
        command =cmd;
        arguments = args;
    }
}// EOF CLASS