using System;
using System.Collections.Generic;
using System.Linq;
using HelperKit.Test.Builders;

namespace HelperKit.Test.Models
{
    public class TestClassWithoutInstance
    {
        public int IntValue { get; set; }
        public string StringValue;
        public bool BooleanValue;
        public int[] IntArray;
        public IEnumerable<int> IntEnum;

        public static IEnumerable<TestClassWithoutInstance> Create(int count)
        {
            return TestClassWithoutInstanceBuilder.Faker().Generate(count);
        }

        public static TestClassWithoutInstance Create()
        {
            return TestClassWithoutInstanceBuilder.Faker().Generate();
        }
    }
}