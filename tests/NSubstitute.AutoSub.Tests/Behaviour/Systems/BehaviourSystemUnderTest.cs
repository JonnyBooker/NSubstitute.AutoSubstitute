﻿using NSubstitute.AutoSub.Tests.Behaviour.Dependencies;

namespace NSubstitute.AutoSub.Tests.Behaviour.Systems;

public class BehaviourSystemUnderTest
{
    private readonly IBehaviourStringGenerationDependency _stringGenerationDependency;
    private readonly IBehaviourIntGenerationDependency _intGenerationDependency;

    public BehaviourSystemUnderTest(IBehaviourStringGenerationDependency stringGenerationDependency, IBehaviourIntGenerationDependency intGenerationDependency)
    {
        _stringGenerationDependency = stringGenerationDependency;
        _intGenerationDependency = intGenerationDependency;
    }

    public string StringGenerationResult()
    {
        var value = _stringGenerationDependency.Generate();
        return value;
    }

    public string CombineStringAndIntGeneration()
    {
        var stringValue = _stringGenerationDependency.Generate();
        var intValue = _intGenerationDependency.Generate();

        return $"{stringValue} {intValue}";
    }
}