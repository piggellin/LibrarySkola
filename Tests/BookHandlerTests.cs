using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries;
using Application.Interfaces;
using Domain.Models;
using FakeItEasy;

namespace Tests
{
    [TestFixture]
    public class BookHandlerTests
    {
        private IBookRepository _bookRepo;

        [SetUp]
        public void Setup()
        {
            _bookRepo = A.Fake<IBookRepository>();
        }

        [Test]
        public async Task GetBookById_ReturnsSuccess_WhenBookExists()
        {
            var existingBook = new Book { Id = 1, Title = "Mocked Book", AuthorId = 1 };
            A.CallTo(() => _bookRepo.GetByIdAsync(1)).Returns(existingBook);

            var handler = new GetBookByIdQueryHandler(_bookRepo);
            var query = new GetBookByIdQuery { Id = 1 };

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Mocked Book", result.Value.Title);
        }

        [Test]
        public async Task GetBookById_ReturnsFailure_WhenBookNotFound()
        {
            A.CallTo(() => _bookRepo.GetByIdAsync(999)).Returns<Book>(null);

            var handler = new GetBookByIdQueryHandler(_bookRepo);
            var query = new GetBookByIdQuery { Id = 999 };

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Book with ID 999 not found.", result.Error);
        }

        [Test]
        public async Task CreateBook_ReturnsSuccess_WhenBookCreated()
        {
            var newBook = new Book { Id = 2, Title = "New Book", AuthorId = 1 };
            A.CallTo(() => _bookRepo.AddAsync(A<Book>.Ignored)).Returns(newBook);

            var handler = new CreateBookCommandHandler(_bookRepo);
            var command = new CreateBookCommand { Title = "New Book", AuthorId = 1 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("New Book", result.Value.Title);
        }

        [Test]
        public async Task UpdateBook_ReturnsSuccess_WhenBookExists()
        {
            var existingBook = new Book { Id = 1, Title = "Old Title", AuthorId = 1 };
            A.CallTo(() => _bookRepo.GetByIdAsync(1)).Returns(existingBook);

            A.CallTo(() => _bookRepo.UpdateAsync(A<Book>.Ignored))
             .ReturnsLazily((Book b) => b);

            var handler = new UpdateBookCommandHandler(_bookRepo);
            var command = new UpdateBookCommand { Id = 1, Title = "Updated Title", AuthorId = 1 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Updated Title", result.Value.Title);
        }

        [Test]
        public async Task UpdateBook_ReturnsFailure_WhenBookNotFound()
        {
            A.CallTo(() => _bookRepo.GetByIdAsync(999)).Returns<Book>(null);

            var handler = new UpdateBookCommandHandler(_bookRepo);
            var command = new UpdateBookCommand { Id = 999, Title = "No Title", AuthorId = 1 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Book with ID 999 not found.", result.Error);
        }
        [Test]
        public async Task DeleteBook_ReturnsSuccess_WhenBookDeleted()
        {
            A.CallTo(() => _bookRepo.DeleteAsync(1)).Returns(true);

            var handler = new DeleteBookCommandHandler(_bookRepo);
            var command = new DeleteBookCommand { Id = 1 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public async Task DeleteBook_ReturnsFailure_WhenBookNotFound()
        {
            A.CallTo(() => _bookRepo.DeleteAsync(999)).Returns(false);

            var handler = new DeleteBookCommandHandler(_bookRepo);
            var command = new DeleteBookCommand { Id = 999 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Book with ID 999 not found.", result.Error);
        }
    }
}
