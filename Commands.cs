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
    [CommandArgument(0, "[ModelFile]")]
    public string ModelFile { get; set; } = default!;
}

public class MinimalApiCommand : Command<MinimalApiSettings>
{
    public override int Execute(CommandContext context, MinimalApiSettings settings)
    {
        var modelFile = settings.ModelFile;
        if (string.IsNullOrEmpty(modelFile))
        {
            var allFiles = Helper.GetAllFiles(@"C:\Users\Deep\source\repos\deepchoudhery\ScaffoldSpectra\", ".cs");
            modelFile = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Pick a valid model file")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more files)[/]")
                    .AddChoices(allFiles));
        }
        AnsiConsole.MarkupLine($"Model file name - {modelFile}");
        return 0;
    }

}
