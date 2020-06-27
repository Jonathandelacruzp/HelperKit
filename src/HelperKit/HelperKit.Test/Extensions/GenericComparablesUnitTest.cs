using HelperKit.Test.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HelperKit.Test.Extensions
{
    public class GenericComparablesUnitTest
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

            string colorNull = null;

            Assert.IsTrue(colorList.HasAny(Color.Blue));
            Assert.IsTrue(colorList.HasAny(Color.Yellow));
            Assert.IsFalse(colorList.HasAny(Color.Red));

            void action() => colorList.IsContainedOn(colorListToFind);
            Assert.Throws<ArgumentException>(action);

            var stringValues = new string[] { "rojo", "verde" };
            void actionNull() => colorNull.IsContainedOn(stringValues);
            Assert.Throws<ArgumentNullException>(actionNull);
        }
    }
}