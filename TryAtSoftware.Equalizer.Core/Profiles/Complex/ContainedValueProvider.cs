namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using System;
using System.Linq.Expressions;
using System.Reflection;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Extensions.Reflection;

public class ContainedValueProvider<T> : IValueProvider<T>
{
    private readonly MemberInfo _memberInfo;

    public ContainedValueProvider(Expression<Func<T, object>> memberSelector)
    {
        if (memberSelector is null)
            throw new ArgumentNullException(nameof(memberSelector));

        this._memberInfo = memberSelector.GetMemberInfo();
    }

    public object GetValue(T instance) => this._memberInfo.GetValue(instance);
}