using HelperKit.Test.Models;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace HelperKit.Test.Extensions
{
    public class StringExtensionUnitTest
    {
        [Test]
        public void String_RemoveDiacritics()
        {
            var stringWithDiacritics = "Je veux aller à Saint-Étienne";

            Assert.AreEqual("Je veux aller a Saint-Etienne", stringWithDiacritics.RemoveDiacritics());
        }

        [Test]
        public void DeleteDotandComa_DeleteThen()
        {
            var stringWithDotsAndComma = "Je veux ,,, aller à Saint-Étienne...";

            Assert.AreEqual("Je veux  aller à Saint-Étienne", stringWithDotsAndComma.DeleteDotAndComma());
        }

        [Test]
        public void ReplaceNoBbreakingSpace_Replace()
        {
            var stringWithDiacritics = "Lorem Ipsum is simply dummy text of the printing and typesetting industry";
            stringWithDiacritics = Regex.Replace(stringWithDiacritics, @"\u00A0", " ");

            Assert.AreEqual("Lorem Ipsum is simply dummy text of the printing and typesetting industry", stringWithDiacritics.ReplaceNoBbreakingSpace());
        }

        [Test]
        public void ToSafeString_ReturnsValue()
        {
            var stringTest = Color.Blue;

            Assert.AreEqual("Blue", stringTest.ToSafeString());
        }

        [Test]
        public void ToSafeString_ReturnsDefaultValue_WithNullParameter()
        {
            string stringTest = null;

            Assert.IsTrue(string.IsNullOrEmpty(stringTest.ToSafeString()));
        }
    }
}