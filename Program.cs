using System;
using McMaster.Extensions.CommandLineUtils;
using ServiceBusSpy;
using ServiceBusSpy.Commands;

[Subcommand(typeof(ListCommand))]
[Subcommand(typeof(AddCommand))]
[Subcommand(typeof(SubscribeCommand))]
public class Program
{
    public static int Main(string[] args)
    {
        var app = new CommandLineApplication<Program>();
        app.Conventions.UseDefaultConventions();

        try
        {
            Console.WriteLine();
            return app.Execute(args);
        }
        catch (Exception ex)
        {
            if (ex is UnrecognizedCommandParsingException)
            {
                Console.WriteLine("Unknown command.");
                app.ShowHelp();
            }
            else
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }

#if DEBUG
            Console.WriteLine();
            Console.WriteLine(ex);
#endif

            return -1;
        }
        finally
        {
            Console.WriteLine();
        }
    }

    public int OnExecute(CommandLineApplication app, IConsole console)
    {
        app.ShowHelp();
        return 0;
    }
}
