using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly DbConnection _db;

    public BooksController(DbConnection db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return _db.GetAll<Book>("books");
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = _db.GetById<Book>("books", id);
        if (book == null)
        {
            return NotFound();
        }
        return book;
    }

    [HttpPut("{id}")]
    public ActionResult<Book> UpdateBook([FromBody] Book book, int id)
    {
        if (!_db.CheckIfExist(id, "books"))
        {
            return NotFound();
        }
        _db.Update<Book>("books", book, id);
        return Ok();
    }

    [HttpPost]
    public ActionResult<Book> AddBook([FromBody] Book book)
    {
        _db.Create<Book>("books", book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut ("{sid}/add-to-shelf")]
    public ActionResult<Book> AddToShelf ([FromBody] Book book, int sid)
    {
        _db.AddBookInShelf(book.Id, sid);
        return Ok();
    }
}