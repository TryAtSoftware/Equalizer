namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using TryAtSoftware.Extensions.Reflection;

public class EqualizationRule<T1, T2> : IEqualizationRule<T1, T2>
{
    public EqualizationRule([NotNull] Expression<Func<T2, object>> member, [NotNull] IValueProvider<T1> valueProvider)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));

        this.MemberName = member.GetMemberInfo().Name;
        this.ValueProvider = valueProvider ?? throw new ArgumentNullException(nameof(valueProvider));
    }

    public string MemberName { get; }
    public IValueProvider<T1> ValueProvider { get; }
}