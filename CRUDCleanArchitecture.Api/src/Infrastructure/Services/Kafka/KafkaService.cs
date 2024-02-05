using Confluent.Kafka;
using Confluent.Kafka.Admin;
using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using Newtonsoft.Json;

namespace CRUDCleanArchitecture.Infrastructure.Services.Kafka;
public class KafkaService : IKafkaService
{
    private readonly ProducerConfig _producerConfig;

    public KafkaService(ProducerConfig producerConfig)
    {
        _producerConfig = producerConfig;
    }

    public async Task SendMessageAsync(string nameOperation)
    {
        await CreateTopic();
        using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
        {
            var topic = "permissions-operations";
            var mensaje = JsonConvert.SerializeObject(new { Id = Guid.NewGuid(), NameOperation = nameOperation });
            await producer.ProduceAsync(topic, new Message<Null, string> { Value = mensaje });
        }
    }

    private async Task CreateTopic()
    {
        var adminClientConfig = new AdminClientConfig
        {
            BootstrapServers = _producerConfig.BootstrapServers
        };

        using var adminClient = new AdminClientBuilder(adminClientConfig).Build();

        var topicName = "permissions-operations";

        try
        {
            await adminClient.CreateTopicsAsync(new TopicSpecification[]
            {
                    new TopicSpecification { Name = topicName, NumPartitions = 1, ReplicationFactor = 1 }
            });
        }
        catch (CreateTopicsException e)
        {
            if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
            {
                throw;
            }
        }
    }
}
