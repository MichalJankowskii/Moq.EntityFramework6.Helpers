namespace Moq.EntityFramework6.Helpers.DbAsyncQueryProvider
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    public class InMemeoryAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public InMemeoryAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public InMemeoryAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new InMemoryDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return this.GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider => new InMemoryAsyncQueryProvider<T>(this);
    }
}
