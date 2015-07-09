using System;
using Apworks.Core;
using Apworks.Domain;

namespace TinyLibrary.Domain
{
    public partial class Registration : IEntity
    {
        public Registration()
        {
            this.Id = ObjectContainer.Instance.GetService<IIdentityGenerator>().Generate();
        }

        public RegistrationStatus RegistrationStatus
        {
            get
            {
                return (RegistrationStatus)this.Status;
            }
            set
            {
                this.Status = Convert.ToInt32(value);
            }
        }

        public bool Expired
        {
            get
            {
                return DateTime.Now > this.DueDate;
            }
        }
    }
}
