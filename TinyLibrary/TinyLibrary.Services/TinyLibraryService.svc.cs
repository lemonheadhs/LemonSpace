using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TinyLibrary.Services.DataObjects;
using Apworks.Domain.Repositories;
using Apworks.Core;
using TinyLibrary.Domain;
using Apworks.Domain.Specifications;

namespace TinyLibrary.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class TinyLibraryService : ITinyLibraryService
    {
        #region ITinyLibraryService Members

        public IEnumerable<BookData> GetAllBooks()
        {
            using (IRepositoryTransactionContext ctx = ObjectContainer.Instance.GetService<IRepositoryTransactionContext>())
            {
                IRepository<Book> bookRepository = ctx.GetRepository<Book>();
                List<BookData> bds = new List<BookData>();
                var books = bookRepository.GetAll();
                foreach (var book in books)
                {
                    BookData bd = new BookData();
                    bd.FromEntity(book);
                    bds.Add(bd);
                }
                return bds;
            }
        }

        public void AddBook(BookData bookData)
        {
            using (IRepositoryTransactionContext ctx = ObjectContainer.Instance.GetService<IRepositoryTransactionContext>())
            {
                IRepository<Book> bookRepository = ctx.GetRepository<Book>();
                Book book = bookData.ToEntity();
                bookRepository.Add(book);
                ctx.Commit();
            }
        }

        public bool AddReader(ReaderData readerData)
        {
            try
            {
                using (IRepositoryTransactionContext ctx = ObjectContainer.Instance.GetService<IRepositoryTransactionContext>())
                {
                    IRepository<Reader> readerRepository = ctx.GetRepository<Reader>();
                    Reader reader = readerData.ToEntity();
                    readerRepository.Add(reader);
                    ctx.Commit();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public BookData GetBookDetail(Guid id)
        {
            try
            {
                using (IRepositoryTransactionContext ctx = ObjectContainer.Instance.GetService<IRepositoryTransactionContext>())
                {
                    IRepository<Book> bookRepository = ctx.GetRepository<Book>();
                    Book bk = bookRepository.GetByKey(id);
                    BookData bd = new BookData();
                    bd.FromEntity(bk);
                    return bd;
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<RegistrationData> GetReaderRegistrations(string readerUserName)
        {
            try
            {
                using (IRepositoryTransactionContext ctx = ObjectContainer.Instance.GetService<IRepositoryTransactionContext>())
                {
                    IRepository<Reader> readerRepository = ctx.GetRepository<Reader>();
                    Reader reader = readerRepository.Find(Specification<Reader>.Eval(r => r.UserName.Equals(readerUserName)));
                    var registrations = reader.Registrations;
                    List<RegistrationData> ret = new List<RegistrationData>();
                    foreach (var registration in registrations)
                    {
                        RegistrationData rd = new RegistrationData
                        {
                            BookGuid = registration.Book.Id,
                            BookISBN = registration.Book.ISBN,
                            BookTitle = registration.Book.Title,
                            Date = registration.Date,
                            DueDate = registration.DueDate,
                            ReaderName = reader.Name,
                            ReaderUserName = reader.UserName,
                            ReturnDate = registration.ReturnDate,
                            Status = registration.Status,
                        };
                        if (registration.Expired)
                        {
                            rd.Status = -1;
                            rd.StatusText = "Expired";
                        }
                        else
                        {
                            switch (registration.RegistrationStatus)
                            {
                                case RegistrationStatus.Normal:
                                    rd.StatusText = "Normal";
                                    break;
                                case RegistrationStatus.Returned:
                                    rd.StatusText = "Returned";
                                    break;
                                default: break;
                            }
                        }
                        ret.Add(rd);
                    }
                    return ret;
                }
            }
            catch
            {
                throw;
            }
        }

        public void Borrow(string readerUserName, Guid bookId)
        {
            try
            {
                using (IRepositoryTransactionContext ctx = ObjectContainer.Instance.GetService<IRepositoryTransactionContext>())
                {
                    IRepository<Book> bookRepository = ctx.GetRepository<Book>();
                    IRepository<Reader> readerRepository = ctx.GetRepository<Reader>();
                    Reader reader = readerRepository.Find(Specification<Reader>.Eval(r => r.UserName.Equals(readerUserName)));
                    Book book = bookRepository.GetByKey(bookId);
                    reader.Borrow(book);
                    ctx.Commit();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Return(string readerUserName, Guid bookId)
        {
            try
            {
                using (IRepositoryTransactionContext ctx = ObjectContainer.Instance.GetService<IRepositoryTransactionContext>())
                {
                    IRepository<Book> bookRepository = ctx.GetRepository<Book>();
                    IRepository<Reader> readerRepository = ctx.GetRepository<Reader>();
                    Reader reader = readerRepository.Find(Specification<Reader>.Eval(r => r.UserName.Equals(readerUserName)));
                    Book book = bookRepository.GetByKey(bookId);
                    reader.Return(book);
                    ctx.Commit();
                }
            }
            catch
            {
                throw;
            }
        }

        public string SayHello()
        {
            return "Hello";
        }

        #endregion
    }
}
