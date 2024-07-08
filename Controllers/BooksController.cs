using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers;

[Authorize]
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

        [HttpGet("{uid}")]
        public ActionResult<Book> GetBook(int uid) {
            var book = _db.GetBook(uid);
            if (book == null) {
                return NotFound();
            }
            return book;
        }

        [HttpPut("{uid}")]
        public ActionResult<Book> UpdateBook ([FromBody] string updateColumn, string updateValue) {
            _db.UpdateModel ("books", updateColumn, updateValue);
            return Ok();
        }
        
        // [HttpPost]
        // public ActionResult<Book> AddBook (Book book) {
        //     _db.AddBook(book);
        //     return CreatedAtAction(nameof(GetBook), new { uid = book.Uid }, book);
        // }
}