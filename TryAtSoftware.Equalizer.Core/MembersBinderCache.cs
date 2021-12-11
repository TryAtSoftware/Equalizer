namespace TryAtSoftware.Equalizer.Core;

using System.Reflection;
using TryAtSoftware.Extensions.Reflection;
using TryAtSoftware.Extensions.Reflection.Interfaces;

internal static class MembersBinderCache<TEntity>
    where TEntity : class
{
    static MembersBinderCache()
    {
        Binder = new MembersBinder<TEntity>(IsValid, BindingFlags.Instance | BindingFlags.Public);
    }

    // NOTE: Tony Troeff, 18/04/2021 - This is the idea of this class - to provide a single `IMembersBinder` instance for any requested type represented by the generic parameter.
    public static IMembersBinder<TEntity> Binder { get; }

    private static bool IsValid(MemberInfo memberInfo)
    {
        if (memberInfo.MemberType != MemberTypes.Property)
            return false;

        return memberInfo is PropertyInfo { CanRead: true };
    }
}