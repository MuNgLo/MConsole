# MConsole

v1.3

Dropin in-game developer console for Godot

![Demo](https://github.com/MuNgLo/MConsole/blob/main/GitHubMedia/MConsole-01.gif)

1.3 command search and listing added, autocompletion on Tab to best match
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

Sometimes you want a command to act as a trigger for a change on things. THen look at the FPSCounter as it makes use of the ConsoleCommands.OnCommandReceived EventHandler to
react when showfps is passed as command.

To toggle the console setup some input like an action and call the GameConsole.Toggle();
```cs
        if(Input.IsActionJustPressed("ToggleConsole")){ MConsole.GameConsole.Toggle(); }
```


# ToDo
Type check parameters according to Command definition using the Command.args;
