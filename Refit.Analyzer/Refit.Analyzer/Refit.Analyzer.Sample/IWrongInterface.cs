using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Refit.Analyzer.Sample;

[SuppressMessage("Design", "RF001:Method either has no Refit HTTP method attribute",
    Justification = "Method for testing")]
public interface IWrongInterface
{
    [Refit.Get("AAA")]
    Task<IApiResponse> Get();
    
    [Refit.Post("BBB")]
    Task<IApiResponse> PostOne();
    
    [Post("BBB")]
    Task<IApiResponse> PostTwo();

    [Put("/CCC")]
    Task<IApiResponse> Put();
}


public class PostAttribute : Attribute
{
    public PostAttribute(string someValue)
    {
        
    }
}