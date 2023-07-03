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
    public void EmptyTextEqualizationProfileShouldWorkCorrectlyWithEmptyText(string? value) => this._equalizer.AssertEquality(Value.Empty, value);

    [Fact]
    public void EmptyTextEqualizationProfileShouldWorkCorrectlyWithNonEmptyText() => this._equalizer.AssertInequality(Value.Empty, "text");

    [Theory, InlineData(null), InlineData(new object[] { new object[0] })]
    public void EmptyCollectionEqualizationProfileShouldWorkCorrectlyWithEmptyCollection(IEnumerable? value)
    {
        this._equalizer.AssertEquality(Value.Empty, value);
        this._equalizer.AssertInequality(Value.Empty, new [] { new object() });
    }

    [Fact]
    public void EmptyCollectionEqualizationProfileShouldWorkCorrectlyWithNonEmptyCollection()
    {
        var collection = new[] { new object() };
        this._equalizer.AssertInequality(Value.Empty, collection);
    }

    [Fact]
    public void LowerThanEqualizationProfileShouldWorkCorrectly()
    {
        this._equalizer.AssertEquality(Value.LowerThan(10), 5);
        this._equalizer.AssertInequality(Value.LowerThan(10), 10);
        this._equalizer.AssertInequality(Value.LowerThan(10), 15);
    }

    [Fact]
    public void LowerThanOrEqualEqualizationProfileShouldWorkCorrectly()
    {
        this._equalizer.AssertEquality(Value.LowerThanOrEqual(10), 5);
        this._equalizer.AssertEquality(Value.LowerThanOrEqual(10), 10);
        this._equalizer.AssertInequality(Value.LowerThanOrEqual(10), 15);
    }

    [Fact]
    public void GreaterThanEqualizationProfileShouldWorkCorrectly()
    {
        this._equalizer.AssertInequality(Value.GreaterThan(10), 5);
        this._equalizer.AssertInequality(Value.GreaterThan(10), 10);
        this._equalizer.AssertEquality(Value.GreaterThan(10), 15);
    }

    [Fact]
    public void GreaterThanOrEqualEqualizationProfileShouldWorkCorrectly()
    {
        this._equalizer.AssertInequality(Value.GreaterThanOrEqual(10), 5);
        this._equalizer.AssertEquality(Value.GreaterThanOrEqual(10), 10);
        this._equalizer.AssertEquality(Value.GreaterThanOrEqual(10), 15);
    }
}