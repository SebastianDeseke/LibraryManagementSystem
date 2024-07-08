using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase {
    private readonly DbController _db;

    public LoanController(DbController db) {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BookLoan>> GetLoans() {
        return _db.GetAll<BookLoan>("loans");
    }
}