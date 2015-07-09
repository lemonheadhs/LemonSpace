using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyLibrary.WebApp.TinyLibraryService;

namespace TinyLibrary.WebApp.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TinyLibraryServiceClient svcClient = new TinyLibraryServiceClient();
            var allBooks = svcClient.GetAllBooks();
            svcClient.Close();
            return View("Index", allBooks);
        }

        public ActionResult AddBook()
        {
            return View();
        }

        public ActionResult BookDetails(Guid id)
        {
            TinyLibraryServiceClient svcClient = new TinyLibraryServiceClient();
            BookData bd = svcClient.GetBookDetail(id);
            svcClient.Close();
            return View(bd);
        }

        [Authorize]
        public ActionResult MyBooks()
        {
            TinyLibraryServiceClient svcClient = new TinyLibraryServiceClient();
            var registrationData = svcClient.GetReaderRegistrations(User.Identity.Name);
            svcClient.Close();
            return View(registrationData);
        }

        [Authorize]
        public ActionResult BookBorrow(Guid id)
        {
            TinyLibraryServiceClient svcClient = new TinyLibraryServiceClient();
            svcClient.Borrow(User.Identity.Name, id);
            svcClient.Close();
            return View();
        }

        [Authorize]
        public ActionResult BookReturn(Guid id)
        {
            TinyLibraryServiceClient svcClient = new TinyLibraryServiceClient();
            svcClient.Return(User.Identity.Name, id);
            svcClient.Close();
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(BookData bookData)
        {
            TinyLibraryServiceClient svcClient = new TinyLibraryServiceClient();
            bookData.Lent = false;
            bookData.Id = Guid.NewGuid();
            svcClient.AddBook(bookData);
            svcClient.Close();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
