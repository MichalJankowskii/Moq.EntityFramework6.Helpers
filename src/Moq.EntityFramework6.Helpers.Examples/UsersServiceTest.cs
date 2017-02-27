namespace Moq.EntityFramework6.Helpers.Examples
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using DbAsyncQueryProvider;
    using Moq;
    using Ploeh.AutoFixture;
    using Xunit;

    public class UsersServiceTest
    {
        [Fact]
        public void GetLockedUsers_Invoke_LockedUsers_v1()
        {
            // Arrange
            var fixture = new Fixture();
            var lockedUser = fixture.Build<User>().With(u => u.AccountLocked, true).Create();
            var users = new List<User>
            {
                lockedUser,
                fixture.Build<User>().With(u => u.AccountLocked, false).Create(),
                fixture.Build<User>().With(u => u.AccountLocked, false).Create()
            }.AsQueryable();

            var usersMock = new Mock<DbSet<User>>();
            usersMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            usersMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            usersMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            usersMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var userContextMock = new Mock<UsersContext>();
            userContextMock.Setup(x => x.Users).Returns(usersMock.Object);

            var usersService = new UsersService(userContextMock.Object);

            // Act
            var lockedUsers = usersService.GetLockedUsers();

            // Assert
            Assert.Equal(new List<User> {lockedUser}, lockedUsers);
        }

        [Fact]
        public void GetLockedUsers_Invoke_LockedUsers_v2()
        {
            // Arrange
            var fixture = new Fixture();
            var lockedUser = fixture.Build<User>().With(u => u.AccountLocked, true).Create();
            IList<User> users = new List<User>
            {
                lockedUser,
                fixture.Build<User>().With(u => u.AccountLocked, false).Create(),
                fixture.Build<User>().With(u => u.AccountLocked, false).Create()
            };

            var userContextMock = new Mock<UsersContext>();
            userContextMock.Setup(x => x.Users).Returns(users);

            var usersService = new UsersService(userContextMock.Object);

            // Act
            var lockedUsers = usersService.GetLockedUsers();

            // Assert
            Assert.Equal(new List<User> {lockedUser}, lockedUsers);
        }
    }
}