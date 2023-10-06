using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Presentation.Controllers;

public class AccountController : BaseController
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet("get/{userId}")]
    public async Task<ActionResult<Account>> GetUserAsync([Required] int userId)
    {
        if (userId == 0)
        {
            return BadRequest("Invalid id");
        }

        var user = await _accountService.GetByIdAsync(userId);
        return Ok(user);
    }
    
    [HttpGet("get")]
    public async Task<ActionResult<Account>> GetAllUsersAsync()
    {
        var users = await _accountService.GetAllAsync();
        return Ok(users);
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUserAsync([Required] int userId)
    {
        if (userId == 0)
        {
            return BadRequest("Invalid id");
        }

        var user = await _accountService.GetByIdAsync(userId);
        
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var response = _accountService.DeleteAsync(user);
        return Ok("User successfully deleted");
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateUser([Required] Account account) 
    {
        var responce = _accountService.CreateAsync(account);
        return Ok("User was created");
    }
    
    
}