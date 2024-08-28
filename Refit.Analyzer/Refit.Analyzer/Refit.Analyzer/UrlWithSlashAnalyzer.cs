using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Toloymak.Refit.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class UrlWithSlashAnalyzer : DiagnosticAnalyzer
{
    private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.TRF001Title),
        Resources.ResourceManager, typeof(Resources));

    private static readonly LocalizableString MessageFormat =
        new LocalizableResourceString(nameof(Resources.TRF001MessageFormat), Resources.ResourceManager,
            typeof(Resources));

    private static readonly LocalizableString Description =
        new LocalizableResourceString(nameof(Resources.TRF001Description), Resources.ResourceManager,
            typeof(Resources));
    
    private const string Category = "Confuration";
    
    public const string DiagnosticId = "AB0002";
    
    private static readonly DiagnosticDescriptor Rule = new(
        id: DiagnosticId,
        title: Title,
        messageFormat: MessageFormat,
        category: Category,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: Description
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Method);
    }

    private void AnalyzeSymbol(SymbolAnalysisContext context)
    {                
        var method = (IMethodSymbol)context.Symbol;
        
        foreach (var attribute in method.GetAttributes())
        {
            var attributeName = attribute.AttributeClass?.Name;
            if (attributeName == null
                || attribute.AttributeClass?.ContainingNamespace.Name != "Refit"
                || (attributeName != "GetAttribute"
                && attributeName != "PostAttribute"
                && attributeName != "PutAttribute"
                && attributeName != "DeleteAttribute"))
                continue;

            if (attribute.ConstructorArguments.Length > 0 &&
                attribute.ConstructorArguments[0].Value is string attributeValue &&
                !attributeValue.StartsWith("/"))
            {
                var location = attribute.ApplicationSyntaxReference?.GetSyntax(context.CancellationToken).GetLocation();

                var diagnostic = Diagnostic.Create(
                    Rule,
                    location,
                    attribute.AttributeClass?.ContainingNamespace,
                    method.Name
                );

                context.ReportDiagnostic(diagnostic);
            }
        }
        
    }
}