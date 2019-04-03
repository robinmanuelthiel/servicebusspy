using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace ServiceBusSpy.Commands
{
    [Command("subscribe", Description = "Subscribes to a queue and shows all existing and new messages.")]
    public class SubscribeCommand
    {
        [Required]
        [Option(LongName = "queue", ShortName = "q", Description = "The name of the queue to add the message to.")]
        public string Queue { get; }
        [Required]
        [Option(LongName = "connectionString", ShortName = "c", Description = "The connection string of your service bus.")]
        public string ConnectionString { get; }

        public async Task<int> OnExecuteAsync(CommandLineApplication app, IConsole console)
        {
            QueueClient client = new QueueClient(ConnectionString, Queue, ReceiveMode.PeekLock);
            client.RegisterMessageHandler(ReceivedMessageAsync, new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                AutoComplete = false
            });
            console.WriteLine("Subscibed to messages from queue.");
            console.WriteLine("Press any key to cancel...");
            console.WriteLine();
            Console.ReadKey();
            await client.CloseAsync();
            return 0;
        }

        private Task ReceivedMessageAsync(Message message, CancellationToken cancel)
        {
            Console.WriteLine(System.Text.Encoding.Unicode.GetString(message.Body));
            return Task.CompletedTask;
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs.Exception);
            return Task.CompletedTask;
        }
    }
}
