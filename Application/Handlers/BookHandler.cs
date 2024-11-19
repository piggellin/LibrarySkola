using Application.Commands;
using Application.Queries;
using Domain.Models;
using Infrastructure;

namespace Application.Handlers
{
    public class BookHandler
    {
        private readonly FakeDatabase _db;

        public BookHandler(FakeDatabase db)
        {
            _db = db;
        }
        public Book Handle(GetBookByIdQuery query)
        {
            return _db.Books.FirstOrDefault(book => book.Id == query.Id);
        }
        public List<Book> Handle(GetAllBooksQuery query)
        {
            return _db.Books;
        }
        public void Handle(CreateBookCommand command)
        {
            var newBook = new Book { Id = _db.Books.Count + 1, Title = command.Title, AuthorId = command.AuthorId };
            _db.Books.Add(newBook);
        }
        public void Handle(UpdateBookCommand command)
        {
            var book = _db.Books.FirstOrDefault(book => book.Id == command.Id);
            if (book != null)
            {
                book.Title = command.Title;
                book.AuthorId = command.AuthorId;
            }
        }
        public void Handle(DeleteBookCommand command)
        {
            var book = _db.Books.FirstOrDefault(book => book.Id == command.Id);
            if (book != null)
                _db.Books.Remove(book);
        }
    }
}
