using System.Threading.Tasks;
using Xunit;
using Verifier =
    Microsoft.CodeAnalysis.CSharp.Testing.XUnit.AnalyzerVerifier<
        Toloymak.Refit.Analyzer.UrlWithSlashAnalyzer>;

namespace Refit.Analyzer.Tests;

public class SampleSemanticAnalyzerTests
{
    [Fact(Skip = "Not implemented")]
    public async Task SetSpeedHugeSpeedSpecified_AlertDiagnostic()
    {
        const string text = @"
public interface IWrongInterface
{
    [Refit.GetAttribute(""AAA"")]
    IApiResponse Get();
}
";

        var expected = Verifier.Diagnostic()
            .WithLocation(3, 7)
            .WithArguments("Get", "GetAttribute");
        await Verifier.VerifyAnalyzerAsync(text, expected);
    }
}