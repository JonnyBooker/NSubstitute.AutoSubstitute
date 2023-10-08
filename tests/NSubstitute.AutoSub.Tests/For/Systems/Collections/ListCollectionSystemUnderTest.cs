﻿using System.Text;
using NSubstitute.AutoSub.Tests.For.Dependencies;
using NSubstitute.AutoSub.Tests.For.Systems.Collections.Interfaces;

namespace NSubstitute.AutoSub.Tests.For.Systems.Collections;

public class ListCollectionSystemUnderTest : ICollectionSystemUnderTest
{
    private readonly IList<ITextGenerationDependency> _textGenerationDependencies;

    public ListCollectionSystemUnderTest(IList<ITextGenerationDependency> textGenerationDependencies)
    {
        _textGenerationDependencies = textGenerationDependencies;
    }

    public string Generate()
    {
        return string.Join(" ", _textGenerationDependencies.Select(x => x.Generate()));
    }
}