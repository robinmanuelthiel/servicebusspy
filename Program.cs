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
        catch (CommandParsingException cpe)
        {
            Console.WriteLine("No such method.");
            Console.WriteLine(cpe.Message);
            return -1;
        }
        finally
        {
            //Console.ReadLine();
            Console.WriteLine();
        }
    }
}
