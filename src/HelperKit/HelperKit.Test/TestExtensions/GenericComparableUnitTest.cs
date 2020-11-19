using System;
using System.Collections.Generic;
using HelperKit.Test.Models;
using NUnit.Framework;

namespace HelperKit.Test.TestExtensions
{
    public class GenericComparableUnitTest
    {
        private readonly IEnumerable<Color> _colorList = new[] {Color.Blue, Color.Yellow, Color.Yellow};

        private readonly ICollection<Color> _colorListToFind = new List<Color> {Color.Blue, Color.Red};

        [Test]
        public void HasAnyExtension_ShouldReturn_Valid_Result()
        {
            Assert.IsTrue(_colorList.ContainsAny(Color.Blue));
            Assert.IsTrue(_colorList.ContainsAny(Color.Yellow));
        }

        [Test]
        public void HasAnyExtension_ShouldReturn_ValidResult_FromParams()
        {
            Assert.IsTrue(_colorList.ContainsAny(Color.Blue, Color.Yellow));
        }

        [Test]
        public void HasAnyExtension_ShouldReturn_Valid_False_Result()
        {
            Assert.IsFalse(_colorList.ContainsAny(Color.Red));
        }

        [Test]
        public void IsContainedOnExtension_ShouldReturn_Valid_Result()
        {
            var colorBlue = Color.Blue;

            var paramTest = colorBlue.IsContainedOn(Color.Blue, Color.Yellow, Color.Red);
            Assert.IsTrue(paramTest);

            var enumerableTest = colorBlue.IsContainedOn(_colorListToFind);
            Assert.IsTrue(enumerableTest);
        }

        [Test]
        public void IsContainedOnExtension_ShouldTrow_AnArgumentException()
        {
            void action() => _colorListToFind.IsContainedOn(_colorList);
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void IsContainedOnExtension_ShouldTrow_AnArgumentNUllException()
        {
            void actionNull() => ((string) null).IsContainedOn("rojo", "verde");
            Assert.Throws<ArgumentNullException>(actionNull);
        }
    }
}