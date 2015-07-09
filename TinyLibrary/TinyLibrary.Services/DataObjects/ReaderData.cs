using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyLibrary.Domain;
using System.Runtime.Serialization;

namespace TinyLibrary.Services.DataObjects
{
    [DataContract]
    public class ReaderData : IDataObject<Reader>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string UserName { get; set; }

        #region IDataObject<Reader> Members

        public void FromEntity(Reader entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.UserName = entity.UserName;
        }

        public Reader ToEntity()
        {
            Reader reader = new Reader();
            reader.Id = this.Id;
            reader.Name = this.Name;
            reader.UserName = this.UserName;
            return reader;
        }

        #endregion
    }
}