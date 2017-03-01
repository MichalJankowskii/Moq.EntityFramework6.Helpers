namespace Moq.EntityFramework6.Helpers.DbAsyncQueryProvider
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    public class InMemoryAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public InMemoryAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public InMemoryAsyncEnumerable(Expression expression)
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
