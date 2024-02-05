namespace CRUDCleanArchitecture.Application.Common.Interfaces.Services;
public interface IKafkaService
{
    Task SendMessageAsync(string nameOperation);
}
