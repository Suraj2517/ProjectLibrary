using Moq;

namespace ProjectLM.Tests
{
    [Test]
    public class BooksTests
    {
        [Test]
        public void AddBookDetails_WhenCalled_ShouldInsertBookIntoDatabase()
        {
            // Arrange
            var mockConnection = new Mock<SqlConnection>();
            var mockCommand = new Mock<SqlCommand>();

            mockConnection.Setup(con => con.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Verifiable();

            var title = "Test Title";
            var author = "Test Author";
            var publication = "Test Publication";
            var stock = 10;

            // Act
            Books.AddBookDetails(mockConnection.Object);

            // Assert
            mockConnection.Verify(con => con.Open(), Times.Once);
            mockConnection.Verify(con => con.Close(), Times.Once);
            mockCommand.Verify(cmd => cmd.Parameters.AddWithValue("@Title", title), Times.Once);
            mockCommand.Verify(cmd => cmd.Parameters.AddWithValue("@Author", author), Times.Once);
            mockCommand.Verify(cmd => cmd.Parameters.AddWithValue("@Publication", publication), Times.Once);
            mockCommand.Verify(cmd => cmd.Parameters.AddWithValue("@Stock", stock), Times.Once);
            mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Once);
        }

        // Similar tests can be written for EditBookDetails and DeleteBookDetails methods
        // by mocking the SqlConnection and SqlCommand objects appropriately.
    }
}
