using Application.Authors.Commands.CreateAuthor;
using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace Test
{
    public class CreateAuthorCommandHandlerTests
    {
        private readonly IAuthorRepository _fakeRepo;
        private readonly ILogger<CreateAuthorCommandHandler> _fakeLogger;
        private readonly CreateAuthorCommandHandler _handler;

        public CreateAuthorCommandHandlerTests()
        {
            _fakeRepo = A.Fake<IAuthorRepository>();
            _fakeLogger = A.Fake<ILogger<CreateAuthorCommandHandler>>();

            _handler = new CreateAuthorCommandHandler(_fakeRepo, _fakeLogger);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenAuthorIsCreated()
        {
            var authorDto = new AuthorDto
            {
                Name = "New Author"
            };

            var command = new CreateAuthorCommand(authorDto);

            A.CallTo(() => _fakeRepo.AddAsync(A<Author>.Ignored)).Returns(Task.FromResult(new Author { Id = 1, Name = "New Author" }));

            var result = await _handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.Equal("New Author", result.Value.Name);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenAuthorAlreadyExists()
        {
            var authorDto = new AuthorDto
            {
                Name = "Existing Author"
            };

            var command = new CreateAuthorCommand(authorDto);

            A.CallTo(() => _fakeRepo.AuthorExists(A<string>.Ignored)).Returns(Task.FromResult(true));

            var result = await _handler.Handle(command, default);

            Assert.False(result.IsSuccess);
            Assert.Equal("Author already exists", result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionOccurs()
        {
            var authorDto = new AuthorDto
            {
                Name = "Author With Exception"
            };

            var command = new CreateAuthorCommand(authorDto);

            A.CallTo(() => _fakeRepo.AddAsync(A<Author>.Ignored)).Throws<Exception>();

            var result = await _handler.Handle(command, default);

            Assert.False(result.IsSuccess); 
            Assert.Equal("Unexpected error", result.Error); 
        }
    }
}
