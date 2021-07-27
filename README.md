<p align="center">
    <img src="https://github.com/lluckymou/simple-pinball/raw/main/Game/title.png?raw=true"/>
</p>
<h1 align="center">
    <img src="https://github.com/lluckymou/simple-pinball/blob/main/Assets/UI/Achievements/icons8-pinball-80.png?raw=true" height="25"/>
    <a href="https://lluckymou.github.io/simple-pinball/">Play</a>
    •
    <img src="https://github.com/lluckymou/simple-pinball/blob/main/Assets/UI/Achievements/icons8-fantasy-80.png?raw=true" height="25"/>
    <a href="https://github.com/lluckymou/simple-pinball/wiki">Wiki</a>
</h1>

Simple pinball is a game made as a university project aimed at teaching [Unity](https://unity.com) physics. Although the idea was to create just a simple pinball game with a plunger, flippers and an "obstacle", the project grew and now has 12 different power-ups, as well as a couple of achievements that make the game much more fun and enjoyable.

## Installation and usage

Simple Pinball requires the latest **[Unity 2020.3](https://unity3d.com/get-unity/download/archive)** LTS version.
[Visual Studio Code](https://code.visualstudio.com) with the [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) is also recommended. The recommended setup guide is described below.

### Development setup (July 2021)

Simply follow the instructions to run the project from the source above or on your own fork.

Download the following development kits:
- [.NET Framework SDK 4.7.1](https://dotnet.microsoft.com/download/dotnet-framework/net471)
- [.NET SDK 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
- [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)

Download and install [Visual Studio Code](https://code.visualstudio.com) for scripting as well as the [The C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).

Download [Unity Hub](https://unity3d.com/get-unity/download) and activate your license there.

Download Unity 2020.3 LTS from their [Archive](https://unity3d.com/get-unity/download/archive) and make sure to install WebGL compiling capabilites to be able to compile Simple Pinball for the web.

> You may also disable the option to download Visual Studio, as this tutorial covers the setup of Visual Studio Code, which is much lighter.

Now clone the repository inside the folder of your choice:

```bash
git clone https://github.com/lluckymou/simple-pinball.git
```

Add the repository folder to Unity Hub and open the project. Once open, you may configure Visual Studio by navigating to:

**Edit** > Preferences > External Tools

And setting Visual Studio Code as your default "External Script Editor".

Now simply open any script and let Visual Studio's C# extension configure itself. If anything fails the console will prompt what is wrong and it wont be hard to fix, from experience it'll probably be some SDK version mismatch.

### Pull requests and changes

Before submitting any changes, make sure to:

- Run your build to check for any compilation/runtime errors;
- Change the *"Game" scene*'s version *Text* component following the format: `1.MAJOR.MINOR.FIXES` (if it's not a fix you can ommit the `.0`, such as `1.0.5` instead of `1.0.5.0`);
- Check if the compression format (under *Project Settings > Player*) is listed as **Disabled**.