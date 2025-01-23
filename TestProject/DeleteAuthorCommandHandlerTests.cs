using Application.Authors.Commands.DeleteAuthor;
using Application.Interfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace Test
{
    public class DeleteAuthorCommandHandlerTests
    {
        private readonly IAuthorRepository _fakeRepo;
        private readonly ILogger<DeleteAuthorCommandHandler> _fakeLogger;
        private readonly DeleteAuthorCommandHandler _handler;

        public DeleteAuthorCommandHandlerTests()
        {
            _fakeRepo = A.Fake<IAuthorRepository>();
            _fakeLogger = A.Fake<ILogger<DeleteAuthorCommandHandler>>();
            _handler = new DeleteAuthorCommandHandler(_fakeRepo, _fakeLogger);
        }

        [Fact]
        public async Task Handle_AuthorExists_DeletesAuthorAndReturnsSuccess()
        {
            var authorId = 1;
            var command = new DeleteAuthorCommand(authorId);
            var author = new Author { Id = authorId };

            A.CallTo(() => _fakeRepo.GetByIdAsync(authorId))
                .Returns(Task.FromResult(author)); 

            A.CallTo(() => _fakeRepo.DeleteAsync(authorId))
                .Returns(Task.FromResult(true)); 

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Author successfully deleted", result.SuccessMessage);
            A.CallTo(() => _fakeRepo.GetByIdAsync(authorId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeRepo.DeleteAsync(authorId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_AuthorDoesNotExist_ReturnsFailure()
        {
            var authorId = 1;
            var command = new DeleteAuthorCommand(authorId);

            A.CallTo(() => _fakeRepo.GetByIdAsync(authorId))
                .Returns(Task.FromResult<Author>(null)); 

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal("Author not found", result.Error);
            A.CallTo(() => _fakeRepo.GetByIdAsync(authorId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeRepo.DeleteAsync(authorId)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ThrowsException()
        {
            var authorId = 1;
            var command = new DeleteAuthorCommand(authorId);

            A.CallTo(() => _fakeRepo.GetByIdAsync(authorId))
                .Throws(new Exception("Database error")); 

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            A.CallTo(() => _fakeRepo.GetByIdAsync(authorId)).MustHaveHappenedOnceExactly();
        }
    }
}
