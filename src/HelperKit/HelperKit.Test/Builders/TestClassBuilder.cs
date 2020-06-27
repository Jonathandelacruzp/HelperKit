using HelperKit.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelperKit.Test.Builders
{
    internal static class TestClassBuilder
    {
        public static IEnumerable<TestClass> GenerateElements(int number)
        {
            var testClassList = new List<TestClass>();
            for (var i = 0; i < number; i++)
            {
                testClassList.Add(GenerateTestClass());
            }
            return testClassList;
        }

        public static TestClass GenerateTestClass()
        {
            var random = new Random();
            var arraySize = random.Next() % 20;
            var listSize = random.Next() % 15;
            var guid = Guid.NewGuid().ToString().Split('-')[0];
            var testClass = new TestClass()
            {
                IntValue = random.Next(),
                BooleanValue = random.Next(200) % 4 == 0,
                IntArray = Enumerable.Repeat(0, arraySize).Select(_ => random.Next(0, 100)).ToArray(),
                IntList = Enumerable.Repeat(0, arraySize).Select(_ => random.Next(0, 100)).ToList(),
                StringValue = guid
            };
            return testClass;
        }
    }
}