using System.Collections.Generic;
using HelperKit.Test.Builders;

namespace HelperKit.Test.Models
{
    public class TestClassWithoutInstance
    {
        public bool BooleanValue;
        public int[] IntArray;
        public IEnumerable<int> IntEnum;
        public string StringValue;
        public int IntValue { get; set; }

        public static TestClassWithoutInstance Create()
        {
            return TestClassWithoutInstanceBuilder.Faker().Generate();
        }
    }
}