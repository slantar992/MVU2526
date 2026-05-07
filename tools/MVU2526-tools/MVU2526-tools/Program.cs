using System.Diagnostics;

var unityPath = Environment.GetEnvironmentVariable("MVU2526_UNITY");
var projectPath = Environment.GetEnvironmentVariable("MVU2526_PROJECT");
var buildPath = Path.Combine(projectPath, @"Builds\Game.exe");

var arguments = @$"-projectPath ""{projectPath}"" -buildWindows64Player ""{buildPath}"" -quit";

var buildProcess = Process.Start(unityPath, arguments);

buildProcess.WaitForExit();