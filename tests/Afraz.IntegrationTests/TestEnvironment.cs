using System.Runtime.CompilerServices;

namespace Afraz.IntegrationTests;

internal static class TestEnvironment
{
    [ModuleInitializer]
    internal static void ConfigureAuthentication()
    {
        Environment.SetEnvironmentVariable(
            "Jwt__SigningKey",
            "integration-tests-only-signing-key-with-at-least-32-characters");
    }
}
