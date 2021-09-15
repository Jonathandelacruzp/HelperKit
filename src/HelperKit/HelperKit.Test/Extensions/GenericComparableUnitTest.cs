using HelperKit.Test.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace HelperKit.Test.Extensions
{
    public class GenericComparableUnitTest
    {
        private readonly List<Color> _colorList = new()
        {
            Color.Blue,
            Color.Yellow,
            Color.Yellow
        };

        private readonly List<Color> _colorListToFind = new()
        {
            Color.Blue,
            Color.Red
        };

        [Test]
        public void HasAnyExtension_ShouldReturn_Valid_Result()
        {
            Assert.IsTrue(_colorList.HasAny(Color.Blue));
            Assert.IsTrue(_colorList.HasAny(Color.Yellow));
        }

        [Test]
        public void HasAnyExtension_ShouldReturn_Valid_False_Result()
        {
            Assert.IsFalse(_colorList.HasAny(Color.Red));
        }

        [Test]
        public void IsContainedOnExtension_ShouldReturn_Valid_Result()
        {
            const Color colorBlue = Color.Blue;

            var paramTest = colorBlue.IsContainedIn(Color.Blue, Color.Yellow, Color.Red);
            Assert.IsTrue(paramTest);

            var enumerableTest = colorBlue.IsContainedIn(_colorListToFind);
            Assert.IsTrue(enumerableTest);
        }

        [Test]
        public void IsContainedOnExtension_ShouldTrowAnArgumentException()
        {
            void Action() => _colorListToFind.IsContainedIn(_colorList);
            Assert.Throws<ArgumentException>(Action);
        }

        [Test]
        public void IsContainedOnExtension_ShouldTrowAnArgumentNUllException()
        {
            static void NullAction() => ((string) null).IsContainedIn("rojo", "verde");
            Assert.Throws<ArgumentNullException>(NullAction);
        }

        [Test]
        public void CreateDirectory_CreatesDirectory()
        {
            var directoryInfo = new DirectoryInfo("./Test");

            directoryInfo.CreateDirectory();

            var directoryInfoValid = new DirectoryInfo("./Test");
            Assert.IsTrue(directoryInfoValid.Exists);
        }
    }
}