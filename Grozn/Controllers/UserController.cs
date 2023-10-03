using System.ComponentModel.DataAnnotations;
using Grozn.Domain.Entities;
using Grozn.Services;
using Microsoft.AspNetCore.Mvc;

namespace Grozn.Controllers;

public class UserController : BaseController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<User>> CreateUser([Required] User user)
    {
        _userService.CreateAsync(user);
        return Ok("User was successfully created");
    }
    
    [HttpGet("get/{userId}")]
    public async Task<ActionResult<User>> GetUserAsync([Required] int userId)
    {
        if (userId == 0)
        {
            return BadRequest("Id is not valid");
        }

        var user = _userService.GetByIdAsync(userId);
        return Ok(user);
    }
    
    [HttpGet("get")]
    public async Task<ActionResult<User>> GetAllUsersAsync()
    {
        var users = _userService.GetAllAsync();
        return Ok(users);
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<User>> UpdateUserAsync([Required]int userId, User user)
    {
        var res = _userService.GetByIdAsync(userId);
        if (userId == 0)
        {
            return BadRequest("Id is not valid");
        }

        var u = _userService.UpdateAsync(user);
        return Ok(u);
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUserAsync([Required] int userId)
    {
        if (userId == 0)
        {
            return BadRequest("Id is not valid");
        }

        var user = _userService.GetByIdAsync(userId);
        
        if (user == null)
        {
            return BadRequest("User not found");
        }
        _userService.RemoveAsync(user.Id);
        return Ok("User successfully deleted");
    }
    
}