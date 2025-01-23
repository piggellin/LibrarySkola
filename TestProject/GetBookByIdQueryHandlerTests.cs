using Application.Books.Queries;
using Application.Interfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace Test
{
    public class GetBookByIdQueryHandlerTests
    {
        private readonly IBookRepository _fakeRepo;
        private readonly ILogger<GetBookByIdQueryHandler> _fakeLogger;
        private readonly GetBookByIdQueryHandler _handler;

        public GetBookByIdQueryHandlerTests()
        {
            _fakeRepo = A.Fake<IBookRepository>();
            _fakeLogger = A.Fake<ILogger<GetBookByIdQueryHandler>>();
            _handler = new GetBookByIdQueryHandler(_fakeRepo, _fakeLogger);
        }

        [Fact]
        public async Task Handle_BookExists_ReturnsSuccess()
        {
            var bookId = 1;
            var command = new GetBookByIdQuery(bookId); 
            var book = new Book { Id = bookId, Title = "Test Book", AuthorId = 1 };

            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId))
                .Returns(Task.FromResult(book)); 

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Book successfully retrieved", result.SuccessMessage);
            Assert.Equal(bookId, result.Value.Id);
            Assert.Equal("Test Book", result.Value.Title);
            Assert.Equal(1, result.Value.AuthorId);
            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_BookNotFound_ReturnsFailure()
        {
            var bookId = 1;
            var command = new GetBookByIdQuery(bookId);

            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId))
                .Returns(Task.FromResult<Book>(null)); 

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal("Book not found", result.Error);
            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ThrowsException()
        {
            var bookId = 1;
            var command = new GetBookByIdQuery(bookId); 

            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId))
                .Throws(new Exception("Database error")); 

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            A.CallTo(() => _fakeRepo.GetByIdAsync(bookId)).MustHaveHappenedOnceExactly();
        }
    }
}
