using ScaffoldSpectra;
using Spectre.Console;
using Spectre.Console.Cli;

public class ScaffoldSettings : CommandSettings
{
    [CommandArgument(0, "[Generator]")]
    public string GeneratorName { get; set; } = default!;
}

public class MinimalApiSettings : ScaffoldSettings
{
    [CommandOption("--model")]
    public string? ModelName { get; set; } = default!;

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
        var modelName = settings.ModelName;
        if (string.IsNullOrEmpty(modelName))
        {
            modelName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Pick a valid [underline bold]model[/]:")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more files)[/]")
                    .AddChoices(AllModels));
        }

        AnsiConsole.MarkupLine($"[underline bold]Model[/] name - [purple]{modelName}[/]");

        //get endpoints class name
        var endpointsClassName = settings.EndpointsClassName;
        if (string.IsNullOrEmpty(endpointsClassName))
        {
            var defaultEndpointsClassName = modelName + "Endpoints";
            var className = AnsiConsole.Ask<string>("Give a [underline bold]class name[/]:", defaultEndpointsClassName);
            endpointsClassName = className;
        }

        AnsiConsole.MarkupLine($"[underline bold]Endpoints class[/] name - [purple]{endpointsClassName}[/]");

        //check for db context
        bool usingDbContext = string.IsNullOrEmpty(settings.DbContext);
        var dbContextName = settings.DbContext;
        if (string.IsNullOrEmpty(dbContextName))
        {
            usingDbContext = ConfirmNoDbContext();
        }

        if (usingDbContext)
        {
            var dbContextListPlus = AllModels;
            dbContextListPlus.Insert(0, "> Add new DbContext");
            dbContextName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Pick a valid DbContext:")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more files)[/]")
                    .AddChoices(dbContextListPlus));

            if (dbContextName.Equals("> Add new DbContext"))
            {
                dbContextName = AnsiConsole.Ask<string>("Give a [underline bold]DbContext class[/] name", "ApplicationDbContext");
            }

            AnsiConsole.MarkupLine($"[underline bold]DbContext class[/] name - [purple]{dbContextName}[/]");
        }

        return 0;
    }

    private bool ConfirmNoDbContext()
    {
        var confPrompt = new ConfirmationPrompt("Do you want a [underline bold]DbContext[/] scaffolded with the endpoints?");
        confPrompt.DefaultValue = false;
        bool wantDbContext = AnsiConsole.Prompt(confPrompt);
        return wantDbContext;
    }

    private IList<string>? _allFiles;
    private IList<string> AllFiles
    {
        get
        {
            if (_allFiles is null)
            {
                _allFiles = Helper.GetAllFiles(Directory.GetCurrentDirectory().ToString(), ".cs");
            }

            return _allFiles;
        }
    }

    private IList<string>? _allModels;
    private IList<string> AllModels
    {
        get
        {
            if (_allModels == null)
            {
                _allModels = AllFiles.Select(x => Path.GetFileNameWithoutExtension(x)).ToList();
            }

            return _allModels;
        }
    }
}
