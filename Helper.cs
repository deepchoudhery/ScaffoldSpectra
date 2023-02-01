using Spectre.Console;

namespace ScaffoldSpectra
{
    internal static class Helper
    {
        internal static List<string> GetAllFiles(string? directory, string extension)
        {
            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentNullException(nameof(directory));
            }
            List<string> files = Directory.GetFiles(directory, $"*{extension}", SearchOption.AllDirectories).ToList();
            return files;
        }

        internal static string[] ValidateArgs(string[] args)
        {
            if (args.Length == 1 || args.Length >= 2 && !Generators.Values.ToList().Contains(args[1]))
            {
                string generatorDisplayName = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Pick a valid dotnet-scaffold generator")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more generators)[/]")
                        .AddChoices(Generators.Keys.ToList()));

                Array.Resize(ref args, 2);
                if (Generators.TryGetValue(generatorDisplayName, out var generatorName))
                {
                    args[1] = generatorName;
                }
            }

            return args;
        }

        public static IDictionary<string, string> Generators = new Dictionary<string, string>
        {
            { "Area", "area" },
            { "Razor Page", "razorpage"  },
            { "Minimal API w/ endpoints", "minimalapi"  },
            { "Controller", "controller" }
        };
    }
}
