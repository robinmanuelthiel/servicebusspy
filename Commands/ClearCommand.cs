using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace ServiceBusSpy.Commands
{
    [Command("clear", Description = "Sends all messages from a queue to the dead letter queue.")]
    public class ClearCommand
    {
        [Required]
        [Option(LongName = "queue", ShortName = "q", Description = "The name of the queue to add the message to.")]
        public string Queue { get; }

        [Required]
        [Option(LongName = "connectionString", ShortName = "c", Description = "The connection string of your service bus.")]
        public string ConnectionString { get; }

        public async Task<int> OnExecuteAsync(CommandLineApplication app, IConsole console)
        {
            var client = new QueueClient(ConnectionString, Queue);
            var receiver = new MessageReceiver(ConnectionString, Queue, ReceiveMode.PeekLock);
            var count = 0;

            while (await receiver.PeekAsync() != null)
            {
                console.Write("...");
                var fullMessage = await receiver.ReceiveAsync();
                await receiver.DeadLetterAsync(fullMessage.SystemProperties.LockToken);
                count++;
            }

            console.WriteLine();
            console.WriteLine($"Sent {count} message(s) to the dead letter queue.");

            await receiver.CloseAsync();
            return 0;
        }
    }
}
