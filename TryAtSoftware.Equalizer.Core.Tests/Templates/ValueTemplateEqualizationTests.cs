namespace TryAtSoftware.Equalizer.Core.Tests.Templates;

using System.Collections;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Templates;
using Xunit;

public class ValueTemplateEqualizationTests
{
    private readonly IEqualizer _equalizer;

    public ValueTemplateEqualizationTests()
    {
        this._equalizer = new Equalizer();
    }

    [Theory, InlineData(null), InlineData(""), InlineData("   ")]
    public void EmptyTextEqualizationProfileShouldWorkCorrectlyWithEmptyText(string? value)
    {
        this._equalizer.AssertEquality(Value.Empty, value);
        this._equalizer.AssertEquality(value, Value.Empty);
    }
    
    [Fact]
    public void EmptyTextEqualizationProfileShouldWorkCorrectlyWithNonEmptyText()
    {
        this._equalizer.AssertInequality(Value.Empty, "text");
        this._equalizer.AssertInequality("text", Value.Empty);
    }

    [Theory, InlineData(null), InlineData(new object[] { new object[0] })]
    public void EmptyCollectionEqualizationProfileShouldWorkCorrectlyWithEmptyCollection(IEnumerable? value)
    {
        this._equalizer.AssertEquality(Value.Empty, value);
        this._equalizer.AssertEquality(value, Value.Empty);
        
        this._equalizer.AssertInequality(Value.Empty, new [] { new object() });
        this._equalizer.AssertInequality("text", Value.Empty);
    }
    
    [Fact]
    public void EmptyCollectionEqualizationProfileShouldWorkCorrectlyWithNonEmptyCollection()
    {
        var collection = new[] { new object() };
        this._equalizer.AssertInequality(Value.Empty, collection);
        this._equalizer.AssertInequality(collection, Value.Empty);
    }
    
    [Fact]
    public void LowerThanEqualizationProfileShouldWorkCorrectly()
    {
        this._equalizer.AssertEquality(Value.LowerThan(10), 5);
        this._equalizer.AssertEquality(5, Value.LowerThan(10));
        
        this._equalizer.AssertInequality(Value.LowerThan(10), 10);
        this._equalizer.AssertInequality(10, Value.LowerThan(10));
        
        this._equalizer.AssertInequality(Value.LowerThan(10), 15);
        this._equalizer.AssertInequality(15, Value.LowerThan(10));
    }
    
    [Fact]
    public void LowerThanOrEqualEqualizationProfileShouldWorkCorrectly()
    {
        this._equalizer.AssertEquality(Value.LowerThanOrEqual(10), 5);
        this._equalizer.AssertEquality(5, Value.LowerThanOrEqual(10));
        
        this._equalizer.AssertEquality(Value.LowerThanOrEqual(10), 10);
        this._equalizer.AssertEquality(10, Value.LowerThanOrEqual(10));
        
        this._equalizer.AssertInequality(Value.LowerThanOrEqual(10), 15);
        this._equalizer.AssertInequality(15, Value.LowerThanOrEqual(10));
    }
    
    [Fact]
    public void GreaterThanEqualizationProfileShouldWorkCorrectly()
    {
        this._equalizer.AssertInequality(Value.GreaterThan(10), 5);
        this._equalizer.AssertInequality(5, Value.GreaterThan(10));
        
        this._equalizer.AssertInequality(Value.GreaterThan(10), 10);
        this._equalizer.AssertInequality(10, Value.GreaterThan(10));
        
        this._equalizer.AssertEquality(Value.GreaterThan(10), 15);
        this._equalizer.AssertEquality(15, Value.GreaterThan(10));
    }
    
    [Fact]
    public void GreaterThanOrEqualEqualizationProfileShouldWorkCorrectly()
    {
        this._equalizer.AssertInequality(Value.GreaterThanOrEqual(10), 5);
        this._equalizer.AssertInequality(5, Value.GreaterThanOrEqual(10));
        
        this._equalizer.AssertEquality(Value.GreaterThanOrEqual(10), 10);
        this._equalizer.AssertEquality(10, Value.GreaterThanOrEqual(10));
        
        this._equalizer.AssertEquality(Value.GreaterThanOrEqual(10), 15);
        this._equalizer.AssertEquality(15, Value.GreaterThanOrEqual(10));
    }
}