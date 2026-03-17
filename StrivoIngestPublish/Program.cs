using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        var config = context.Configuration;
        var credential = new DefaultAzureCredential();

        var blobServiceUri = new Uri(config["BlobServiceUri"]
            ?? throw new InvalidOperationException("Missing required app setting: BlobServiceUri"));

        var queueServiceUri = new Uri(config["QueueServiceUri"]
            ?? throw new InvalidOperationException("Missing required app setting: QueueServiceUri"));

        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(blobServiceUri)
                   .WithCredential(credential);

            builder.AddQueueServiceClient(queueServiceUri)
                   .WithCredential(credential)
                   .ConfigureOptions(options => options.MessageEncoding = QueueMessageEncoding.Base64);
        });
    })
    .Build();

host.Run();
