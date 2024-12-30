using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Refit.Analyzer.Sample;

#if DEBUG
[SuppressMessage("Confuration", "TRF001:Refit path should start with /")]
[SuppressMessage("Refit", "RF001:Refit types must have Refit HTTP method attributes")]
#endif
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