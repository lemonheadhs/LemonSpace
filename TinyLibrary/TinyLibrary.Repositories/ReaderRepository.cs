using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyLibrary.Domain;
using Apworks.Domain.Specifications;

namespace TinyLibrary.Repositories
{
    public class ReaderRepository : EdmRepository<Reader>
    {
        protected override void DoAdd(Reader entity)
        {
            container.Readers.AddObject(entity);
        }

        protected override bool DoExists(ISpecification<Reader> specification)
        {
            var query = container.Readers.Where(specification.GetExpression());
            return query.Count() > 0;
        }

        protected override Reader DoFind(ISpecification<Reader> specification)
        {
            var query = container.Readers.Where(specification.GetExpression());
            if (query.Count() > 0)
                return query.First();
            return null;
        }

        protected override IEnumerable<Reader> DoFindAll()
        {
            return container.Readers;
        }

        protected override IEnumerable<Reader> DoFindAll(ISpecification<Reader> specification)
        {
            return container.Readers.Where(specification.GetExpression());
        }

        protected override Reader DoGetByKey(object key)
        {
            Guid id = (Guid)key;
            var query = from r in container.Readers
                        where r.Id.Equals(id)
                        select r;
            if (query.Count() > 0)
                return query.First();
            return null;
        }

        protected override void DoRemove(Reader entity)
        {
            container.Readers.DeleteObject(entity);
        }

        protected override void DoUpdate(Reader entity)
        {
            var r = this.GetByKey(entity.Id);
            r = entity;
        }
    }
}
