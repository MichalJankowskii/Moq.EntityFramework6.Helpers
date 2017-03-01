namespace Moq.EntityFramework.Helpers.Tests.DbAsyncQueryProvider
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using Helpers.DbAsyncQueryProvider;
    using Xunit;

    public class InMemoryAsyncEnumerableTests
    {
        [Fact]
        public void
            Given_InMemoryAsyncEnumerable_When_GetAsyncEnumerator_Then_EnumeratorFromInInnerEnumerableShouldBeUsed()
        {
            // Arrange
            var enumerableMock = new Mock<IEnumerable<int>>();
            var inMemoryAsyncEnumerable = new InMemoryAsyncEnumerable<int>(enumerableMock.Object);

            // Act
            inMemoryAsyncEnumerable.GetAsyncEnumerator();

            // Assert
            enumerableMock.Verify(x => x.GetEnumerator());
        }

        [Fact]
        public void
            Given_InMemoryAsyncEnumerable_When_GetAsyncEnumeratorFromInterface_Then_EnumeratorFromInInnerEnumerableShouldBeUsed
            ()
        {
            // Arrange
            var enumerableMock = new Mock<IEnumerable<int>>();
            var inMemoryAsyncEnumerable = new InMemoryAsyncEnumerable<int>(enumerableMock.Object);

            // Act
            ((IDbAsyncEnumerable) inMemoryAsyncEnumerable).GetAsyncEnumerator();

            // Assert
            enumerableMock.Verify(x => x.GetEnumerator());
        }

        [Fact]
        public void Given_InMemoryAsyncEnumerable_When_GetProviderFromInterface_Then_CorrectProviderIsReturned()
        {
            // Arrange
            var enumerableMock = new Mock<IEnumerable<int>>();
            var inMemoryAsyncEnumerable = new InMemoryAsyncEnumerable<int>(enumerableMock.Object);

            // Act
            var provider = ((IQueryable) inMemoryAsyncEnumerable).Provider;

            // Assert
            Assert.IsType<InMemoryAsyncQueryProvider<int>>(provider);
        }

        [Fact]
        public void Given_Enumerable_When_ObjectCreated_Then_ObjectCorrectlyBuild()
        {
            // Arrange
            var enumerableMock = new Mock<IEnumerable<int>>();

            // Act
            var inMemoryAsyncEnumerable = new InMemoryAsyncEnumerable<int>(enumerableMock.Object);

            // Assert
            Assert.Equal(enumerableMock.Object.GetEnumerator(), ((IEnumerable<int>)inMemoryAsyncEnumerable).GetEnumerator());
        }

        [Fact]
        public void Given_Expression_When_ObjectCreated_Then_ObjectCorrectlyBuild()
        {
            // Arrange
            var expressionMock = new Mock<Expression>().Object;

            // Act
            var inMemoryAsyncEnumerable = new InMemoryAsyncEnumerable<int>(expressionMock);

            // Assert
            Assert.Equal(expressionMock, ((IQueryable)inMemoryAsyncEnumerable).Expression);
        }
    }
}
