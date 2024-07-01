using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase {
    private readonly LibraryContext _context;

    public BooksController(LibraryContext context) {
        _context = context;
    }
}