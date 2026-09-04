using Afraz.Domain.Users;
using FluentAssertions;
using Xunit;

namespace Afraz.UnitTests;

public sealed class UserTests
{
    [Fact]
    public void Constructor_ShouldAssignUniquePublicId()
    {
        var now = DateTime.UtcNow;

        var first = new User("9120000001", "+98", null, now);
        var second = new User("9120000002", "+98", null, now);

        first.Id.Should().NotBe(Guid.Empty);
        second.Id.Should().NotBe(Guid.Empty);
        first.Id.Should().NotBe(second.Id);
    }
}
