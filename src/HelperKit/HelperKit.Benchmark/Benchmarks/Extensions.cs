using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace HelperKit.Benchmark.Benchmarks;

[MemoryDiagnoser]
public class Extensions
{
    private readonly string _textSlash;
    private readonly string _text;

    public Extensions()
    {
        _text =
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.";
        
         _textSlash = @"Je veux /// aller \ Saint-Etienne...";
    }

    [Benchmark]
    public void DeleteDotAndComma()
    {
        var text = _text.DeleteDotAndComma();
    }

    [Benchmark]
    public void SpanDeleteDotAndComma()
    {
        var text = _text.AsSpan().DeleteDotAndComma();
    }

    [Benchmark]
    public void DeleteSlashAndBackslash()
    {
        var text = _textSlash.DeleteSlashAndBackslash();
    }

    [Benchmark]
    public void SpanDeleteSlashAndBackslash()
    {
        var text = _textSlash.AsSpan().DeleteSlashAndBackslash();
    }
}