using System.ComponentModel.DataAnnotations;
using Contracts.Account;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Abstactions;

namespace Presentation.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<AccountDto>> GetIdentity()
    {
        return await _accountService.GetByIdAsync(AccountId);
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
    
    [HttpGet("get-all")]
    public async Task<ActionResult<Account>> GetAllUsersAsync()
    {
        var users = await _accountService.GetAllAsync();
        return Ok(users);
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult<AccountDeletedResponseDto>> DeleteUserAsync([Required] DeleteAccountRequestDto request)
    {
        if (request.Id <= 0)
        {
            return BadRequest("Invalid id");
        }

        var user = await _accountService.GetByIdAsync(request.Id);
        
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var response = _accountService.DeleteAsync(request);
        return Ok(response);
    }
}