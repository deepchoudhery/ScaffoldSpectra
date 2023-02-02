using ScaffoldSpectra;
using Spectre.Console;
using Spectre.Console.Cli;
using System.IO;

public class ScaffoldSettings : CommandSettings
{
    [CommandArgument(0, "[Generator]")]
    public string GeneratorName { get; set; } = default!;
}

public class MinimalApiSettings : ScaffoldSettings
{
    [CommandOption("--modelFile")] 
    public string? ModelFile { get; set; } = default!;
    
    [CommandOption("--endpointsClassName")]
    public string? EndpointsClassName { get; set; } = default!;
    
    [CommandOption("--endpointsFile")]
    public string? EndpointsFile { get; set; }

    [CommandOption("--dbContext")]
    public string? DbContext { get; set; }
}

public class MinimalApiCommand : Command<MinimalApiSettings>
{
    public override int Execute(CommandContext context, MinimalApiSettings settings)
    {
        //get model class
        var modelFile = settings.ModelFile;
        if (string.IsNullOrEmpty(modelFile))
        {
            var allFiles = Helper.GetAllFiles(Directory.GetCurrentDirectory().ToString(), ".cs");
            modelFile = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Pick a valid model file")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more files)[/]")
                    .AddChoices(allFiles));
        }
        
        AnsiConsole.MarkupLine($"`Model file name` - {modelFile}");
        var modelName = Path.GetFileNameWithoutExtension(modelFile) + "Endpoints";

        //get endpoints class name
        var endpointsClassName = settings.EndpointsClassName;
        if (string.IsNullOrEmpty(endpointsClassName))
        {
            var className = AnsiConsole.Ask<string>("Give a `class name`:", modelName);
            endpointsClassName = className;
        }

        //check for db context
        if (string.IsNullOrEmpty(settings.DbContext))
        {
            var confPrompt = new ConfirmationPrompt("Do you want a `DbContext` scaffolded with the endpoints?");
            confPrompt.DefaultValue = false;
            bool wantDbContext = AnsiConsole.Prompt(confPrompt);
        }
        return 0;
    }

}
