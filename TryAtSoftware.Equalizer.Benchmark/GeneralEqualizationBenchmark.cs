namespace TryAtSoftware.Equalizer.Benchmark;

using BenchmarkDotNet.Attributes;
using KellermanSoftware.CompareNetObjects;
using TryAtSoftware.Equalizer.Core;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Profiles.General;

/*
|                       Method |     Mean |   Error |  StdDev |
|----------------------------- |---------:|--------:|--------:|
|         AssertUsingEqualizer | 164.0 ms | 3.13 ms | 2.93 ms |
| AssertUsingCompareNetObjects | 299.7 ms | 3.62 ms | 3.21 ms |
 */
public class GeneralEqualizationBenchmark
{
    private const int IterationsCount = 100_000;
    private readonly GeneralEqualizationModel[] _instances = new GeneralEqualizationModel[2 * IterationsCount];
    
    private IEqualizer _equalizer;
    private ICompareLogic _compareLogic;

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
        for (var i = 0; i < IterationsCount; i++)
        {
            var time = DateTime.Now;
            
        this._instances[2 * i] = new GeneralEqualizationModel { Id = i, Time = time };
        this._instances[2 * i + 1] = new GeneralEqualizationModel { Id = i, Time = time };
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

class GeneralEqualizationModel
{
    public required int Id { get; set; }
    public required DateTime Time { get; set; }
}