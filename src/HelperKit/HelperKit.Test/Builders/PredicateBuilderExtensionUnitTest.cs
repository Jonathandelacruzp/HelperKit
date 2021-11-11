using HelperKit.Builders;

namespace HelperKit.Test.Builders;

public class PredicateBuilderExtensionUnitTest
{
    [Fact]
    public void TruePredicate_ReturnsCorrectStatement()
    {
        const int count = 5;
        var nidePredicate = PredicateBuilder.True<string>();
        nidePredicate = nidePredicate.And(x => x == "nide").And(x => x.Contains("ni")).And(x => x.EndsWith("e"));

        var nicePredicate = PredicateBuilder.True<string>();
        nicePredicate = nicePredicate.And(x => x == "nice").And(x => x.Contains("ni")).And(x => x.EndsWith("e"));

        var niceArr = Enumerable.Repeat("nice", count).ToArray();
        var nideArr = Enumerable.Repeat("nide", count).ToArray();

        var niceResult = niceArr.Where(nicePredicate.Compile()).ToArray();
        var nideResult = nideArr.Where(nidePredicate.Compile()).ToArray();

        var mergeArr = new List<string>();
        mergeArr.AddRange(niceArr);
        mergeArr.AddRange(nideArr);

        var mergePredicate = PredicateBuilder.New<string>();
        mergePredicate = mergePredicate.Or(x => x == "nice").Or(x => x == "nide");

        var mergeResult = mergeArr.Where(mergePredicate.Compile()).ToArray();

        niceResult.Should().HaveCount(count);
        nideResult.Should().HaveCount(count);
        mergeResult.Should().HaveCount(count + count);
    }

    [Fact]
    public void FalsePredicate_ReturnsCorrectStatement()
    {
        const int count = 5;
        var predicate = PredicateBuilder.False<string>();
        predicate = predicate.And(x => x == "nide").And(x => x.Contains("ni")).And(x => x.EndsWith("e"));
        //predicate.And(x => x == "nide").And(x => x.Contains("ni")).And(x => x.EndsWith("e"));

        var predicate2 = PredicateBuilder.False<string>();
        predicate2 = predicate2.And(x => x == "nice").And(x => x.Contains("ni")).And(x => x.EndsWith("e"));

        var enumerable = Enumerable.Repeat("nice", count).ToArray();

        var empty = enumerable.Where(predicate.Compile()).ToArray();
        var empty2 = enumerable.Where(predicate2.Compile()).ToArray();

        empty.Should().NotHaveCount(count);
        empty2.Should().NotHaveCount(count);
    }
}