namespace Mongo.Service.EmailAPI.Messaging;

public interface IAzureServiceBusConsumer
{
   Task Start();
   Task Stop();
   
}