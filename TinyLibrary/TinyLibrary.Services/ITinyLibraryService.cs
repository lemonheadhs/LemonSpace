using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TinyLibrary.Services.DataObjects;

namespace TinyLibrary.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITinyLibraryService
    {
        [OperationContract]
        string SayHello();

        [OperationContract]
        IEnumerable<BookData> GetAllBooks();

        [OperationContract]
        void AddBook(BookData bookData);

        [OperationContract]
        bool AddReader(ReaderData readerData);

        [OperationContract]
        BookData GetBookDetail(Guid id);

        [OperationContract]
        IEnumerable<RegistrationData> GetReaderRegistrations(string readerUserName);

        [OperationContract]
        void Borrow(string readerUserName, Guid bookId);

        [OperationContract]
        void Return(string readerUserName, Guid bookId);
    }

}
