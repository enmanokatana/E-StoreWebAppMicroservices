using System.Text;
using Azure.Messaging.ServiceBus;
using Mongo.Service.EmailAPI.Models.Dto;
using Mongo.Service.EmailAPI.Services;
using Newtonsoft.Json;

namespace Mongo.Service.EmailAPI.Messaging;

public class AzureServiceBusConsumer : IAzureServiceBusConsumer
{
    private readonly string serviceBusConnectionString;
    private readonly string emailCartQueue;
    private readonly IConfiguration _configuration;
    private ServiceBusProcessor _emailCartProcessor;
    private EmailService _emailService;
    public AzureServiceBusConsumer(IConfiguration configuration, EmailService emailService)
    {
        _configuration = configuration;
        _emailService = emailService;

        serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
        
        emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");

        var client = new ServiceBusClient(serviceBusConnectionString);

        _emailCartProcessor = client.CreateProcessor(emailCartQueue);
        
    }


    public async Task Start()
    {
        _emailCartProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
        _emailCartProcessor.ProcessErrorAsync += ErrorHandler;
        await _emailCartProcessor.StartProcessingAsync();
    }

    private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs arg)
    {
        var message = arg.Message;
        var body = Encoding.UTF8.GetString(message.Body);
        CartDto objMessage = JsonConvert.DeserializeObject<CartDto>(body);

        try
        {
            await _emailService.EmailCartAndLog(objMessage);
            await arg.CompleteMessageAsync(arg.Message);
        }
        catch (Exception e)
        {
            throw;
        }

    }

    private Task ErrorHandler(ProcessErrorEventArgs arg)
    {
        Console.WriteLine(arg.Exception.Message.ToString());
        return Task.CompletedTask;
    }

    public async Task Stop()
    {
        await _emailCartProcessor.StartProcessingAsync();
        await _emailCartProcessor.DisposeAsync();
    }
}