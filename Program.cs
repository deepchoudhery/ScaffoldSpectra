using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(c =>
{
    c.CaseSensitivity(CaseSensitivity.None);
    c.SetApplicationName("scaffold");
    c.AddCommand<RazorPageCommand>("razorpage");
    c.AddCommand<MinimalApiCommand>("minimalapi");
});

await app.RunAsync(args);