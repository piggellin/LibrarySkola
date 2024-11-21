using Application.Commands;
using Application.Handlers;
using Application.Queries;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class BookHandlerTests
    {
        private FakeDatabase _db;
        private BookHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _db = new FakeDatabase();
            _handler = new BookHandler(_db);
        }

        [TestMethod]
        public void CanCreateBook()
        {
            var command = new CreateBookCommand { Title = "New Book", AuthorId = 1 };
            _handler.Handle(command);
            Assert.AreEqual(2, _db.Books.Count);
        }

        [TestMethod]
        public void CanGetBookById()
        {
            var query = new GetBookByIdQuery { Id = 1 };
            var book = _handler.Handle(query);
            Assert.IsNotNull(book);
            Assert.AreEqual("Book", book.Title);
        }
    }
}
