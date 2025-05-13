using System;

namespace MConsole;

public class Command
{
    /// <summary>
    /// Pass in the path/class where the command is getting registered from
    /// </summary>
    /// <param name="sourceNote"></param>
    /// <param name="argumentCount">0 for no arguments, -1 for unlimited</param>
    public Command(string sourceNote, int argumentCount=0){
        source = sourceNote;
        argCount = argumentCount;
    }

    private string source;
    /// <summary>
    /// Dev note to know where the command was registered from pass in with constructor.
    /// Suggest using class name
    /// </summary>
    internal string Source => source;
    /// <summary>
    /// Name of the command and also the command
    /// </summary>
    internal string Name;
    /// <summary>
    /// Description of command 
    /// </summary>
    internal string Tip;
    /// <summary>
    /// Text returned when command failed
    /// </summary>
    internal string Help { get => help.Length == 0 ? Tip : help; set => help = value; }
    internal string help;
    
    /// <summary>
    /// Defines the Types the parameters needs to be for the command
    /// </summary>
    internal Type[] args;
    /// <summary>
    /// The action the command runs. It returns what should be output in the console
    /// </summary>
    internal Func<string[], string> act;
    private int argCount;
    internal int ArgCount => argCount;
}// EOF CLASS