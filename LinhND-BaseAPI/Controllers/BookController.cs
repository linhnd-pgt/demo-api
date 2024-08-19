using LinhND_BaseAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Entities;
using Service.Services.Base;

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
        /// <response code="404">If no books are found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IEnumerable<BookEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _serviceManager.BookService.GetAllBook();
            if (result.Count == 0)
            {
                return NotFound(DevMessageConstants.OBJECT_IS_EMPTY);
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
        /// <response code="404">If no books are found.</response>
        [HttpGet("get-all-paginated")]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <response code="404">If add failed.</response>
        /*[Authorize(Roles = "ROLE_LIBRARIAN")]*/
        [HttpPost("create")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateBook([FromBody] BookRequestDTO newBook)
        {
            var result = await _serviceManager.BookService.AddBook(newBook);
            if (!result)
            {
                return NotFound(DevMessageConstants.ADD_OBJECT_FAILED);
            }
            return Ok(DevMessageConstants.ADD_OBJECT_SUCCESS);


        }

        /// <summary>
        /// Retrieves a message dictates that whether the object is added or not.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a message dictates that whether the object is added or not.
        /// </remarks>
        /// <returns>A message.</returns>
        /// <response code="200">Returns a message.</response>
        /// <response code="404">If add failed.</response>
        /*[Authorize(Roles = "ROLE_LIBRARIAN")]*/
        [HttpPost("update")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBook([FromBody] BookRequestDTO updatedBook)
        {
            var result = await _serviceManager.BookService.UpdateBook(updatedBook);
            if (!result)
            {
                return NotFound(DevMessageConstants.ADD_OBJECT_FAILED);
            }
            return Ok(DevMessageConstants.ADD_OBJECT_SUCCESS);


        }

    }
}
