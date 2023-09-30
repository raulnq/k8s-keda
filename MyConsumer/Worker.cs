using Azure.Core;
using Azure.Messaging.ServiceBus;

namespace MyConsumer;

public class Worker : IHostedService
{
    ServiceBusClient client;

    ServiceBusProcessor processor;

    public Worker()
    {
        client = new ServiceBusClient("<MY_QUEUE_CONNECTION_STRING>");
        processor = client.CreateProcessor("MyQueue", new ServiceBusProcessorOptions());
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;
        await processor.StartProcessingAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await processor.StopProcessingAsync();
        await processor.CloseAsync();
        await processor.DisposeAsync();
        await client.DisposeAsync();
    }

    async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();
        Console.WriteLine($"Received: {body}");
        await args.CompleteMessageAsync(args.Message);
    }

    Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}
