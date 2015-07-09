using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using TinyLibrary.Domain;

namespace TinyLibrary.Services.DataObjects
{
    [DataContract]
    public class BookData : IDataObject<Book>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Publisher { get; set; }

        [DataMember]
        public DateTime PubDate { get; set; }

        [DataMember]
        public string ISBN { get; set; }

        [DataMember]
        public int Pages { get; set; }

        [DataMember]
        public bool Lent { get; set; }

        [DataMember]
        public string LendTo { get; set; }

        [DataMember]
        public DateTime LendDate { get; set; }

        #region IDataObject<Book> Members

        public void FromEntity(Book entity)
        {
            this.Id = entity.Id;
            this.ISBN = entity.ISBN;
            this.Pages = entity.Pages;
            this.PubDate = entity.PubDate;
            this.Publisher = entity.Publisher;
            this.Title = entity.Title;
            this.Lent = entity.Lent;
            if (entity.LendTo != null)
            {
                this.LendTo = entity.LendTo.Name;
                this.LendDate = entity.LendDate;
            }
            else
            {
                this.LendTo = string.Empty;
                this.LendDate = DateTime.MinValue;
            }
        }

        public Book ToEntity()
        {
            Book b = new Book();
            b.Id = this.Id;
            b.ISBN = this.ISBN;
            b.Pages = this.Pages;
            b.PubDate = this.PubDate;
            b.Publisher = this.Publisher;
            b.Title = this.Title;
            return b;
        }

        #endregion
    }
}