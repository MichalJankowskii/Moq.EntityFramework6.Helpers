namespace Moq.EntityFramework.Helpers.Tests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Xunit;

    public class MoqExtensionsTests
    {
        private readonly Mock<UsersContext> objectsContextMock = new Mock<UsersContext>();

        [Fact]
        public void Given_DBContextMock_When_MockingDBSet_Then_NormalQueryShouldWork()
        {
            // Arrange
            this.objectsContextMock.Setup(x => x.Users).Returns(GenerateUsers());

            // Act
            List<User> result = this.objectsContextMock.Object.Users.ToList();

            // Assert
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public async void Given_DBContextMock_When_MockingDBSet_Then_AsyncQueryShouldWork()
        {
            // Arrange
            this.objectsContextMock.Setup(x => x.Users).Returns(GenerateUsers());

            // Act
            List<User> result = await this.objectsContextMock.Object.Users.ToListAsync();

            // Assert
            Assert.Equal(4, result.Count);
        }

        private static IEnumerable<User> GenerateUsers()
        {
            return new List<User> {new User(), new User(), new User(), new User()};
        }

        public class UsersContext : DbContext
        {
            public virtual DbSet<User> Users { get; set; }
        }

        public class User
        {
            public int Id { get; set; }
            public string Login { get; set; }
        }
    }
}
