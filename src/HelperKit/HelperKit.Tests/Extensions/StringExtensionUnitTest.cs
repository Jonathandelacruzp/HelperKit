using System.Text.RegularExpressions;

namespace HelperKit.Tests.Extensions;

public class StringExtensionUnitTest
{
    [Fact]
    public void String_RemoveDiacritics()
    {
        const string stringWithDiacritics = "Je veux aller à Saint-Étienne";
        var result = stringWithDiacritics.RemoveDiacritics();

        result.Should().Be("Je veux aller a Saint-Etienne");
    }

    [Fact]
    public void DeleteDotAndComa_DeleteThen()
    {
        const string stringWithDotsAndComma = "Je veux ,,, aller à Saint-Étienne...";
        var result = stringWithDotsAndComma.DeleteDotAndComma();

        result.Should().Be("Je veux  aller à Saint-Étienne");
    }

    [Fact]
    public void DeleteDotAndComaOpt_DeleteThen()
    {
        const string stringWithDotsAndComma = "Je veux ,,, aller à Saint-Étienne...";
        var result = stringWithDotsAndComma.AsSpan().DeleteDotAndComma();

        result.Should().Be("Je veux  aller à Saint-Étienne");
    }

    [Fact]
    public void DeleteSlashes_DeleteThen()
    {
        const string stringWithDotsAndComma = @"Je veux /// aller \ Saint-Etienne...";
        var result = stringWithDotsAndComma.DeleteSlashAndBackslash();

        result.Should().Be("Je veux  aller  Saint-Etienne...");
    }

    [Fact]
    public void DeleteSlashesOpt_DeleteThen()
    {
        const string stringWithDotsAndComma = @"Je veux /// aller \ Saint-Etienne...";
        var result = stringWithDotsAndComma.AsSpan().DeleteSlashAndBackslash();

        result.Should().Be("Je veux  aller  Saint-Etienne...");
    }

    [Fact]
    public void DeleteCustomStrings_DeleteThen()
    {
        const string stringWithDotsAndComma = @"Je veux /// aller \ Saint-Etienne...";
        var result = stringWithDotsAndComma.CustomReplaceOn(@"\", "/", ".");

        result.Should().Be("Je veux  aller  Saint-Etienne");
    }

    [Fact]
    public void ReplaceNoBreakingSpace_Replace()
    {
        var withDiacritics = "Lorem Ipsum is simply dummy text of the printing and typesetting industry";
        withDiacritics = Regex.Replace(withDiacritics, @"\u00A0", " ");

        var result = withDiacritics.ReplaceNonBreakingSpace();

        result.Should().Be("Lorem Ipsum is simply dummy text of the printing and typesetting industry");
    }

    [Fact]
    public void ToSafeString_ReturnsValue()
    {
        const Color stringTest = Color.Blue;

        Assert.Equal("Blue", stringTest.ToSafeString());
    }

    [Fact]
    public void ToSafeString_ReturnsDefaultValue_WithNullParameter()
    {
        var fromNullStringResult = ((string)null).ToSafeString();

        fromNullStringResult.Should().HaveLength(0);
    }

    //[Fact]
    //public void ToStringUtf_ReturnsDefaultValue_WithNullParameter()
    //{
    //    string stringTest = "\xf0\x90\x8c\xbc";

    //    string expected = "U+1F601";

    //    Assert.Equal(expected, stringTest.ToStringUtf8());
    //}
}
