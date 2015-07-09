using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apworks.Domain;

namespace TinyLibrary.Services
{
    public interface IDataObject<TEntity> where TEntity : IEntity
    {
        void FromEntity(TEntity entity);
        TEntity ToEntity();
    }
}
