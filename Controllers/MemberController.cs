using K4os.Compression.LZ4.Internal;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly DbController _db;
    private readonly IConfiguration _config;
    private readonly ILogger<MemberController> _logger;
    private Member member;

    public MemberController (IConfiguration config, ILogger<MemberController> logger, DbController db)
    {
        _config = config;
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
    {
        return _db.GetAllMembers();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetMember(int id)
    {
        member =  _db.GetMember(id);
        if (member == null)
        {
            return NotFound();
        }
        return member;
    }
}