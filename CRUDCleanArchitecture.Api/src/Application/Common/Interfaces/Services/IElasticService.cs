namespace CRUDCleanArchitecture.Application.Common.Interfaces.Services;
public interface IElasticService
{
    void IndexDocument<T>(T document) where T : class;
    Task BulkIndexAsync<T>(IEnumerable<T> documents) where T : class;
}
