using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase {
    private readonly DbConnection _db;

    public LoanController(DbConnection db) {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BookLoan>> GetLoans() {
        return _db.GetAll<BookLoan>("loans");
    }
}