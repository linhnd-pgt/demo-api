using LinhND_BaseAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Services.Base;
using System.Security.Claims;

namespace LinhND_BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {

        public CategoryController(IServiceManager serviceManager) : base(serviceManager) { }

        /// <summary>
        /// Retrieves a list of all categories.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of categories available in the system.
        /// </remarks>
        /// <returns>A list of categories.</returns>
        /// <response code="200">Returns the list of categories.</response>
        /// <response code="400">If no categories are found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCategorys()
        {
            var result = await _serviceManager.CategoryService.GetAllCategory();
            if (result.Count == 0)
            {
                return BadRequest(DevMessageConstants.OBJECT_IS_EMPTY);
            }
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of all categories paginated with page size and page number.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of categories available in the system paginated with page size and page number.
        /// </remarks>
        /// <returns>A list of categories.</returns>
        /// <response code="200">Returns the list of categories.</response>
        /// <response code="400">If no categories are found.</response>
        [HttpGet("get-all-paginated")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAuthorPaginated([FromQuery] int page, [FromQuery] int pageSize)
        {

            if (page == 0 || pageSize == 0)
            {
                page = 1;
                pageSize = 2;
            }

            var result = await _serviceManager.CategoryService.GetAllCategoryPaginated(page, pageSize);
            if (result.Count == 0)
            {
                return BadRequest(DevMessageConstants.OBJECT_IS_EMPTY);
            }

            return Ok(result);

        }

        /// <summary>
        /// Retrieves a message dictates that whether the object is added or not.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a message dictates that whether the object is added or not.
        /// </remarks>
        /// <returns>A message.</returns>
        /// <response code="200">Returns a message.</response>
        /// <response code="400">If add failed.</response>
        [Authorize(Roles = "ROLE_LIBRARIAN")]
        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryRequestDTO newCategory)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User not found.");
            }

            var result = await _serviceManager.CategoryService.AddCategory(newCategory, username);
            if (!result)
            {
                return BadRequest(DevMessageConstants.ADD_OBJECT_FAILED);
            }
            return Ok(DevMessageConstants.ADD_OBJECT_SUCCESS);


        }

        /// <summary>
        /// Retrieves a message dictates that whether the object is updated or not.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a message dictates that whether the object is updated or not.
        /// </remarks>
        /// <returns>A message.</returns>
        /// <response code="200">Returns a message.</response>
        /// <response code="400">If update failed.</response>
        [Authorize(Roles = "ROLE_LIBRARIAN")]
        [HttpPut("update/{categoryId}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategory([FromForm] CategoryRequestDTO updatedCategory, [FromRoute] long categoryId)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User not found.");
            }

            var result = await _serviceManager.CategoryService.UpdateCategory(updatedCategory, categoryId, username);
            if (!result)
            {
                return BadRequest(DevMessageConstants.NOTIFICATION_UPDATE_FAILED);
            }
            return Ok(DevMessageConstants.NOTIFICATION_UPDATE_SUCCESS);
        }

        /// <summary>
        /// Retrieves a message dictates that whether the object is deleted or not.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a message dictates that whether the object is deleted or not.
        /// </remarks>
        /// <returns>A message.</returns>
        /// <response code="200">Returns a message.</response>
        /// <response code="400">If delete failed.</response>
        [Authorize(Roles = "ROLE_LIBRARIAN")]
        [HttpDelete("delete/{categoryId}")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCategory([FromRoute] long categoryId)
        {

            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User not found.");
            }

            var result = await _serviceManager.CategoryService.DeleteCategory(categoryId);
            if (!result)
            {
                return BadRequest(DevMessageConstants.NOTIFICATION_DELETE_FAILED);
            }
            return Ok(DevMessageConstants.NOTIFICATION_DELETE_SUCCESS);


        }

    }
}
