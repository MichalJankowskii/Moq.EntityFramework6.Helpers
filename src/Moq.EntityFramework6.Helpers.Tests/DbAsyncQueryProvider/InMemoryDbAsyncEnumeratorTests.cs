namespace Moq.EntityFramework6.Helpers.Tests.DbAsyncQueryProvider
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using Helpers.DbAsyncQueryProvider;
    using Xunit;

    public class InMemoryDbAsyncEnumeratorTests
    {
        private readonly Mock<IEnumerator<int>> enumeratorMock = new Mock<IEnumerator<int>>();
        private readonly InMemoryDbAsyncEnumerator<int> inMemoryDbAsyncEnumerator;

        public InMemoryDbAsyncEnumeratorTests()
        {
            this.inMemoryDbAsyncEnumerator = new InMemoryDbAsyncEnumerator<int>(this.enumeratorMock.Object);
        }

        [Fact]
        public void Given_InMemoryDbAsyncEnumerator_When_Dispose_Then_InnerEnumeratorShouldBeDisposed()
        {
            // Act
            this.inMemoryDbAsyncEnumerator.Dispose();

            // Assert
            this.enumeratorMock.Verify(x => x.Dispose());
        }

        [Fact]
        public void Given_InMemoryDbAsyncEnumerator_When_Current_Then_CurrentFromInInnerEnumeratorShouldBeUsed()
        {
            // Act
            int result = this.inMemoryDbAsyncEnumerator.Current;

            // Assert
            this.enumeratorMock.VerifyGet(x=> x.Current);
        }

        [Fact]
        public void Given_InMemoryDbAsyncEnumerator_When_CurrentFromInterface_Then_CurrentFromInInnerEnumeratorShouldBeUsed()
        {
            // Act
            object result = ((IDbAsyncEnumerator)this.inMemoryDbAsyncEnumerator).Current;

            // Assert
            this.enumeratorMock.VerifyGet(x => x.Current);
        }

        [Fact]
        public async void Given_InMemoryDbAsyncEnumerator_When_Current_Then_CurrentShouldBeSameAsInInnerEnumerator()
        {
            // Act
            await this.inMemoryDbAsyncEnumerator.MoveNextAsync(CancellationToken.None);

            // Assert
            this.enumeratorMock.Verify(x => x.MoveNext());
        }
    }
}
