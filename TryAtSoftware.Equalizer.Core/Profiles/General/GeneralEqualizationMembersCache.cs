namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using TryAtSoftware.Equalizer.Core.Attributes;
using TryAtSoftware.Extensions.Reflection;

internal static class GeneralEqualizationMembersCache<T>
{
    public static IReadOnlyDictionary<string, Func<T, object>> ValueAccessors { get; } = InitializeValueAccessors();

    private static IReadOnlyDictionary<string, Func<T, object>> InitializeValueAccessors()
    {
        var membersBinder = new MembersBinder<T>(IsValid, BindingFlags.Public | BindingFlags.Instance);

        var valueAccessors = new Dictionary<string, Func<T, object>>();
        foreach (var (key, memberInfo) in membersBinder.MemberInfos)
        {
            if (memberInfo is not PropertyInfo propertyInfo) continue;
            valueAccessors[key] = propertyInfo.ConstructPropertyAccessor<T, object>().Compile();
        }

        return new ReadOnlyDictionary<string, Func<T, object>>(valueAccessors);
    }

    private static bool IsValid(MemberInfo memberInfo)
    {
        if (memberInfo is null) return false;
        if (memberInfo.MemberType != MemberTypes.Property) return false;
        return memberInfo is PropertyInfo { CanRead: true } && !memberInfo.IsDefined(typeof(SurplusPropertyAttribute));
    }
}