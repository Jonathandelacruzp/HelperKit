using System.Text.RegularExpressions;
using HelperKit.Test.Models;
using NUnit.Framework;

namespace HelperKit.Test.TestExtensions
{
    public class StringExtensionUnitTest
    {
        [Test]
        public void String_RemoveDiacritics()
        {
            const string stringWithDiacritics = "Je veux aller à Saint-Étienne";

            Assert.AreEqual("Je veux aller a Saint-Etienne", stringWithDiacritics.RemoveDiacritics());
        }

        [Test]
        public void DeleteDotAndComa_DeleteThen()
        {
            const string stringWithDotsAndComma = "Je veux ,,, aller à Saint-Étienne...";

            Assert.AreEqual("Je veux  aller à Saint-Étienne", stringWithDotsAndComma.DeleteDotAndComma());
        }

        [Test]
        public void DeleteSlashes_DeleteThen()
        {
            const string stringWithDotsAndComma = @"Je veux /// aller \ Saint-Etienne...";

            Assert.AreEqual("Je veux  aller  Saint-Etienne...", stringWithDotsAndComma.DeleteSlashAndBackslash());
        }

        [Test]
        public void DeleteCustomStrings_DeleteThen()
        {
            const string stringWithDotsAndComma = @"Je veux /// aller \ Saint-Etienne...";

            Assert.AreEqual("Je veux  aller  Saint-Etienne", stringWithDotsAndComma.CustomReplaceOn(@"\", "/", "."));
        }

        [Test]
        public void ReplaceNoBreakingSpace_Replace()
        {
            var stringWithDiacritics = "Lorem Ipsum is simply dummy text of the printing and typesetting industry";
            stringWithDiacritics = Regex.Replace(stringWithDiacritics, @"\u00A0", " ");

            Assert.AreEqual("Lorem Ipsum is simply dummy text of the printing and typesetting industry", stringWithDiacritics.ReplaceNonBreakingSpace());
        }

        [Test]
        public void ToSafeString_ReturnsValue()
        {
            const Color stringTest = Color.Blue;

            Assert.AreEqual("Blue", stringTest.ToSafeString());
        }

        [Test]
        public void ToSafeString_ReturnsDefaultValue_WithNullParameter()
        {
            Assert.IsTrue(((string) null).ToSafeString() == string.Empty);
        }

        //[Test]
        //public void ToStringUtf_ReturnsDefaultValue_WithNullParameter()
        //{
        //    string stringTest = "\xf0\x90\x8c\xbc";

        //    string expected = "U+1F601";

        //    Assert.AreEqual(expected, stringTest.ToStringUtf8());
        //}
    }
}