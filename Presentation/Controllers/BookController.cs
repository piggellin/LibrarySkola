using Application.Commands;
using Application.Handlers;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookHandler _handler;

        public BookController(BookHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _handler.Handle(new GetBookByIdQuery { Id = id });
            return book != null ? Ok(book) : NotFound();
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] CreateBookCommand command)
        {
            _handler.Handle(command);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook([FromBody] UpdateBookCommand command)
        {
            _handler.Handle(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            _handler.Handle(new DeleteBookCommand { Id = id });
            return Ok();
        }
    }

}
