namespace HelperKit.Interfaces
{
    public interface IDataResponse<T> : IDataResponse
    {
        T Result { get; set; }
    }

    public interface IDataResponse
    {
        string StatusCode { get; set; }
        string Message { get; set; }
        string Detail { get; set; }
    }
}