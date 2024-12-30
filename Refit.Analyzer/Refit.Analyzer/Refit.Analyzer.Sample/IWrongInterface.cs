using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Refit.Analyzer.Sample;

public interface IWrongInterface
{
    [Get("AAA")]
    Task<IApiResponse> Get();
    
    [Refit.Post("BBB")]
    Task<IApiResponse> PostOne();
    
    [FakeTestAttributes.Post("BBB")]
    Task<IApiResponse> PostTwo();

    [Put("/CCC")]
    Task<IApiResponse> Put();
}