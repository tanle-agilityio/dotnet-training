using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/book")]
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly BookServices _booksService;

    public BooksController(BookServices bookServices) => _booksService = bookServices;

    [HttpGet]
    public async Task<List<Book>> Get() => await _booksService.GetBooksAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    /// <summary>
    /// Creates a new Book.
    /// </summary>
    /// <param name="newBook"></param>
    /// <returns>A newly created Book</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /book
    ///     {
    ///         "Name": "Design Patterns",
    ///         "Price": 54.93,
    ///         "Category": "Computers",
    ///         "Author": "Ralph Johnson"
    ///     }
    /// </remarks>
    /// <response code="201">Returns the newly created book</response>
    /// <response code="400">If the book is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(Book newBook)
    {
        await _booksService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id =  newBook.Id }, newBook);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _booksService.UpdateAsync(id, book);

        return NoContent();
    }

    /// <summary>
    /// Deletes a specific Book.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /book/655dc4c31c21ec133e5a4c3e
    /// </remarks>
    /// <response code="204">The book is deleted</response>
    /// <response code="404">If the book is not found</response>
    [HttpDelete("{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _booksService.DeleteAsync(id);

        return NoContent();
    }
}
