using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyLibrary.Domain;
using Apworks.Domain.Repositories;
using Apworks.Domain;
using Apworks.Core;

namespace TinyLibrary.Repositories
{
    public class EdmTransactionContext : IRepositoryTransactionContext
    {
        private TinyLibraryContainer container = new TinyLibraryContainer();

        #region IRepositoryTransactionContext Members

        public void BeginTransaction()
        {
            
        }

        public void Commit()
        {
            container.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IAggregateRoot
        {
            try
            {
                EdmRepository<TEntity> r = ObjectContainer.Instance.GetService<IRepository<TEntity>>() as EdmRepository<TEntity>;
                r.Container = container;
                return r;
            }
            catch
            {
                throw;
            }
        }

        public void Rollback()
        {
            
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            container.Dispose();
        }

        #endregion
    }
}
