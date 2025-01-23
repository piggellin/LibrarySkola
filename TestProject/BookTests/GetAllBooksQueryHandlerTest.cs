using Application.Books.Queries;
using Application.Interfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace Test.BookTests
{
    public class GetAllBooksQueryHandlerTests
    {
        private readonly IBookRepository _fakeRepo;
        private readonly ILogger<GetAllBooksQueryHandler> _fakeLogger;
        private readonly GetAllBooksQueryHandler _handler;

        public GetAllBooksQueryHandlerTests()
        {
            _fakeRepo = A.Fake<IBookRepository>();
            _fakeLogger = A.Fake<ILogger<GetAllBooksQueryHandler>>();
            _handler = new GetAllBooksQueryHandler(_fakeRepo, _fakeLogger);
        }

        [Fact]
        public async Task Handle_BooksExist_ReturnsSuccess()
        {
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", AuthorId = 1 },
            new Book { Id = 2, Title = "Book 2", AuthorId = 2 }
        };

            A.CallTo(() => _fakeRepo.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Book>>(books));

            var result = await _handler.Handle(new GetAllBooksQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Books successfully retrieved", result.SuccessMessage);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Book 1", result.Value[0].Title);
            Assert.Equal("Book 2", result.Value[1].Title);
            A.CallTo(() => _fakeRepo.GetAllAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_NoBooksFound_ReturnsFailure()
        {
            A.CallTo(() => _fakeRepo.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Book>>(new List<Book>()));

            var result = await _handler.Handle(new GetAllBooksQuery(), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal("No books found", result.Error);
            A.CallTo(() => _fakeRepo.GetAllAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _fakeRepo.GetAllAsync())
                .Throws(new Exception("Database error"));

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetAllBooksQuery(), CancellationToken.None));
            A.CallTo(() => _fakeRepo.GetAllAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
