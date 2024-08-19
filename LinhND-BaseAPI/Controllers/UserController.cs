using LinhND_BaseAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Services.Base;

namespace LinhND_BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {

        public UserController(IServiceManager serviceManager) : base(serviceManager) { }    

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var isUserRegistered = await _serviceManager.UserService.CreateUser(userDTO);
            if (!isUserRegistered)
            {
                return BadRequest(DevMessageConstants.ADD_OBJECT_FAILED);
            }
            return Ok(DevMessageConstants.ADD_OBJECT_SUCCESS);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LogInDTO loginParam)
        {
            var authenRes = await _serviceManager.TokenService.Login(loginParam.username, loginParam.password);

            if (authenRes is null) return BadRequest();

            // add the tokens to the response cookie
            // jwt will be in httponly cookie, means it stays in back end and front end cant access it
            Response.Cookies.Append("jwt", authenRes, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(authenRes);
        }

        /// <summary>
        /// Retrieves a message dictates that whether the object is deleted or not.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a message dictates that whether the object is deleted or not.
        /// </remarks>
        /// <returns>A message.</returns>
        /// <response code="200">Returns a message.</response>
        /// <response code="404">If delete failed.</response>
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut("update-role")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromQuery] long id, string role)
        {
            if (role == "ROLE_ADMIN" || role == "ROLE_LIBRARIAN" || role == "ROLE_MEMBER")
            {
                var user = await _serviceManager.UserService.GetUserById(id);
                if (user == null)
                {
                    return NotFound(DevMessageConstants.NOTIFICATION_DELETE_FAILED);
                }
                var result = await _serviceManager.UserService.UpdateUserWithEntity(user);
                return Ok(DevMessageConstants.NOTIFICATION_DELETE_SUCCESS);
            }

            return BadRequest(DevMessageConstants.NOT_VALID);
            
        }




    }
}
