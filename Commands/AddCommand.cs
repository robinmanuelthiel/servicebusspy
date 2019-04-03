using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace ServiceBusSpy.Commands
{
    [Command("add", Description = "Adds a message to a queue.")]
    public class AddCommand
    {
        [Required]
        [Argument(0, Name = "Message", Description = "The message content as string.")]
        public string Message { get; }

        [Required]
        [Option(LongName = "queue", ShortName = "q", Description = "The name of the queue to add the message to.")]
        public string Queue { get; }
        [Option(LongName = "connectionString", ShortName = "c", Description = "The connection string of your service bus.")]
        public string ConnectionString { get; }

        public async Task<int> OnExecuteAsync(CommandLineApplication app, IConsole console)
        {
            var messageBody = System.Text.Encoding.Unicode.GetBytes(Message);
            var client = new QueueClient(ConnectionString, Queue);
            await client.SendAsync(new Message(messageBody));
            console.WriteLine("Added message to queue.");
            return 0;
        }
    }
}
