namespace TryAtSoftware.Equalizer.Benchmark;

using BenchmarkDotNet.Attributes;
using KellermanSoftware.CompareNetObjects;
using TryAtSoftware.Equalizer.Core;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Profiles.General;

/*
|                       Method | Depth |       Mean |    Error |   StdDev |
|----------------------------- |------ |-----------:|---------:|---------:|
|         AssertUsingEqualizer |     0 |   214.7 ms |  2.29 ms |  2.03 ms |
| AssertUsingCompareNetObjects |     0 |   318.4 ms |  4.50 ms |  4.21 ms |
|         AssertUsingEqualizer |     1 |   361.7 ms |  2.09 ms |  1.85 ms |
| AssertUsingCompareNetObjects |     1 |   573.7 ms |  3.37 ms |  3.15 ms |
|         AssertUsingEqualizer |    10 | 1,766.6 ms |  7.52 ms |  6.67 ms |
| AssertUsingCompareNetObjects |    10 | 2,829.1 ms | 11.60 ms | 10.85 ms |
|         AssertUsingEqualizer |   100 | 1,670.6 ms |  6.13 ms |  5.73 ms |
| AssertUsingCompareNetObjects |   100 | 3,048.1 ms | 13.36 ms | 11.84 ms |
 */
public class GeneralEqualizationBenchmark
{
    private const int IterationsCount = 100_000;
    private readonly GeneralEqualizationModel[] _instances = new GeneralEqualizationModel[2 * IterationsCount];

    private IEqualizer _equalizer;
    private ICompareLogic _compareLogic;
    
    [Params(0, 1, 10, 100)]
    public int Depth { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var equalizer = new Equalizer();

        var dedicatedEqualizationProfilesProvider = new DedicatedProfileProvider();
        dedicatedEqualizationProfilesProvider.AddProfile(new GeneralEqualizationProfile<GeneralEqualizationModel>());
        equalizer.AddProfileProvider(dedicatedEqualizationProfilesProvider);

        this._equalizer = equalizer;

        this._compareLogic = new CompareLogic();
    }

    [IterationSetup]
    public void IterationSetup()
    {
        var reductionCoefficient = this.Depth / 10;
        var normalizedIterationsCount = IterationsCount;
        if (reductionCoefficient > 0) normalizedIterationsCount /= reductionCoefficient;
        
        for (var i = 0; i < normalizedIterationsCount; i++)
        {
            var time = DateTime.Now;

            var first = new GeneralEqualizationModel { Id = i, Time = time };
            this._instances[2 * i] = first;

            var second = new GeneralEqualizationModel { Id = i, Time = time };
            this._instances[2 * i + 1] = second;

            for (var j = 0; j < this.Depth; j++)
            {
                first.Child = new GeneralEqualizationModel { Id = i, Time = time };
                first = first.Child;

                second.Child = new GeneralEqualizationModel { Id = i, Time = time };
                second = second.Child;
            }
        }
    }

    [Benchmark]
    public void AssertUsingEqualizer()
    {
        for (var i = 0; i < IterationsCount; i++)
            this._equalizer.AssertEquality(this._instances[2 * i], this._instances[2 * i + 1]);
    }

    [Benchmark]
    public void AssertUsingCompareNetObjects()
    {
        for (var i = 0; i < IterationsCount; i++)
            _ = this._compareLogic.Compare(this._instances[2 * i], this._instances[2 * i + 1]);
    }
}

public class GeneralEqualizationModel
{
    public required int Id { get; set; }
    public required DateTime Time { get; set; }
    
    public GeneralEqualizationModel Child { get; set; }
}