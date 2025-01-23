using Application.Books.Commands.UpdateBook;
using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace Test.BookTests
{
    public class UpdateBookCommandHandlerTests
    {
        private readonly IBookRepository _fakeRepo;
        private readonly ILogger<UpdateBookCommandHandler> _fakeLogger;
        private readonly UpdateBookCommandHandler _handler;

        public UpdateBookCommandHandlerTests()
        {
            _fakeRepo = A.Fake<IBookRepository>();
            _fakeLogger = A.Fake<ILogger<UpdateBookCommandHandler>>();
            _handler = new UpdateBookCommandHandler(_fakeRepo, _fakeLogger);
        }

        [Fact]
        public async Task Handle_BookExists_UpdatesBookAndReturnsSuccess()
        {
            var bookId = 1;
            var command = new UpdateBookCommand(new BookDto { Id = bookId, Title = "Updated Title", AuthorId = 2 });
            var existingBook = new Book { Id = bookId, Title = "Old Title", AuthorId = 1 };

            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId))
                .Returns(Task.FromResult(existingBook));
            A.CallTo(() => _fakeRepo.UpdateAsync(A<Book>.Ignored))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Book successfully updated", result.SuccessMessage);
            Assert.Equal("Updated Title", result.Value.Title);
            Assert.Equal(2, result.Value.AuthorId);
            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeRepo.UpdateAsync(A<Book>.That.Matches(b => b.Id == bookId && b.Title == "Updated Title" && b.AuthorId == 2)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_BookNotFound_ReturnsFailure()
        {
            var bookId = 1;
            var command = new UpdateBookCommand(new BookDto { Id = bookId, Title = "Updated Title", AuthorId = 2 });

            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId))
                .Returns(Task.FromResult<Book>(null));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal("Book not found", result.Error);
            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeRepo.UpdateAsync(A<Book>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ThrowsException()
        {
            var bookId = 1;
            var command = new UpdateBookCommand(new BookDto { Id = bookId, Title = "Updated Title", AuthorId = 2 });

            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId))
                .Throws(new Exception("Database error"));

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId)).MustHaveHappenedOnceExactly();
        }
    }
}
