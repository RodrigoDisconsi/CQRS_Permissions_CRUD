using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using Nest;

namespace CRUDCleanArchitecture.Infrastructure.Services.Elastic;
public class ElasticService : IElasticService
{
    private readonly ElasticClient _client;

    public ElasticService(Uri elasticsearchUri)
    {
        var settings = new ConnectionSettings(elasticsearchUri)
            .DefaultIndex("permissions");
        _client = new ElasticClient(settings);
    }

    public void IndexDocument<T>(T document) where T : class
    {
        var response = _client.IndexDocument(document);
        if (!response.IsValid)
        {
            throw new Exception($"Error registrando documento en Elasticsearch: {response.DebugInformation}");
        }
    }

    public async Task BulkIndexAsync<T>(IEnumerable<T> documents) where T : class
    {
        var bulkDescriptor = new BulkDescriptor();

        foreach (var document in documents)
        {
            bulkDescriptor.Index<T>(op => op
                .Index("permissions")
                .Document(document)
            );
        }

        var response = await _client.BulkAsync(bulkDescriptor);

        if (!response.IsValid)
        {
            throw new Exception($"Error al indexar documentos en Elasticsearch: {response.DebugInformation}");
        }
    }
}

