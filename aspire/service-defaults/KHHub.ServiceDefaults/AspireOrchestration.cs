using System;

namespace KHHub;

/// <summary>
/// Detects when the process is hosted under .NET Aspire AppHost (resource / service discovery endpoints injected).
/// </summary>
public static class AspireOrchestration
{
    public static bool IsEnabled =>
        !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL"))
        || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DOTNET_RESOURCE_SERVICE_ENDPOINT_URL"));
}
