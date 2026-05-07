using Discord.Webhook;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

var unityPath = Environment.GetEnvironmentVariable("MVU2526_UNITY");
var projectPath = Environment.GetEnvironmentVariable("MVU2526_PROJECT");
var buildPath = Path.Combine(projectPath, @"Builds\Game.exe");

var discordWebhook = @"https://discord.com/api/webhooks/1232757670075830395/gkknkq-Yv-TP2DcDAghPgBcLk3fu183zXgoZ4Sw5pLFs-R2j71arO96YGpzTzd519QyY";
var discordClient = new DiscordWebhookClient(discordWebhook);

var arguments = @$"-batchmode -projectPath ""{projectPath}"" -buildWindows64Player ""{buildPath}"" -quit";

CommandExecutionResult result = RunCommand(unityPath, arguments);

if (result.exitCode != 0)
{
    await discordClient.SendMessageAsync($"There was an error:\n```\nstdout:\n{result.stdout}\nstderr:\n{result.stderr}\n```");
}
else
{
    await discordClient.SendMessageAsync("Build success!");
}

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

public class CommandExecutionResult
{
    public int exitCode;
    public string stdout;
    public string stderr;
}