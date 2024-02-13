using System.Text;
using System.Text.Json.Serialization;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace Mongo.ServiceBus;

public class MessageBus : IMessageBus
{


    private string connectionString =
        "Endpoint=sb://mouadmongo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=vjo5lwvELT3bqC/asMnMz1tqjawSl8dh3+ASbC22M2U=";
    
    
    public async Task PublishMessage(object message, string topic_queue_name)
    {
        await using var client = new ServiceBusClient(connectionString);

        ServiceBusSender sender = client.CreateSender(topic_queue_name);
        var jsonMessage = JsonConvert.SerializeObject(message);

        ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
        {
            CorrelationId = Guid.NewGuid().ToString()
        };

        await sender.SendMessageAsync(finalMessage);
        await client.DisposeAsync();
    }
}