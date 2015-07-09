using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyLibrary.Domain;
using Apworks.Domain.Specifications;

namespace TinyLibrary.Repositories
{
    public class BookRepository : EdmRepository<Book>
    {

        protected override void DoAdd(Book entity)
        {
            container.Books.AddObject(entity);
        }

        protected override bool DoExists(ISpecification<Book> specification)
        {
            var query = container.Books.Where(specification.GetExpression());
            return query.Count() > 0;
        }

        protected override Book DoFind(ISpecification<Book> specification)
        {
            var query = container.Books.Where(specification.GetExpression());
            if (query.Count() > 0)
                return query.First();
            return null;
        }

        protected override IEnumerable<Book> DoFindAll()
        {
            return container.Books;
        }

        protected override IEnumerable<Book> DoFindAll(ISpecification<Book> specification)
        {
            return container.Books.Where(specification.GetExpression());
        }

        protected override Book DoGetByKey(object key)
        {
            Guid k = (Guid)key;
            var query = from b in container.Books
                        where b.Id.Equals(k)
                        select b;
            if (query.Count() > 0)
                return query.First();
            return null;
        }

        protected override void DoRemove(Book entity)
        {
            container.Books.DeleteObject(entity);
        }

        protected override void DoUpdate(Book entity)
        {
            var b = this.GetByKey(entity.Id);
            b = entity;
        }
    }
}
