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

public class ShelfController : ControllerBase {
    private readonly DbConnection _db;

    public ShelfController(DbConnection db) {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Shelf>> GetShelves() {
        return _db.GetAll<Shelf>("shelves");
    }

    [HttpGet("{id}")]
    public ActionResult<Shelf> GetShelf (int id)
    {
        var shelf = _db.GetById<Shelf>("shelves", id);
        if (shelf == null)
        {
            return NotFound();
        }
        return shelf;
    }

    [HttpPut("{id}")]
    public ActionResult<Shelf> UpdateShelf([FromBody] Shelf shelf, int id)
    {
        if (!_db.CheckIfExist(id, "shelves"))
        {
            return NotFound();
        }
        _db.Update<Shelf>("shelves", shelf, id);
        return Ok();
    }

    [HttpPost]
    public ActionResult<Shelf> AddShelf([FromBody] Shelf shelf)
    {
        _db.Create<Shelf>("shelves", shelf);
        return CreatedAtAction(nameof(GetShelf), new { id = shelf.Id }, shelf);
    }
}