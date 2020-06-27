using System.Collections.Generic;

namespace HelperKit.Test.Models
{
    public class TestClassWithoutInstance
    {
        public int IntValue { get; set; }
        public string StringValue;
        public bool BooleanValue;
        public int[] IntArray;
        public IEnumerable<int> IntList;
    }
}