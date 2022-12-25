namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using TryAtSoftware.Extensions.Reflection;
using TryAtSoftware.Extensions.Reflection.Interfaces;

/// <summary>
/// A standard implementation of the <see cref="IGeneralEqualizationContext{T}"/> interface.
/// </summary>
/// <typeparam name="T">The concrete entity type for the general equalization process.</typeparam>
public class GeneralEqualizationContext<T> : IGeneralEqualizationContext<T>
{
    /// <summary>
    /// Gets a singleton instance of the default <see cref="GeneralEqualizationContext{T}"/> for <typeparamref name="T"/>.
    /// It will include public instance properties only and each value accessor will be mapped against the corresponding name of the property.
    /// </summary>
    public static GeneralEqualizationContext<T> Instance { get; } = Initialize();

    /// <summary>
    /// Initializes a new instance of the <see cref="GeneralEqualizationContext{T}"/> class.
    /// </summary>
    /// <param name="membersBinder">An <see cref="IMembersBinder"/> instance exposing all members corresponding to the segments of <typeparamref name="T"/> that should be equalized.</param>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="membersBinder"/> operates with a type different than <typeparamref name="T"/>.</exception>
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

    /// <inheritdoc />
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