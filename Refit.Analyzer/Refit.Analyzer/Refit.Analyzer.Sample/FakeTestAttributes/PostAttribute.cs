using System;

namespace Refit.Analyzer.Sample.FakeTestAttributes;

/// <summary>
/// Fake Post attribute to test RF001
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class PostAttribute : Attribute
{
    public PostAttribute(string someValue)
    {
        
    }
}