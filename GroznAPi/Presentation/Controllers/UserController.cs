using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Presentation.Controllers;

public class UserController : BaseController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("get/{userId}")]
    public async Task<ActionResult<User>> GetUserAsync([Required] int userId)
    {
        if (userId == 0)
        {
            return BadRequest("Invalid id");
        }

        var user = await _userService.GetByIdAsync(userId);
        return Ok(user);
    }
    
    [HttpGet("get")]
    public async Task<ActionResult<User>> GetAllUsersAsync()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUserAsync([Required] int userId)
    {
        if (userId == 0)
        {
            return BadRequest("Invalid id");
        }

        var user = await _userService.GetByIdAsync(userId);
        
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var response = _userService.DeleteAsync(user);
        return Ok("User successfully deleted");
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateUser([Required] User user) 
    {
        var responce = _userService.CreateAsync(user);
        return Ok("User was created");
    }
    
    
}