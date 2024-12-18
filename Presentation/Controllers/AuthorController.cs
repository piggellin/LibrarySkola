using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var query = new GetAuthorByIdQuery { Id = id };
        var author = await _mediator.Send(query);

        return author != null ? Ok(author) : NotFound($"Author with ID {id} not found.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var query = new GetAllAuthorsQuery();
        var authors = await _mediator.Send(query);

        return Ok(authors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetAuthorById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in the URL does not match ID in the body.");
        }

        var updatedAuthor = await _mediator.Send(command);
        return Ok(updatedAuthor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var command = new DeleteAuthorCommand { Id = id };
        var result = await _mediator.Send(command);

        return result.IsSuccess
            ? NoContent()
            : NotFound(result.Error ?? $"Author with ID {id} not found.");
    }
}
