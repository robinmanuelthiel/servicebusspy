﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace ServiceBusSpy.Commands
{
    [Command("list", Description = "Lists all messages from a queue.")]
    public class ListCommand
    {
        [Required]
        [Option(LongName = "queue", ShortName = "q", Description = "The name of the queue to add the message to.")]
        public string Queue { get; }

        [Required]
        [Option(LongName = "connectionString", ShortName = "c", Description = "The connection string of your service bus.")]
        public string ConnectionString { get; }

        [Option(LongName = "verbose", ShortName = "v", Description = "Outputs the message as JSON instead of the content only")]
        public bool Verbose { get; set; }

        public async Task<int> OnExecuteAsync(CommandLineApplication app, IConsole console)
        {
            var batchSize = 100;
            var receiver = new MessageReceiver(ConnectionString, Queue, ReceiveMode.PeekLock);

            // Get first batch
            var messages = await receiver.PeekAsync(batchSize);

            // Check, if any messages are in the queue
            if (messages.Count == 0)
            {
                console.WriteLine("No messages in the queue.");
            }
            else
            {
                // Process messages
                while (messages.Count > 0)
                {
                    foreach (var message in messages)
                    {
                        Console.WriteLine(System.Text.Encoding.Unicode.GetString(message.Body));
                        if (Verbose)
                        {
                            Console.WriteLine(JsonConvert.SerializeObject(message));
                            Console.WriteLine();
                        }
                    }

                    // Get next batch
                    messages = await receiver.PeekAsync(batchSize);
                }
            }
            await receiver.CloseAsync();
            return 0;
        }
    }
}
