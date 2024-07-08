using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase {
        private readonly DbConnection _db;

        public BooksController(DbConnection db) {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks() {
            return _db.GetAll<Book>( "books");
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id) {
            var book = _db.GetById<Book> ("books",id);
            if (book == null) {
                return NotFound();
            }
            return book;
        }

        [HttpPut("{uid}")]
        public ActionResult<Book> UpdateBook ([FromBody] string updateColumn, string updateValue, int id) {
            if (!_db.CheckIfExist(id, updateColumn)) {
                return NotFound();
            }
            _db.UpdateModel ("books", updateColumn, updateValue, id);
            return Ok();
        }
        
        // [HttpPost]
        // public ActionResult<Book> AddBook (Book book) {
        //     _db.AddBook(book);
        //     return CreatedAtAction(nameof(GetBook), new { uid = book.Uid }, book);
        // }
}