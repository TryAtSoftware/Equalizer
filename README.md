[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=bugs)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)

[![Core validation](https://github.com/TryAtSoftware/Equalizer/actions/workflows/Core%20validation.yml/badge.svg)](https://github.com/TryAtSoftware/Equalizer/actions/workflows/Core%20validation.yml)

# About the project

`TryAtSoftware.Equalizer` is a library that should simplify the process of validating the equality between two values no matter of the complexity.

Maybe you are used to writing code like this (where you have methods asserting the equality between every common properties of two objects):

```C#
public static void AssertAreEqual(Person a, Person b)
{
    Assert.NotNull(a);
    Assert.NotNull(b);

    Assert.AreEqual(a.Id, b.Id);
    Assert.AreEqual(a.FirstName, b.FirstName);
    Assert.AreEqual(a.LastName, b.LastName);
    // ... Assert all other propertes ...
}
```

There's nothing wrong with this code and it is totally fine to use such methods. But there are also some situations that are more complex and this solution is not applicable. For example, if you need to assert the equality between two different types, two different types that are part of separate polymorphic hierarchies, different data structures containing entities of different types.

Here comes our library! We offer a set of methods and components that can be used to accomplish this goal. They are reusable and can be applied to every projects of yours.

# About us

`Try At Software` is a software development company based in Bulgaria. We are mainly using `dotnet` technologies (`C#`, `ASP.NET Core`, `Entity Framework Core`, etc.) and our main idea is to provide a set of tools that can simplify the majority of work a developer does on a daily basis.

# Getting started

## Installing the package

Before creating any equalization profiles, you need to install the package.
The simplest way to do this is to either use the `NuGet package manager`, or the `dotnet CLI`.

Using the `NuGet package manager` console within Visual Studio, you can install the package using the following command:

> Install-Package TryAtSoftware.Equalizer

Or using the `dotnet CLI` from a terminal window:

> dotnet add package TryAtSoftware.Equalizer

## Introducing the `Equalizer`

The `Equalizer` class is the heart of this library. It exposes two methods - `AssertEquality` and `AddProfileProvider` (in the next two chapters, you can find out more anout both of them). We suggest instantiating this class for every test (as some of the custom equalization profiles may depend on contextual data) using a similar structure:

```C#
public abstract class MyBaseTest
{
    protected IEqualizer Equalizer { get; }

    protected MyBaseTest()
    {
        var equalizer = new Equalizer();

        // Here you can register additional equalization profile providers if necessary.
        // equalizer.AddProfileProvider(<profile_provider>);

        this.Equalizer = equalizer;
    }
}
```

### Asserting equality between two values

In order to assert equality between two values all you need to do is call the `AssertEquality` method of the previously instantiated `Equalizer`. The first parameter you should pass is the expected value and the second - the actual one. The `Equalizer` will then choose the most suitable equalization profile to execute the assertion.

> We should note here that every equalization profile may work with values of different types. There are no restrictions about the type of equality that should be asserted (it all depends on the custom equalization profiles that are used). However, our library is developed with the presumption that two values of different types may be semantically equal and one should be able to assert that without the necessity of defining additional methods for conversions.

### Registering additional equalization profiles

The registration of additional equalization profiles can happen in three different ways:

- Using a `DedicatedProfileProvider`:

This profile provider should be used whenever you know in advance the equalization profiles you want to register. Even the `Equalizer` class uses this in its core. Here is a short example:

```C#
var equalizer = new Equalizer();

var dedicatedProfileProvider = new DedicatedProfileProvider();
dedicatedProfileProvider.AddProfile(new MyCustomEqualizationProfile());

equalizer.AddProfileProvider(dedicatedProfileProvider);
```

- Using a `DynamicProfileProvider`

This profile provider should be used whenever you do not know in advance the exact multitude of equalization profiles that should be registered (or the registration of an equalization profile depends on some contextual data). For example, this profile provider can be used alongside with a DI container just like this:

```C#
static void RegisterEqualizationProfilesFromDI(Equalizer equalizer, IServiceProvider serviceProvider)
{
    var profileProvider = new DynamicProfileProvider(serviceProvider.GetServices<IEqualizationProfile>);
    equalizer.AddProfileProvider(profileProvider);
}
```

- Using a custom implementation of the `IEqualizationProfileProvider` interface.

If you have some very special case so none of the existing profile providers can deal with it, of course, feel free to write your own custom implementation of the `IEqualizationProfileProvider` interface.

## Complex equalization profiles

The equalization profiles described in this chapter should be used to setup a `complex equalization` process.
There are some of examples of what might be referred by this term:

- Equalizing values of different types
- Equalizing values of the same type when this is not a trivial task

All complex equalization profiles should inherit a common base class called `ComplexEqualizationProfile<TExpected, TActual>`.
The inheritors should setup the equalization logic (throughout registering various `IComplexEqualizationRule<TExpected, TActual>` instances) in their constructor.
There are predefined methods for equalization and differentiation that can be used directly.
Here is one standard example:

```C#
public class CodeRepositoryEqualizationProfile : ComplexEqualizationProfile<CodeRepositoryPrototype, CodeRepository>
{
    public CodeRepositoryEqualizationProfile()
    {
        this.Extend(new CommonIdentifiableEqualizationProfile<CodeRepositoryPrototype, CodeRepository, int>());
        this.Equalize(rp => rp.Name, r => r.Name);
        this.Equalize(rp => rp.Description, r => r.Description);
        this.Equalize(5, r => r.OrganizationId);
        this.Differentiate(rp => rp.Name, r => r.InternalName);
        this.Differentiate(Value.Empty, r => r.InternalName);
        this.Equalize(rp => rp.CommitMessages, r => r.InitialCommits);
        this.Equalize(Value.Empty, r => r.SubsequentCommits);
        this.Equalize(Value.LowerThan(100), r => r.Likes);
        this.Equalize(Value.GreaterThanOrEqual(3), r => r.Likes);
    }
}
```

### Extending

As you can see in the example above, there is one method that has not been mentioned yet.
The `Extend` method is used to enrich the current complex equalization profile with the equalization rules defined within the provided one.

This is the recommended way of reusing complex equalization rules instead of building fancy hierarchies of types.

### Customizing

Validating the equality or inequality between specific segments of two complex objects is enough to cover a big percentage of use cases (including the possibility of using various logical functions throughout [value templates](#value-templates)).
Nevertheless, the complex equalization profiles allow registering custom `complex equalization rules` using the `AddRule(complexEqualizationRule)` method.

## General equalization profiles

General equalization profiles exist to make it easier than ever to equalize two instances of the same type.
Without this feature, it would have been necessary to define a separate equalization profile for every type that should be generally equalized.
Here is an example:

```C#
public class PersonEqualizationProfile : ComplexEqualizationProfile<Person, Person>
{
    public PersonEqualizationProfile()
    {
        this.Equalize(x => x.Id, x => x.Id);
        this.Equalize(x => x.FirstName, x => x.FirstName);
        this.Equalize(x => x.LastName, x => x.LastName);
    }
}
```

This code is simple, very straightforward and easy to understand. However, it seems redundant and hard to maintain (especially when the structure of the equalized type changes).

Instead of creating multiple complex equalization profiles to equalize the values of all publicly exposed properties for a given type, you can use a `general equalization` profile.

```C#
var equalizer = new Equalizer();

var dedicatedProfileProvider = new DedicatedProfileProvider();
dedicatedProfileProvider.AddProfile(new GeneralEqualizationProfile<Person>());

equalizer.AddProfileProvider(dedicatedProfileProvider);
```

### Modifying the default general equalization behavior

This is an advanced topic that describes how the default `general equalization` behavior can be controlled.
As written above the `general equalization` profiles will equalize all publicly exposed properties for a given type.
However, in some cases one would like to equalize some inaccessible properties or even fields; in others one would like to prevent certain properties from being equalized.
This can be achieved by passing a custom `IGeneralEqualizationContext<T>` instance to the `GeneralEqualizationProfile<T>` constructor.
We have identified two approaches of doing this:

- Reusing the default `GeneralEqualizationContext<T>` implementation by instantiating it with a specific `IMembersBinder` instance. _The default general equalization context works with properties only._
- Defining a custom implementation of the `IGeneralEqualizationContext<T>` interface.

No matter of the selected approach, there are a few more things to be considered:

- Use reflection wisely
- Cache the value accessors per type

> The default general equalization context uses reflection optimally by constructing an expression for each property included within the general equalization process and then compiling it.
> The singleton instance `GeneralEqualizationContext<T>.Instance` is initialized by following the described process and thus is realized a simple caching mechanism.

## Value templates

There are some cases for which standard value equality is not applicable and the plain old process of equality validation is no longer appropriate.
`Value templates` allow us to be as flexible and minimalistic as possible because thus we can extend the existing platform with different behavior.
For each defined `value template` there are standard internally included equalization profiles that realize additional logical functions - `greater than a value`, `greater than or equal to a value`, `lower than a value`, `lower than or equal to a value`, `is empty`, etc.

- All `value templates` _should_ be constructed throughout the `Value` static class.
- All `value templates` _must_ be included within the equality validation process as an expected value.
