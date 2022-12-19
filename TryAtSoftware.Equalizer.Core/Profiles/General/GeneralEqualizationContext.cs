namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using TryAtSoftware.Extensions.Reflection;
using TryAtSoftware.Extensions.Reflection.Interfaces;

public class GeneralEqualizationContext<T> : IGeneralEqualizationContext<T>
{
    internal static GeneralEqualizationContext<T> Instance { get; } = Initialize();

    public GeneralEqualizationContext(IMembersBinder membersBinder)
    {
        if (membersBinder is null) throw new ArgumentNullException(nameof(membersBinder));
        if (membersBinder.Type != typeof(T)) throw new InvalidOperationException("The provided members binder instance operates with a different type.");
        
        var valueAccessors = new Dictionary<string, Func<T, object>>();
        foreach (var (key, memberInfo) in membersBinder.MemberInfos)
        {
            if (memberInfo is not PropertyInfo propertyInfo) continue;
            valueAccessors[key] = propertyInfo.ConstructPropertyAccessor<T, object>().Compile();
        }

        this.ValueAccessors = new ReadOnlyDictionary<string, Func<T, object>>(valueAccessors);
    }

    public IReadOnlyDictionary<string, Func<T, object>> ValueAccessors { get; }

    private static GeneralEqualizationContext<T> Initialize()
    {
        var membersBinder = new MembersBinder<T>(IsValid, BindingFlags.Instance | BindingFlags.Public);
        return new GeneralEqualizationContext<T>(membersBinder);

        static bool IsValid(MemberInfo memberInfo)
        {
            if (memberInfo.MemberType != MemberTypes.Property) return false;
            return memberInfo is PropertyInfo { CanRead: true };
        }
    }
}