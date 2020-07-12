using HelperKit.Test.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HelperKit.Test.Extensions
{
    public class GenericComparableUnitTest
    {
        [Test]
        public void HasAnyExtension_ShouldReturn_Valid_Result()
        {
            var colorList = new List<Color>
            {
                Color.Blue,
                Color.Yellow,
                Color.Yellow
            };

            var colorListToFind = new List<Color>
            {
                Color.Blue,
                Color.Red
            };

            Assert.IsTrue(colorList.HasAny(Color.Blue));
            Assert.IsTrue(colorList.HasAny(Color.Yellow));
            Assert.IsFalse(colorList.HasAny(Color.Red));

            void action() => colorList.IsContainedOn(colorListToFind);
            Assert.Throws<ArgumentException>(action);

            void actionNull() => ((string) null).IsContainedOn("rojo", "verde");
            Assert.Throws<ArgumentNullException>(actionNull);
        }
    }
}