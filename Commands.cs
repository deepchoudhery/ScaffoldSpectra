using Spectre.Console;
using Spectre.Console.Cli;

public class RazorPageCommand : Command<RazorPageCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[Name]")]
        public string Name { get; set; } = default!;
        
        [CommandArgument(1, "[Type]")]
        public string Type { get; set; } = default!;

        [CommandArgument(2, "[DbContext]")]
        public string DbContext { get; set; } = default!;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.MarkupLine($"Name - {settings.Name}");
        AnsiConsole.MarkupLine($"Type - {settings.Type}");
        AnsiConsole.MarkupLine($"DbContext - {settings.DbContext}");
        return 0;
    }
}

public class MinimalApiCommand : Command<MinimalApiCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[Name]")]
        public string Name { get; set; } = default!;
        
        [CommandArgument(1, "[Endpoints Class]")]
        public string EndpointsClass { get; set; } = default!;

        [CommandArgument(2, "[DbContext]")]
        public string DbContext { get; set; } = default!;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.MarkupLine($"Name - {settings.Name}");
        AnsiConsole.MarkupLine($"Endpoints Class - {settings.EndpointsClass}");
        AnsiConsole.MarkupLine($"DbContext - {settings.DbContext}");
        return 0;
    }
}