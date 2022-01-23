namespace HelperKit;

public static partial class Extensions
{
    /// <summary>
    /// Convert stream value to byte[]
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ReadOnlyMemory<byte> ToBytes(this Stream stream)
    {
        _ = stream ?? throw new ArgumentNullException(nameof(stream));

        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        return ms.ToArray();
    }
}
