using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase {

}