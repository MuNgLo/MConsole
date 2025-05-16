# MConsole

v1.2

Dropin in-game developer console for Godot

[![Example](https://github.com/MuNgLo/MConsole/blob/main/GitHubMedia/MConsole010001-0535.mp4)()]

<video src='https://github.com/MuNgLo/MConsole/blob/main/GitHubMedia/MConsole010001-0535.mp4' with=720>


1.2 adding video and use instructions
1.1 history works, fpscounter, defaultcommands node added

# Install

place all repo files in /addons/MConsole

drop the addons/MConsole/DropinScene/GameConsole.tscn where you want it.

Also add the FPSCounter.tscn if you want

Note that there should ever only be one COnsoleCommand Node and it comes in that Console scene so depending on how your project
handles scenes it might need to be broken apart and tweaked.

# How to use

After you followed the install instruction. Have the GameConsole in the scene and maybe even the FPS counter. Start registering your own commands
For a clear example look to the RegisterDefaultCommands Class. It comes down to creating an instance of Command class and pass it to the static ConsoleCommands.RegisterCommand.

Sometimes you want a commnand to act as a trigger for a change on things. THen look at the FPSCOunter as it makes use of the ConsoleCommands.OnCommandRecieved EventHandler to
react when showfps is passed as command.

To toggle the console setup some input like an action and call the GameConsole.Toggle();
```cs
        if(Input.IsActionJustPressed("ToggleConsole")){ MConsole.GameConsole.Toggle(); }
```


# ToDo
autocomplete, command list, command search
Type check parameters according to Command definition using the Command.args;
