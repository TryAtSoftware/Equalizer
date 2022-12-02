namespace TryAtSoftware.Equalizer.Core.Interfaces;

public interface IEqualizationProfileProvider
{
    IEqualizationProfile GetProfile(object expected, object actual);
}