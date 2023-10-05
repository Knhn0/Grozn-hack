using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Presentation.Controllers;

public class AccountController : BaseController
{
    private readonly AcconutService _acconutService;

    public AccountController(AcconutService acconutService)
    {
        _acconutService = acconutService;
    }
    
    [HttpGet("get/{userId}")]
    public async Task<ActionResult<Account>> GetUserAsync([Required] int userId)
    {
        if (userId == 0)
        {
            return BadRequest("Invalid id");
        }

        var user = await _acconutService.GetByIdAsync(userId);
        return Ok(user);
    }
    
    [HttpGet("get")]
    public async Task<ActionResult<Account>> GetAllUsersAsync()
    {
        var users = await _acconutService.GetAllAsync();
        return Ok(users);
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUserAsync([Required] int userId)
    {
        if (userId == 0)
        {
            return BadRequest("Invalid id");
        }

        var user = await _acconutService.GetByIdAsync(userId);
        
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var response = _acconutService.DeleteAsync(user);
        return Ok("User successfully deleted");
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateUser([Required] Account account) 
    {
        var responce = _acconutService.CreateAsync(account);
        return Ok("User was created");
    }
    
    
}