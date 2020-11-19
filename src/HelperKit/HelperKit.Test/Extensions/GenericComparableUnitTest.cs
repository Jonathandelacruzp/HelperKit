using HelperKit.Test.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HelperKit.Test.Extensions
{
    public class GenericComparableUnitTest
    {
        private List<Color> colorList = new List<Color>
        {
            Color.Blue,
            Color.Yellow,
            Color.Yellow
        };

        private List<Color> colorListToFind = new List<Color>
        {
            Color.Blue,
            Color.Red
        };

        [Test]
        public void HasAnyExtension_ShouldReturn_Valid_Result()
        {
            Assert.IsTrue(colorList.HasAny(Color.Blue));
            Assert.IsTrue(colorList.HasAny(Color.Yellow));
        }

        [Test]
        public void HasAnyExtension_ShouldReturn_Valid_False_Result()
        {
            Assert.IsFalse(colorList.HasAny(Color.Red));
        }

        [Test]
        public void IsContainedOnExtension_ShouldReturn_Valid_Result()
        {
            var colorBlue = Color.Blue;

            var paramTest = colorBlue.IsContainedOn(Color.Blue, Color.Yellow, Color.Red);
            Assert.IsTrue(paramTest);

            var enumerableTest = colorBlue.IsContainedOn(colorListToFind);
            Assert.IsTrue(enumerableTest);
        }

        [Test]
        public void IsContainedOnExtension_ShouldTrowAnArgumentException()
        {
            void action() => colorListToFind.IsContainedOn(colorList);
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void IsContainedOnExtension_ShouldTrowAnArgumentNUllException()
        {
            void actionNull() => ((string) null).IsContainedOn("rojo", "verde");
            Assert.Throws<ArgumentNullException>(actionNull);
        }
    }
}