using K4os.Compression.LZ4.Internal;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<MemberController> _logger;
    private Member member;

    public MemberController (IConfiguration config, ILogger<MemberController> logger, LibraryContext context)
    {
        _context = context;
        _config = config;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
    {
        return await _context.Members.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetMember(int id)
    {
        member = await _context.Members.FindAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return member;
    }
}