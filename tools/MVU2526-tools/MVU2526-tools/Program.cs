using CG.Web.MegaApiClient;
using Discord.Webhook;
using MVU2526_tools;
using System.Diagnostics;
using System.IO.Compression;
using File = System.IO.File;

var unityPath = Environment.GetEnvironmentVariable("MVU2526_UNITY");
var projectPath = Environment.GetEnvironmentVariable("MVU2526_PROJECT");
var buildFolder = Path.Combine(projectPath, "Builds");
var buildPath = Path.Combine(buildFolder, @"Windows/Game.exe");
var zipFileName = Path.Combine(buildFolder, "Game.zip");

var discordClient = new DiscordWebhookClient(Secrets.DiscordWebhook);

var arguments = @$"-batchmode -projectPath ""{projectPath}"" -buildWindows64Player ""{buildPath}"" -quit";

MegaApiClient client = new MegaApiClient();
client.Login(Secrets.Email, Secrets.Password);

Console.WriteLine("Building Game...");
CommandExecutionResult result = RunCommand(unityPath, arguments);


if (result.exitCode != 0)
{
    await discordClient.SendMessageAsync($"There was an error:\n```\nstdout:\n{result.stdout}\nstderr:\n{result.stderr}\n```");
    return 1;
}

Console.WriteLine("Compressing build folder...");

if (File.Exists(zipFileName))
{
    File.Delete(zipFileName);
}

ZipFile.CreateFromDirectory(buildFolder+"\\Windows", zipFileName);

Console.WriteLine("Uploading zip file...");
var downloadUrl = UploadFile(zipFileName);

Console.WriteLine("Upload success, sending discord message...");
await discordClient.SendMessageAsync($"Build Success!!\nurl: {downloadUrl}");


client.Logout();

return 0;

static CommandExecutionResult RunCommand(string unityPath, string arguments)
{
    var processInfo = new ProcessStartInfo()
    {
        FileName = unityPath,
        Arguments = arguments,
        RedirectStandardError = true,
        RedirectStandardOutput = true,
    };

    var buildProcess = new Process() { StartInfo = processInfo };

    buildProcess.Start();
    buildProcess.WaitForExit();
    return new CommandExecutionResult
    {
        exitCode = buildProcess.ExitCode,
        stdout = buildProcess.StandardOutput.ReadToEnd(),
        stderr = buildProcess.StandardError.ReadToEnd()
    };
}

string UploadFile(string filePath)
{
    IEnumerable<INode> nodes = client.GetNodes();
    INode root = nodes.Single(x => x.Type == NodeType.Root);
    INode myFile = client.UploadFile(filePath, root);

    Uri downloadLink = client.GetDownloadLink(myFile);

    return downloadLink.ToString();
}

public class CommandExecutionResult
{
    public int exitCode;
    public string stdout;
    public string stderr;
}