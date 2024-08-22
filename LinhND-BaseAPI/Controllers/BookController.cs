using LinhND_BaseAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Entities;
using Service.Services.Base;
using System.Net;
using System.Security.Claims;

namespace LinhND_BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {

        public BookController(IServiceManager serviceManager) : base(serviceManager) { }

        /// <summary>
        /// Retrieves a list of all books.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of books available in the system.
        /// </remarks>
        /// <returns>A list of books.</returns>
        /// <response code="200">Returns the list of books.</response>
        /// <response code="400">If no books are found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _serviceManager.BookService.GetAllBook();
            if (result.Count == 0)
            {
                return BadRequest(DevMessageConstants.OBJECT_IS_EMPTY);
            }
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of all books.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of books filtered by keyword available in the system.
        /// </remarks>
        /// <returns>A list of books filtered by keyword.</returns>
        /// <response code="200">Returns the list of books filtered by keyword.</response>
        /// <response code="400">If no books are found.</response>
        [HttpGet("get-all-filtered")]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FilterBooks([FromQuery] string keyword)
        {
            var result = await _serviceManager.BookService.FilterBook(keyword);
            if (result == null)
            {
                return BadRequest(DevMessageConstants.OBJECT_IS_EMPTY);
            }
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of all books paginated with page size and page number.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of books available in the system paginated with page size and page number.
        /// </remarks>
        /// <returns>A list of books.</returns>
        /// <response code="200">Returns the list of books.</response>
        /// <response code="400">If no books are found.</response>
        [HttpGet("get-all-paginated")]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAuthorPaginated([FromQuery] int page, [FromQuery] int pageSize)
        {

            if(page == 0 || pageSize == 0)
            {
                page = 1;
                pageSize = 2;
            }

            var result = await _serviceManager.BookService.GetAllBookPaginated(page, pageSize);
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
        [Authorize(Roles = "ROLE_LIBRARIAN, ROLE_ADMIN")]
        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBook([FromForm] BookRequestDTO newBook)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User not found.");
            }

            var result = await _serviceManager.BookService.AddBook(newBook, username);
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
        [Authorize(Roles = "ROLE_LIBRARIAN, ROLE_ADMIN")]
        [HttpPut("update/{bookId}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBook([FromForm] BookRequestDTO updatedBook, [FromRoute] long bookId)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User not found.");
            }

            var result = await _serviceManager.BookService.UpdateBook(updatedBook, bookId, username);
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
        [Authorize(Roles = "ROLE_LIBRARIAN, ROLE_ADMIN")]
        [HttpDelete("delete/{bookId}")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBook([FromRoute] long bookId)
        {

            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User not found.");
            }

            var result = await _serviceManager.BookService.DeleteBook(bookId);
            if (!result)
            {
                return BadRequest(DevMessageConstants.NOTIFICATION_DELETE_FAILED);
            }
            return Ok(DevMessageConstants.NOTIFICATION_DELETE_SUCCESS);


        }

    }
}
