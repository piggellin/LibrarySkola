using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Queries;
using Infrastructure;

namespace Tests
{
    [TestFixture]
    public class BookHandlerTests
    {
        private FakeDatabase _db;

        [SetUp]
        public void Setup()
        {
            _db = new FakeDatabase();
        }

        [Test]
        public async Task CanGetBookById()
        {
            var handler = new GetBookByIdQueryHandler(_db);
            var query = new GetBookByIdQuery { Id = 1 };

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Sample Book", result.Title);
        }

        [Test]
        public async Task CanGetAllBooks()
        {
            var handler = new GetAllBooksQueryHandler(_db);
            var query = new GetAllBooksQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Sample Book", result[0].Title);
        }

        [Test]
        public async Task CanCreateBook()
        {
            var handler = new CreateBookCommandHandler(_db);
            var command = new CreateBookCommand { Title = "New Book", AuthorId = 1 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, _db.Books.Count);
            Assert.AreEqual("New Book", result.Title);
        }

        [Test]
        public async Task CanDeleteBook()
        {
            var handler = new DeleteBookCommandHandler(_db);
            var command = new DeleteBookCommand { Id = 1 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual(0, _db.Books.Count);
        }

        [Test]
        public async Task DeletingNonexistentBookReturnsFalse()
        {
            var handler = new DeleteBookCommandHandler(_db);
            var command = new DeleteBookCommand { Id = 999 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result);
            Assert.AreEqual(1, _db.Books.Count); 
        }
    }
}
