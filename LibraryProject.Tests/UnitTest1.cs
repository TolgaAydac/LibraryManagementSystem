using Xunit;
using Moq;
using LibraryProject.Application;
using LibraryProject.Domain;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryProject.Tests
{
    public class LibraryManagerTests
    {
        // 1. TEST: Üye Listeleme Testi
        [Fact]
        public void GetAllMembers_ShouldReturnEmptyList_WhenNoMembersExist()
        {
            var data = new List<Member>().AsQueryable();
            var mockSet = new Mock<DbSet<Member>>();

            mockSet.As<IQueryable<Member>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Member>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Member>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Member>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ILibraryDbContext>();
            mockContext.Setup(c => c.Members).Returns(mockSet.Object);

            var manager = new LibraryManager(mockContext.Object);
            var result = manager.GetAllMembers();

            Assert.Empty(result);
        }

        // 2. TEST: Kitap Ekleme Testi
        [Fact]
        public void AddBook_ShouldAddBookAndSaveChanges_WhenDataIsValid()
        {

            var mockSet = new Mock<DbSet<Book>>();
            var mockContext = new Mock<ILibraryDbContext>();
            mockContext.Setup(m => m.Books).Returns(mockSet.Object);

            var manager = new LibraryManager(mockContext.Object);
            string testTitle = "Nutuk";
            string testAuthor = "Mustafa Kemal Atatürk";
            int testYear = 1927;


            manager.AddBook(testTitle, testAuthor, testYear);


            mockSet.Verify(m => m.Add(It.Is<Book>(b =>
                b.Title == testTitle &&
                b.Author == testAuthor &&
                b.publishYear == testYear)), Times.Once);


            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}