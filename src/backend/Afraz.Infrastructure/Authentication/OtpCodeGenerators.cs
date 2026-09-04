using System.Globalization;
using System.Security.Cryptography;
using Afraz.Application.Features.Authentication;

namespace Afraz.Infrastructure.Authentication;

public sealed class SecureOtpCodeGenerator : IOtpCodeGenerator
{
    public string Generate() =>
        RandomNumberGenerator.GetInt32(10000, 100000).ToString(CultureInfo.InvariantCulture);
}

public sealed class DevelopmentOtpCodeGenerator : IOtpCodeGenerator
{
    public const string TestCode = "54321";

    public string Generate() => TestCode;
}
