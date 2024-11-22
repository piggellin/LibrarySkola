using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var query = new GetBookByIdQuery { Id = id };
        var book = await _mediator.Send(query);

        return book != null ? Ok(book) : NotFound($"Book with ID {id} not found.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var query = new GetAllBooksQuery();
        var books = await _mediator.Send(query);

        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        var book = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in the URL does not match ID in the body.");
        }

        var updatedBook = await _mediator.Send(command);
        return Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var command = new DeleteBookCommand { Id = id };
        var result = await _mediator.Send(command);

        return result ? NoContent() : NotFound($"Book with ID {id} not found.");
    }
}
