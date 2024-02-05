namespace CRUDCleanArchitecture.Application.Common.Wrapper
{
    public interface IResponse
    {
        List<string> Messages { get; set; }

        bool Succeeded { get; set; }
    }

    public interface IResponse<out T> : IResponse
    {
        T Data { get; }
    }
}