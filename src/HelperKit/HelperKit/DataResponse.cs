using System.Diagnostics.Contracts;

namespace HelperKit;

internal class Result : IResult
{
    public string StatusCode { get; set; }
    public string Message { get; set; }
    public string Detail { get; set; }

    public Result(string statusCode = "200")
    {
        StatusCode = statusCode;
    }

    protected Result(Exception ex)
    {
        Message = ex.Message;
        StatusCode = "500";
    }

    [Pure]

    public static implicit operator Result(Exception ex) => new(ex);
}

internal class Result<T> : Result, IResult<T>
{
    public T Value { get; set; }

    public Result(string statusCode = "200") : base(statusCode) { }

    private Result(T result, string statusCode = "200") : base(statusCode)
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
