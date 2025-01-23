using Application.Books.Commands.CreateBook;
using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace Test.BookTests
{
    public class CreateBookCommandHandlerTests
    {
        private readonly IBookRepository _fakeRepo;
        private readonly ILogger<CreateBookCommandHandler> _fakeLogger;
        private readonly CreateBookCommandHandler _handler;

        public CreateBookCommandHandlerTests()
        {
            _fakeRepo = A.Fake<IBookRepository>();
            _fakeLogger = A.Fake<ILogger<CreateBookCommandHandler>>();

            _handler = new CreateBookCommandHandler(_fakeRepo, _fakeLogger);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenBookIsCreated()
        {
            var bookDto = new BookDto
            {
                Title = "New Book"
            };

            var command = new CreateBookCommand(bookDto);

            A.CallTo(() => _fakeRepo.AddAsync(A<Book>.Ignored)).Returns(Task.FromResult(new Book { Id = 1, Title = "New Book" }));

            var result = await _handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.Equal("New Book", result.Value.Title);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenBookAlreadyExists()
        {
            var bookDto = new BookDto
            {
                Title = "Existing Book"
            };

            var command = new CreateBookCommand(bookDto);

            A.CallTo(() => _fakeRepo.BookTitleExists(A<string>.Ignored)).Returns(Task.FromResult(true));

            var result = await _handler.Handle(command, default);

            Assert.False(result.IsSuccess);
            Assert.Equal("Book already exists", result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionOccurs()
        {
            var bookDto = new BookDto
            {
                Title = "Book With Exception"
            };

            var command = new CreateBookCommand(bookDto);

            A.CallTo(() => _fakeRepo.AddAsync(A<Book>.Ignored)).Throws<Exception>();

            var result = await _handler.Handle(command, default);

            Assert.False(result.IsSuccess);
            Assert.Equal("Unexpected error", result.Error);
        }
    }
}
