[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Equalizer&metric=bugs)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Equalizer)

[![Core validation](https://github.com/TryAtSoftware/Equalizer/actions/workflows/Core%20validation.yml/badge.svg)](https://github.com/TryAtSoftware/Equalizer/actions/workflows/Core%20validation.yml)

# About the project

`TryAtSoftware.Equalizer` is a library that should simplify the process of validating the equality between two values no matter of the complexity.
We offer a set of methods and components that can be used to accomplish this goal. They are reusable and can be applied to every projects of yours.

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
        // var myProfileProvider = new MyProfileProvider();
        // equalizer.AddProfileProvider(myPRofileProvider);

        this.Equalizer = equalizer;
    }
}
```

### Asserting equality between two values

In order to assert equality between two values all you need to do is call the `AssertEquality` method of the previously instantiated `Equalizer`. The first parameter you should pass is the expected value and the second - the actual one. The `Equalizer` will then choose the most suitable equalization profile to execute the assertion.

> We should note here that every equalization profile may work with values of different types. There are no restrictions about the type of equality that should be asserted (it all depends on the custom equalization profiles that are used). However, our library is developed with the presumption that two values of different types may be semantically equal and one should be able to assert that without the necessity of defining additional methods for conversions.

### Registering additional equalization profiles

## Creating your first equalization profile
