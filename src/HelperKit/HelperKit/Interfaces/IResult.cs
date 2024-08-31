namespace HelperKit.Interfaces;

public interface IResult
{
    int StatusCode { get; set; }
    string Message { get; set; }
    string Detail { get; set; }
}

public interface IResult<T> : IResult
{
    T Value { get; set; }
}
