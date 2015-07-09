using System.Linq;
using Apworks.Core;
using Apworks.Domain;
using System;

namespace TinyLibrary.Domain
{
    public partial class Book : IAggregateRoot
    {
        public Book()
        {
            this.Id = ObjectContainer.Instance.GetService<IIdentityGenerator>().Generate();
        }

        public Reader LendTo
        {
            get
            {
                if (this.Lent)
                {
                    var query = from r in this.Registrations
                                where r.Book == this &&
                                r.RegistrationStatus == RegistrationStatus.Normal
                                select r;
                    if (query != null && query.Count() > 0)
                        return query.First().Reader;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime LendDate
        {
            get
            {
                if (this.Lent)
                {
                    var query = from r in this.Registrations
                                where r.Book == this &&
                                r.RegistrationStatus == RegistrationStatus.Normal
                                select r;
                    if (query != null && query.Count() > 0)
                        return query.First().Date;
                    else
                        return DateTime.MinValue;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        #region IAggregateRoot Members

        public string AggregateName
        {
            get { return this.GetType().Name; }
        }

        #endregion
    }
}
