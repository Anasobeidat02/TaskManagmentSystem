using Domains.DTOs.User;
using Domains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _userSerivce;

        public UsersController(IUser userSerivce)
        {
            _userSerivce = userSerivce;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Invalid login data.");

                var result = await _userSerivce.Login(request);
                
                if (result == null)
                    return Unauthorized("Invalid email or password.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost] 
        public async Task<IActionResult> AddUser(AddUserRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Invalid user data.");

                await _userSerivce.Add(request);
                return Ok("User added successfully.");
            }
            catch (Exception)
            {
                 return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the user.");
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userSerivce.GetAll();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving users.");
            }
        }
         
        [HttpGet]
        [Authorize]
        [Route("get-current-user-info")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            try
            {
                var data = User.Claims.FirstOrDefault(); 
                var userIdFromToken = data != null ? int.Parse(data.Value) : 0;

                var user = await _userSerivce.GetById(userIdFromToken);
                if (user == null)
                    return NotFound("User not found.");

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the user.");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {  
                var user = await _userSerivce.GetById(id);
                if (user == null)
                    return NotFound("User not found.");

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the user.");
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Invalid user ID.");

                var result = await _userSerivce.Delete(id);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting the user - {ex.Message}.");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                if (request == null || request.Id == 0)
                    return BadRequest("Invalid input data.");

                await _userSerivce.Update(request);
                return Ok("User updated successfully.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the user.");
            }
        }
    }
}
