namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using System.Reflection;
using TryAtSoftware.Equalizer.Core.Attributes;
using TryAtSoftware.Extensions.Reflection;
using TryAtSoftware.Extensions.Reflection.Interfaces;

internal static class GeneralEqualizationMembersCache<T>
{
    public static IMembersBinder<T> Binder { get; } = new MembersBinder<T>(IsValid, BindingFlags.Public | BindingFlags.Instance);

    private static bool IsValid(MemberInfo memberInfo)
    {
        if (memberInfo is null) return false;
        if (memberInfo.MemberType != MemberTypes.Property) return false;
        return memberInfo is PropertyInfo { CanRead: true } && !memberInfo.IsDefined(typeof(SurplusPropertyAttribute));
    }
}