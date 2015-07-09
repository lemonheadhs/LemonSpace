using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyLibrary.Domain;
using System.Runtime.Serialization;

namespace TinyLibrary.Services.DataObjects
{
    [DataContract]
    public class RegistrationData
    {
        [DataMember]
        public Guid BookGuid { get; set; }

        [DataMember]
        public string BookTitle { get; set; }

        [DataMember]
        public string BookISBN { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public DateTime DueDate { get; set; }

        [DataMember]
        public DateTime ReturnDate { get; set; }

        [DataMember]
        public string ReaderUserName { get; set; }

        [DataMember]
        public string ReaderName { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string StatusText { get; set; }

    }
}