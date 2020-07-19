using System.Collections.Generic;
using System.Linq;
using Bogus;
using HelperKit.Test.Models;

namespace HelperKit.Test.Builders
{
    public static class TestClassBuilder
    {
        public static Faker<TestClass> Faker()
        {
            return new Faker<TestClass>()
                .RuleFor(x => x.BooleanValue, s => s.PickRandom(true, false))
                .RuleFor(x => x.IntArray, s => Enumerable.Repeat(0, s.Random.Number(0, 50)).Select(_ => s.Random.Number(0, 100)).ToArray())
                .RuleFor(x => x.IntList, s => Enumerable.Repeat(0, s.Random.Number(0, 50)).Select(_ => s.Random.Number(0, 100)).ToList())
                .RuleFor(x => x.StringValue, s => s.Name.FirstName())
                .RuleFor(x => x.IntValue, s => s.Random.Number(0, 100));
        }
    }
}