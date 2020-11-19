using System.Collections.Generic;
using HelperKit.Test.Builders;

namespace HelperKit.Test.Models
{
    public class TestClass
    {
        public int IntValue { get; set; }
        public string StringValue { get; set; }
        public bool BooleanValue { get; set; }
        public int[] IntArray { get; set; }
        public List<int> IntList { get; set; }

        public static TestClass Create()
        {
            return TestClassBuilder.Faker().Generate();
        }
    }
}