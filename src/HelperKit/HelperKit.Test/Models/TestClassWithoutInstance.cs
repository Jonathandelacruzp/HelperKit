using System;
using System.Collections.Generic;
using System.Linq;

namespace HelperKit.Test.Models
{
    public class TestClassWithoutInstance
    {
        public int IntValue { get; set; }
        public string StringValue;
        public bool BooleanValue;
        public int[] IntArray;
        public IEnumerable<int> IntList;

        public static TestClassWithoutInstance Create()
        {
            var random = new Random();
            var arraySize = random.Next() % 20;
            var guid = Guid.NewGuid().ToString().Split('-')[0];
            var testClass = new TestClassWithoutInstance
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