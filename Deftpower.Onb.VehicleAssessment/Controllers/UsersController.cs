using Deftpower.Onb.VehicleAssessment.Models;
using Deftpower.Onb.VehicleAssessment.Repositories;
using Deftpower.Onb.VehicleAssessment.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Deftpower.Onb.VehicleAssessment.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController (IUserRepository userRepository): ControllerBase
{
    // POST /api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] AddUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Prevent duplicate emails
        if (await userRepository.EmailExistsAsync(request.Email))
            return Conflict(new { message = "A user with that email already exists." });

        var user = new User
        {
            Email = request.Email,
            Name = request.Name,
            CreatedAt = DateTime.UtcNow
        };

        var created = await userRepository.AddUserAsync(user);

        // Return 201 with location header (GetUserById below)
        return CreatedAtAction(nameof(GetUserById), new { id = created.Id }, created);
    }

    // GET /api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // POST /api/users/{userId}/forbidden
    [HttpPost("{userId}/forbidden")]
    public async Task<IActionResult> AddForbiddenUser(string userId, [FromBody] AddForbiddenUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await userRepository.UserExistsAsync(userId))
            return NotFound(new { message = "User not found." });

        var forbidden = await userRepository.AddForbiddenUserAsync(userId, request.Reason);

        // You can return the created forbidden record or the user's updated resource
        return CreatedAtAction(nameof(GetUserById), new { id = userId }, forbidden);
    }
    
    [HttpGet("forbidden")]
    public async Task<IActionResult> GetAllForbiddenUsers()
    {
        var forbiddenUsers = await userRepository.GetAllForbiddenUsersAsync();
        return Ok(forbiddenUsers);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var allUsers = await userRepository.GetAllUsersAsync();
        return Ok(allUsers);
    }
}