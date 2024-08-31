using System.Diagnostics.Contracts;

namespace HelperKit.Tests.Models;

internal class Result(int statusCode = 200) : IResult
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; }
    public string Detail { get; set; }

    protected Result(Exception ex) : this(422)
    {
        Message = ex.Message;
        Detail = ex.InnerException?.Message;
    }

    [Pure]
    public static implicit operator Result(Exception ex) => new(ex);
}

internal sealed class Result<T> : Result, IResult<T>
{
    public T Value { get; set; }

    public Result(int statusCode = 200) : base(statusCode) { }

    private Result(T result, int statusCode = 200) : base(statusCode)
    {
        Value = result;
    }

    private Result(Exception ex) : base(ex) { }

    [Pure]
    public static implicit operator Result<T>(T result) => new(result);

    [Pure]
    public static implicit operator Result<T>(Exception ex) => new(ex);

    [Pure]
    public static implicit operator T(Result<T> response) => response.Value;
}
