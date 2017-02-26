namespace Moq.EntityFramework6.Helpers.DbAsyncQueryProvider
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class InMemoryDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> innerEnumerator;

        public InMemoryDbAsyncEnumerator(IEnumerator<T> enumerator)
        {
            this.innerEnumerator = enumerator;
        }

        public void Dispose()
        {
            this.innerEnumerator.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(this.innerEnumerator.MoveNext());
        }

        public T Current => this.innerEnumerator.Current;

        object IDbAsyncEnumerator.Current => this.Current;
    }
}
