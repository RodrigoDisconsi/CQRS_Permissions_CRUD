namespace CRUDCleanArchitecture.Application.Common.Wrapper;
public class Response : IResponse
{
    public Response()
    {
    }

    public List<string> Messages { get; set; } = new();
    public bool Succeeded { get; set; }

    public static IResponse Fail()
    {
        return new Response { Succeeded = false };
    }

    public static IResponse Fail(string message)
    {
        return new Response { Succeeded = false, Messages = new List<string> { message } };
    }

    public static IResponse Fail(List<string> messages)
    {
        return new Response { Succeeded = false, Messages = messages };
    }

    public static Task<IResponse> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public static Task<IResponse> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }

    public static Task<IResponse> FailAsync(List<string> messages)
    {
        return Task.FromResult(Fail(messages));
    }

    public static IResponse Success()
    {
        return new Response { Succeeded = true };
    }

    public static IResponse Success(string message)
    {
        return new Response { Succeeded = true, Messages = new List<string> { message } };
    }

    public static IResponse Success(List<string> messages)
    {
        return new Response { Succeeded = true, Messages = messages };
    }

    public static Task<IResponse> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public static Task<IResponse> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<IResponse> SuccessAsync(List<string> messages)
    {
        return Task.FromResult(Success(messages));
    }
}

public class ErrorResponse : Response
{
    public string Source { get; set; }
    public string Exception { get; set; }
    public int ErrorCode { get; set; }
}

public class Response<T> : Response
{
    public Response()
    {
    }

    public T Data { get; set; }

    public new static Response<T> Fail()
    {
        return new() { Succeeded = false };
    }

    public new static Response<T> Fail(string message)
    {
        return new() { Succeeded = false, Messages = new List<string> { message } };
    }

    public new static Response<T> Fail(List<string> messages)
    {
        return new() { Succeeded = false, Messages = messages };
    }

    public static ErrorResponse ReturnError(string message)
    {
        return new() { Succeeded = false, Messages = new List<string> { message }, ErrorCode = 500 };
    }

    public static ErrorResponse ReturnError(List<string> messages)
    {
        return new() { Succeeded = false, Messages = messages, ErrorCode = 500 };
    }

    public new static Task<Response<T>> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public new static Task<Response<T>> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }
    

    public new static Task<Response<T>> FailAsync(List<string> messages)
    {
        return Task.FromResult(Fail(messages));
    }

    public static Task<ErrorResponse> ReturnErrorAsync(string message)
    {
        return Task.FromResult(ReturnError(message));
    }

    public static Task<ErrorResponse> ReturnErrorAsync(List<string> messages)
    {
        return Task.FromResult(ReturnError(messages));
    }

    public new static Response<T> Success()
    {
        return new() { Succeeded = true };
    }

    public new static Response<T> Success(string message)
    {
        return new() { Succeeded = true, Messages = new List<string> { message } };
    }

    public new static Response<T> Success(List<string> messages)
    {
        return new() { Succeeded = true, Messages = messages };
    }

    public static Response<T> Success(T data)
    {
        return new() { Succeeded = true, Data = data };
    }

    public static Response<T> Success(T data, string message)
    {
        return new() { Succeeded = true, Data = data, Messages = new List<string> { message } };
    }

    public static Response<T> Success(T data, List<string> messages)
    {
        return new() { Succeeded = true, Data = data, Messages = messages };
    }

    public new static Task<Response<T>> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public new static Task<Response<T>> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public new static Task<Response<T>> SuccessAsync(List<string> messages)
    {
        return Task.FromResult(Success(messages));
    }

    public static Task<Response<T>> SuccessAsync(T data)
    {
        return Task.FromResult(Success(data));
    }

    public static Task<Response<T>> SuccessAsync(T data, string message)
    {
        return Task.FromResult(Success(data, message));
    }

    public static Task<Response<T>> SuccessAsync(T data, List<string> messages)
    {
        return Task.FromResult(Success(data, messages));
    }
}