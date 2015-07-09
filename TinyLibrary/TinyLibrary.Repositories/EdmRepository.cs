using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apworks.Domain.Repositories;
using Apworks.Domain;
using TinyLibrary.Domain;
using Apworks.Domain.Specifications;

namespace TinyLibrary.Repositories
{
    public abstract class EdmRepository<TEntity> : Repository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        protected TinyLibraryContainer container;

        public TinyLibraryContainer Container
        {
            set { this.container = value; }
        }


        protected override TEntity DoGet(ISpecification<TEntity> specification)
        {
            return this.DoFind(specification);
        }

        protected override IEnumerable<TEntity> DoGetAll()
        {
            return this.DoFindAll();
        }

        protected override IEnumerable<TEntity> DoGetAll(ISpecification<TEntity> specification)
        {
            return this.DoFindAll(specification);
        }
    }
}
