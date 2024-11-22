﻿using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Queries;
using Domain.Models;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class AuthorHandlerTests
    {
        private FakeDatabase _db;

        [TestInitialize]
        public void Setup()
        {
            // Initialiserar fake-databasen med testdata
            _db = new FakeDatabase();
        }

        [TestMethod]
        public async Task CanGetAuthorById()
        {
            var handler = new GetAuthorByIdQueryHandler(_db);
            var query = new GetAuthorByIdQuery { Id = 1 };

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Sample Author", result.Name);
        }

        [TestMethod]
        public async Task CanGetAllAuthors()
        {
            var handler = new GetAllAuthorsQueryHandler(_db);
            var query = new GetAllAuthorsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Sample Author", result[0].Name);
        }

        [TestMethod]
        public async Task CanCreateAuthor()
        {
            var handler = new CreateAuthorCommandHandler(_db);
            var command = new CreateAuthorCommand { Name = "New Author" };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, _db.Authors.Count);
            Assert.AreEqual("New Author", result.Name);
        }

        [TestMethod]
        public async Task CanDeleteAuthor()
        {
            var handler = new DeleteAuthorCommandHandler(_db);
            var command = new DeleteAuthorCommand { Id = 1 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual(0, _db.Authors.Count);
        }

        [TestMethod]
        public async Task DeletingNonexistentAuthorReturnsFalse()
        {
            var handler = new DeleteAuthorCommandHandler(_db);
            var command = new DeleteAuthorCommand { Id = 999 }; // Ogiltigt ID

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result);
            Assert.AreEqual(1, _db.Authors.Count); // Ingen författare ska påverkas
        }

        [TestMethod]
        public async Task DeletingAuthorAlsoDeletesAssociatedBooks()
        {
            // Lägg till en bok kopplad till författaren
            _db.Books.Add(new Book { Id = 2, Title = "Book by Author", AuthorId = 1 });

            var handler = new DeleteAuthorCommandHandler(_db);
            var command = new DeleteAuthorCommand { Id = 1 };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual(0, _db.Authors.Count);
            Assert.AreEqual(0, _db.Books.Count); // Boken ska också raderas
        }
    }
}
