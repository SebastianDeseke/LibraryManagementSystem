using K4os.Compression.LZ4.Internal;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly DbConnection _db;
    private readonly IConfiguration _config;
    private readonly ILogger<MemberController> _logger;
    private Member member;

    public MemberController (IConfiguration config, ILogger<MemberController> logger, DbConnection db)
    {
        _config = config;
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Member>> GetMembers()
    {
        return _db.GetAllMembers();
    }

    [HttpGet("{id}")]
    public ActionResult<Member> GetMember(int id)
    {
        member =  _db.GetMember(id);
        if (member == null)
        {
            return NotFound();
        }
        return member;
    }

    [HttpPost]
    public ActionResult<Member> AddMember ([FromBody] Member member){
        _db.Create<Member>("members", member);
        return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
    }

    [HttpPut("{id}")]
    public ActionResult<Member> UpdateMember([FromBody] Member member, int id)
    {
        if (_db.CheckIfExist(id, "members"))
        {
            _db.Update("member", member, id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}