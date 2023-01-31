using ScaffoldSpectra;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(c =>
{
    c.CaseSensitivity(CaseSensitivity.None);
    c.AddBranch<ScaffoldSettings>("scaffold", scaffold =>
    {
        scaffold.AddCommand<MinimalApiCommand>("minimalapi");
    });

});
args = Helper.ValidateArgs(args);
await app.RunAsync(args);