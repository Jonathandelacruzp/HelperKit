﻿using HelperKit.Functions;

namespace HelperKit.Test.Functions;

public class MemoizerUnitTest
{
    private static int _value;
    private static int _numberOfCallsMemoizer;
    private static int _numberOfCalls;

    [Fact]
    public void SimpleMemoizerFunction_ReturnCachedFunction()
    {
        Func<int> simpleValueFunction = null;
        simpleValueFunction = Memoizer.Memoize(() => SimpleValueFunction(simpleValueFunction));
        var result = simpleValueFunction();

        Assert.Equal(3, result);
    }

    private static int SimpleValueFunction(Func<int> simpleValueFunction)
    {
        _value++;
        if (_value == 2)
            return 1;

        return simpleValueFunction() + simpleValueFunction() + simpleValueFunction();
    }

    [Fact]
    public void MemoizerFunctionWithValueInput_ReturnCachedFunctions()
    {
        Func<int, int> fibonacci = null;
        fibonacci = Memoizer.Memoize((int n1) => Fibonacci(n1, fibonacci));
        var cachedResult = fibonacci(4);
        var result = Fibonacci(4);

        Assert.Equal(3, cachedResult);
        Assert.Equal(3, result);
        Assert.True(_numberOfCallsMemoizer < _numberOfCalls);
    }

    private static int Fibonacci(int value)
    {
        _numberOfCalls++;
        if (value <= 2)
            return 1;

        return Fibonacci(value - 1) + Fibonacci(value - 2);
    }

    private static int Fibonacci(int value, Func<int, int> fibonacci)
    {
        _numberOfCallsMemoizer++;
        if (value <= 2)
            return 1;

        return fibonacci(value - 1) + fibonacci(value - 2);
    }

    [Fact]
    public void MemoizerConcurrentFunctionWithValueInput_ReturnCachedFunctions()
    {
        Func<int, int> fibonacci = null;
        fibonacci = Memoizer.ConcurrentMemoize((int n1) => Fibonacci(n1, fibonacci));
        var result = fibonacci(3);

        Assert.Equal(2, result);
        _numberOfCallsMemoizer = _numberOfCalls = 0;
    }
}