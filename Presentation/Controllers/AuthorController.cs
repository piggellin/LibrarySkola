using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var query = new GetAllAuthorsQuery();
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error); 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var query = new GetAuthorByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value); 
        }

        return NotFound(result.Error); 
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto authorDto)
    {
        var command = new CreateAuthorCommand(authorDto);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetAuthorById), new { id = result.Value.Id }, result.Value);
        }

        return BadRequest(result.Error); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
    {
        if (id != authorDto.Id)
        {
            return BadRequest("ID mismatch");
        }

        var command = new UpdateAuthorCommand(authorDto);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var command = new DeleteAuthorCommand(id);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return NotFound(result.Error);
    }
}
