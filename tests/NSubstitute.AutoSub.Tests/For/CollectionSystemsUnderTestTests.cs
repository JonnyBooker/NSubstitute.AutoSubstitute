﻿using AutoFixture;
using NSubstitute.AutoSub.Tests.For.Collections;
using NSubstitute.AutoSub.Tests.For.Collections.Interfaces;
using NSubstitute.AutoSub.Tests.For.Implementations;
using NSubstitute.AutoSub.Tests.For.Interfaces;
using Xunit;

namespace NSubstitute.AutoSub.Tests.For;

public class CollectionSystemsUnderTestTests
{
    private AutoSubstitute AutoSubstitute { get; } = new();

    private Fixture Fixture { get; } = new();
    
    public static IEnumerable<object[]> CollectionData => new List<object[]>
    {
        new object[] { typeof(ReadOnlyCollectionSystemUnderTest) },
        new object[] { typeof(EnumerableSystemUnderTest) },
        new object[] { typeof(ListCollectionSystemUnderTest) },
        new object[] { typeof(CollectionSystemUnderTest) }
    };
    
    [Theory]
    [MemberData(nameof(CollectionData))]
    public void CollectionSystemUnderTestInstances_WhenMultipleDependenciesMocked_ReturnsAllSubstitutedMockedValues(Type value)
    {
        //Arrange
        var item1 = Fixture.Create<string>();
        var item2 = Fixture.Create<string>();

        var instance1 = AutoSubstitute.SubstituteForNoCache<IStringGenerationDependency>();
        var instance2 = AutoSubstitute.SubstituteForNoCache<IStringGenerationDependency>();

        instance1
            .Generate()
            .Returns(item1);
        instance2
            .Generate()
            .Returns(item2);
        
        AutoSubstitute.UseCollection(instance1, instance2);

        var sut = (ICollectionSystemUnderTest) AutoSubstitute.CreateInstance(value);
        var result = sut.Generate();

        //Assert
        Assert.Equal($"{item1} {item2}", result);
    }

    [Theory]
    [MemberData(nameof(CollectionData))]
    public void CollectionSystemUnderTest_WhenUsingSubstituteForEnumerableOnce_ReturnsOnlySubstitutedMockedValue(Type value)
    {
        //Arrange
        var item = Fixture.Create<string>();

        var instance = AutoSubstitute.SubstituteFor<IStringGenerationDependency>();

        instance
            .Generate()
            .Returns(item);

        //Act
        var sut = (ICollectionSystemUnderTest) AutoSubstitute.CreateInstance(value);
        var result = sut.Generate();

        //Assert
        Assert.Equal($"{item}", result);
    }
    
    [Theory]
    [MemberData(nameof(CollectionData))]
    public void CollectionSystemUnderTest_WhenGivenMultipleImplementations_WillReturnImplementationsResult(Type value)
    {
        //Arrange
        AutoSubstitute.UseCollection<IStringGenerationDependency>(new HelloStringGenerationDependency(),
            new WorldStringGenerationDependency());

        //Act
        var sut = (ICollectionSystemUnderTest) AutoSubstitute.CreateInstance(value);
        var result = sut.Generate();

        //Assert
        Assert.Equal($"{HelloStringGenerationDependency.HelloText} {WorldStringGenerationDependency.WorldText}", result);
    }
    
    [Theory]
    [MemberData(nameof(CollectionData))]
    public void CollectionSystemUnderTest_WhenNoEnumerableSubstitutesUsedOrProvided_WillReturnExpectedResult(Type value)
    {
        //Arrange & Act
        var sut = (ICollectionSystemUnderTest) AutoSubstitute.CreateInstance(value);
        var result = sut.Generate();

        //Assert
        Assert.Empty(result);
    }
}