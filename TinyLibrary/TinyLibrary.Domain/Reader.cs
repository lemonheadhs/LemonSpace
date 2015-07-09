using System;
using System.Collections.Generic;
using System.Linq;
using Apworks.Core;
using Apworks.Domain;

namespace TinyLibrary.Domain
{
    public partial class Reader : IAggregateRoot
    {
        public Reader()
        {
            this.Id = ObjectContainer.Instance.GetService<IIdentityGenerator>().Generate();
        }

        public void Borrow(Book book)
        {
            if (book.Lent)
                throw new InvalidOperationException("The book has been lent.");
            Registration reg = new Registration();
            reg.RegistrationStatus = RegistrationStatus.Normal;
            reg.Book = book;
            reg.Date = DateTime.Now;
            reg.DueDate = reg.Date.AddDays(90);
            reg.ReturnDate = DateTime.MaxValue;
            book.Registrations.Add(reg);
            book.Lent = true;
            this.Registrations.Add(reg);
        }

        public void Return(Book book)
        {
            if (!book.Lent)
                throw new InvalidOperationException("The book has not been lent.");
            var q = from r in this.Registrations
                    where r.Book.Id.Equals(book.Id) &&
                    r.RegistrationStatus == RegistrationStatus.Normal
                    select r;
            if (q.Count() > 0)
            {
                var reg = q.First();
                if (reg.Expired)
                {
                    // TODO: Reader should pay for the expiration.
                }
                reg.ReturnDate = DateTime.Now;
                reg.RegistrationStatus = RegistrationStatus.Returned;
                book.Lent = false;
            }
            else
                throw new InvalidOperationException(string.Format("Reader {0} didn't borrow this book.",
                    this.Name));
        }

        public IEnumerable<Registration> GetExpiredRegistrations()
        {
            return from r in this.Registrations
                   where r.Expired
                   select r;
        }

        #region IAggregateRoot Members

        public string AggregateName
        {
            get { return this.GetType().Name; }
        }

        #endregion
    }
}
