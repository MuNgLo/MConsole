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
    public string Source => source;
    /// <summary>
    /// Name of the command and also the command
    /// </summary>
    public string Name;
    /// <summary>
    /// Description of command 
    /// </summary>
    public string Tip;
    /// <summary>
    /// Text returned when command failed. If help isnt set it will use the Tip text
    /// </summary>
    internal string Help { get => help.Length < 2 ? Tip : help; set => help = value; }
    private string help = string.Empty;
    
    /// <summary>
    /// Defines the Types the parameters needs to be for the command
    /// </summary>
    public Type[] args;
    /// <summary>
    /// The action the command runs. It returns what should be output in the console
    /// </summary>
    public Func<string[], string> act;
    private int argCount;
    public int ArgCount => argCount;
}// EOF CLASS