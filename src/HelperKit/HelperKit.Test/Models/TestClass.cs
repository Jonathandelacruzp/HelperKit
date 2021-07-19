using System;
using System.Collections.Generic;
using System.Linq;

namespace HelperKit.Test.Models
{
    public class TestClass
    {
        public int IntValue { get; set; }
        public string StringValue { get; set; }
        public bool BooleanValue { get; set; }
        public int[] IntArray { get; set; }
        public List<int> IntList { get; set; }

        public static IEnumerable<TestClass> CreateElements(int number)
        {
            var testClassList = new List<TestClass>();
            for (var i = 0; i < number; i++) testClassList.Add(Create());

            return testClassList;
        }

        public static TestClass Create()
        {
            var random = new Random();
            var arraySize = random.Next() % 20;
            var guid = Guid.NewGuid().ToString().Split('-')[0];
            var testClass = new TestClass
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