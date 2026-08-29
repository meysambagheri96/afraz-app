using FluentAssertions;
using Xunit;

namespace Afraz.UnitTests;

public sealed class FoundationTests
{
    [Fact]
    public void DomainAssembly_ShouldBeLoadable()
    {
        typeof(Afraz.Domain.AssemblyMarker).Assembly.GetName().Name.Should().Be("Afraz.Domain");
    }
}
