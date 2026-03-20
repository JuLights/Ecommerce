
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Extensions;

public static class ServiceRegistrationExtensions
{
    // Method to register services for Debug environment
    public static void AddDebugServices(this IServiceCollection services)
    {
        // Register services specific to Debug
    }

    // Method to register services for Release environment
    public static void AddReleaseServices(this IServiceCollection services)
    {
        // Register services specific to Release
    }
}
