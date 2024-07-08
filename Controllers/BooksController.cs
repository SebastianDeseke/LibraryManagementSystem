using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase {

        private readonly DbController _db;

        public BooksController(DbController db) {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks() {
            return _db.GetAll<Book>( "books");
        }
}