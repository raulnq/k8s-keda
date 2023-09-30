using Azure.Messaging.ServiceBus;

await using (var client = new ServiceBusClient("<MY_QUEUE_CONNECTION_STRING>"))
{
    await using (var sender = client.CreateSender("MyQueue"))
    {
        var numOfMessages = 1000;

        using (ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync())
        {
            for (int i = 1; i <= numOfMessages; i++)
            {
                if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
                {
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }

            await sender.SendMessagesAsync(messageBatch);

            Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
        }
    }
}


