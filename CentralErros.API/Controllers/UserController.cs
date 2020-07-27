using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/user")]
[ApiController]
[Authorize("Bearer")]
public sealed class UserController : ControllerBase
{
    private readonly ILoggedUserRepository _loggedUserRepository;
    public UserController(ILoggedUserRepository loggedUserRepository)
    {
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    public ActionResult<BaseResult<User>> Get()
    {
        var user = _loggedUserRepository.GetLoggedUser();
        return Ok(user);
    }
}