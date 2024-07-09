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

public class SectionController : ControllerBase {
    private readonly DbConnection _db;

    public SectionController(DbConnection db) {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Section>> GetSections() {
        return _db.GetAll<Section>("sections");
    }

    [HttpGet("{id}")]
    public ActionResult<Section> GetSection (int id)
    {
        var section = _db.GetById<Section>("sections", id);
        if (section == null)
        {
            return NotFound();
        }
        return section;
    }

    [HttpPut("{id}")]
    public ActionResult<Section> UpdateSection([FromBody] Section section, int id)
    {
        if (!_db.CheckIfExist(id, "sections"))
        {
            return NotFound();
        }
        _db.Update<Section>("sections", section, id);
        return Ok();
    }

    [HttpPost]
    public ActionResult<Section> AddSection([FromBody] Section section)
    {
        _db.Create<Section>("sections", section);
        return CreatedAtAction(nameof(GetSection), new { id = section.Id }, section);
    }
}