using LinhND_BaseAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Entities;
using Service.Services.Base;

namespace LinhND_BaseAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthorController : BaseController
    {

        public AuthorController(IServiceManager serviceManager,IHttpContextAccessor contextAccessor) : base(serviceManager) { }

        /// <summary>
        /// Retrieves a list of all authors.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of authors available in the system.
        /// </remarks>
        /// <returns>A list of categories.</returns>
        /// <response code="200">Returns the list of authors.</response>
        /// <response code="404">If no categories are found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IEnumerable<AuthorEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAuthor()
        {
            var result = await _serviceManager.AuthorService.GetAllAuthors();
            if (result.Count == 0)
            {
                return NotFound(DevMessageConstants.OBJECT_IS_EMPTY);
            }
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of all authors paginated with page size and page number.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of authors available in the system paginated with page size and page number.
        /// </remarks>
        /// <returns>A list of categories.</returns>
        /// <response code="200">Returns the list of authors.</response>
        /// <response code="404">If no categories are found.</response>
        [HttpGet("get-all-paginated")]
        [ProducesResponseType(typeof(IEnumerable<AuthorEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAuthorPaginated([FromQuery] int page, [FromQuery] int pageSize)
        {

            if(page == 0 || pageSize == 0)
            {
                page = 1;
                pageSize = 2;
            }

            var result = await _serviceManager.AuthorService.GetAuthorListPaginated(page, pageSize);
            if (result.Count == 0)
            {
                return NotFound(DevMessageConstants.OBJECT_IS_EMPTY);
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
        /// <response code="404">If added failed.</response>
        [Authorize(Roles = "ROLE_LIBRARIAN")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAutor([FromBody] AuthorDTO newAuthor)
        {
            var result = await _serviceManager.AuthorService.CreateAuthor(newAuthor);
            if (!result)
            {
                return NotFound(DevMessageConstants.ADD_OBJECT_FAILED);
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
        /// <response code="404">If update failed.</response>
        [Authorize(Roles = "ROLE_LIBRARIAN")]
        [HttpPut("update/{authorId}")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAuthor([FromRoute] long authorId, [FromBody] AuthorDTO newAuthor)
        {
            newAuthor.id = authorId;
            var result = await _serviceManager.AuthorService.UpdateAuthor(newAuthor);
            if (!result)
            {
                return NotFound(DevMessageConstants.NOTIFICATION_UPDATE_FAILED);
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
        /// <response code="404">If delete failed.</response>
        [Authorize(Roles = "ROLE_LIBRARIAN")]
        [HttpDelete("delete/{authorId}")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAuthor([FromRoute] long authorId)
        {
            var result = await _serviceManager.AuthorService.DeleteAuthor(authorId);
            if (!result)
            {
                return NotFound(DevMessageConstants.NOTIFICATION_DELETE_FAILED);
            }
            return Ok(DevMessageConstants.NOTIFICATION_DELETE_SUCCESS);
        }


    }
}
