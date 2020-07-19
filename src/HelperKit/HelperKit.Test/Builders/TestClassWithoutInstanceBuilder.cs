using System.Linq;
using Bogus;
using HelperKit.Test.Models;

namespace HelperKit.Test.Builders
{
    public class TestClassWithoutInstanceBuilder
    {
        public static Faker<TestClassWithoutInstance> Faker()
        {
            return new Faker<TestClassWithoutInstance>()
                .RuleFor(x => x.BooleanValue, s => s.PickRandom(true, false))
                .RuleFor(x => x.IntArray, s => Enumerable.Repeat(0, s.Random.Number(0, 50)).Select(_ => s.Random.Number(0, 100)).ToArray())
                .RuleFor(x => x.IntEnum, s => Enumerable.Repeat(0, s.Random.Number(0, 50)).Select(_ => s.Random.Number(0, 100)))
                .RuleFor(x => x.StringValue, s => s.Name.FirstName())
                .RuleFor(x => x.IntValue, s => s.Random.Number(0, 100));
        }
    }
}