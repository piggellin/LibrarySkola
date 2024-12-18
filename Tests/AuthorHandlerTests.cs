using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries;
using Application.Interfaces;
using Domain.Models;
using FakeItEasy;

namespace Tests
{
    [TestFixture]
    public class AuthorHandlerTests
    {
        private IAuthorRepository _authorRepo;

        [SetUp]
        public void Setup()
        {
            _authorRepo = A.Fake<IAuthorRepository>();
        }

        [Test]
        public async Task GetAuthorById_ReturnsSuccessResult_WhenAuthorExists()
        {
            // Arrange
            var existingAuthor = new Author { Id = 1, Name = "Mocked Author" };
            A.CallTo(() => _authorRepo.GetByIdAsync(1)).Returns(existingAuthor);

            var handler = new GetAuthorByIdQueryHandler(_authorRepo);
            var query = new GetAuthorByIdQuery { Id = 1 };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Mocked Author", result.Value.Name);
        }

        [Test]
        public async Task GetAuthorById_ReturnsFailureResult_WhenAuthorNotFound()
        {
            // Arrange
            A.CallTo(() => _authorRepo.GetByIdAsync(999)).Returns<Author>(null);

            var handler = new GetAuthorByIdQueryHandler(_authorRepo);
            var query = new GetAuthorByIdQuery { Id = 999 };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Value);
            Assert.AreEqual("Author with ID 999 not found.", result.Error);
        }

        [Test]
        public async Task CreateAuthor_ReturnsSuccessResult_WhenAuthorIsCreated()
        {
            // Arrange
            var newAuthor = new Author { Id = 2, Name = "New Author" };
            A.CallTo(() => _authorRepo.AddAsync(A<Author>.Ignored)).Returns(newAuthor);

            var handler = new CreateAuthorCommandHandler(_authorRepo);
            var command = new CreateAuthorCommand { Name = "New Author" };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("New Author", result.Value.Name);
        }

        [Test]
        public async Task UpdateAuthor_ReturnsSuccessResult_WhenAuthorIsUpdated()
        {
            // Arrange
            var existingAuthor = new Author { Id = 1, Name = "Old Name" };
            A.CallTo(() => _authorRepo.GetByIdAsync(1)).Returns(existingAuthor);

            A.CallTo(() => _authorRepo.UpdateAsync(A<Author>.Ignored))
             .ReturnsLazily((Author a) => a);

            var handler = new UpdateAuthorCommandHandler(_authorRepo);
            var command = new UpdateAuthorCommand { Id = 1, Name = "Updated Name" };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Updated Name", result.Value.Name);
        }

        [Test]
        public async Task UpdateAuthor_ReturnsFailureResult_WhenAuthorNotFound()
        {
            // Arrange
            A.CallTo(() => _authorRepo.GetByIdAsync(999)).Returns<Author>(null);

            var handler = new UpdateAuthorCommandHandler(_authorRepo);
            var command = new UpdateAuthorCommand { Id = 999, Name = "Doesn't matter" };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Author with ID 999 not found.", result.Error);
        }

        [Test]
        public async Task DeleteAuthor_ReturnsSuccessResult_WhenAuthorDeleted()
        {
            // Arrange
            A.CallTo(() => _authorRepo.DeleteAsync(1)).Returns(true);

            var handler = new DeleteAuthorCommandHandler(_authorRepo);
            var command = new DeleteAuthorCommand { Id = 1 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(true, result.Value);
        }
        [Test]
        public async Task DeleteAuthor_ReturnsFailureResult_WhenAuthorNotFound()
        {
            // Arrange
            A.CallTo(() => _authorRepo.DeleteAsync(999)).Returns(false);

            var handler = new DeleteAuthorCommandHandler(_authorRepo);
            var command = new DeleteAuthorCommand { Id = 999 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Author with ID 999 not found.", result.Error);
        }
    }
}
