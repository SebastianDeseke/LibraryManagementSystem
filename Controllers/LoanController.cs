using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly DbConnection _db;

    public LoanController(DbConnection db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BookLoan>> GetLoans()
    {
        return _db.GetAll<BookLoan>("loans");
    }

    [HttpGet("{id}")]
    public ActionResult<BookLoan> GetLoan(int id)
    {
        var loan = _db.GetById<BookLoan>("loans", id);
        if (loan == null)
        {
            return NotFound();
        }
        return loan;
    }

    [HttpPut("{id}")]
    public ActionResult<BookLoan> UpdateLoan(int id, [FromBody] BookLoan loan)
    {
        if (_db.CheckIfExist(id, "loans"))
        {
            _db.Update<BookLoan>("loans", loan, id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}