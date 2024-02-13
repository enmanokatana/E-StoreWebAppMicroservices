using Mongo.Service.EmailAPI.Messaging;

namespace Mongo.Service.EmailAPI.Extension;

public static class ApplicationBuilderExtension
{
    private static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }
    
    public static IApplicationBuilder UserAzureServiceBusConsumer(this IApplicationBuilder app)
    {
        ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
        var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        hostApplicationLife.ApplicationStarted.Register(OnStart);
        hostApplicationLife.ApplicationStopped.Register(OnStop);
        return app;
    }

    private static void OnStop()
    {
        ServiceBusConsumer.Stop();
    }

    private static void OnStart()
    {
        ServiceBusConsumer.Start();
        
    }
}