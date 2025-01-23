using Application.Books.Commands.DeleteBook;
using Application.Interfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace Test.BookTests
{
    public class DeleteBookCommandHandlerTests
    {
        private readonly IBookRepository _fakeRepo;
        private readonly ILogger<DeleteBookCommandHandler> _fakeLogger;
        private readonly DeleteBookCommandHandler _handler;

        public DeleteBookCommandHandlerTests()
        {
            _fakeRepo = A.Fake<IBookRepository>();
            _fakeLogger = A.Fake<ILogger<DeleteBookCommandHandler>>();
            _handler = new DeleteBookCommandHandler(_fakeRepo, _fakeLogger);
        }

        [Fact]
        public async Task Handle_BookExists_DeletesBookAndReturnsSuccess()
        {
            var BookId = 1;
            var command = new DeleteBookCommand(BookId);
            var book = new Book { Id = BookId };

            A.CallTo(() => _fakeRepo.GetByIdAsync(BookId))
                .Returns(Task.FromResult(book));

            A.CallTo(() => _fakeRepo.DeleteAsync(BookId))
                .Returns(Task.FromResult(true));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Book successfully deleted", result.SuccessMessage);
            A.CallTo(() => _fakeRepo.GetByIdAsync(BookId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeRepo.DeleteAsync(BookId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_BookDoesNotExist_ReturnsFailure()
        {
            var BookId = 1;
            var command = new DeleteBookCommand(BookId);

            A.CallTo(() => _fakeRepo.GetByIdAsync(BookId))
                .Returns(Task.FromResult<Book>(null));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal("Book not found", result.Error);
            A.CallTo(() => _fakeRepo.GetByIdAsync(BookId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeRepo.DeleteAsync(BookId)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ThrowsException()
        {
            var BookId = 1;
            var command = new DeleteBookCommand(BookId);

            A.CallTo(() => _fakeRepo.GetByIdAsync(BookId))
                .Throws(new Exception("Database error"));

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            A.CallTo(() => _fakeRepo.GetByIdAsync(BookId)).MustHaveHappenedOnceExactly();
        }
    }
}
