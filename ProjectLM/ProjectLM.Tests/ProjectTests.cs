using Moq;
using FluentAssertions;
using ProjectLM;

namespace ProjectLM.Tests
{
    public class UnitTest1
    {
        [Test]
        public void AddBookDetails_WhenCalled_ReturnsValue()
        {
            var _booksmock = new Mock<IBooks>();
            _booksmock.Setup(b => b.AddBookDetails()).Returns(1);
            var result = _booksmock.Object.AddBookDetails();
            result.Should().Be(1);
        }


        [Test]
        public void DeleteBookDetails_WhenCalled_ReturnsValue()
        {
            var _deletebookmock = new Mock<IBooks>();
            _deletebookmock.Setup(b => b.DeleteBookDetails()).Returns(1);
            var result = _deletebookmock.Object.DeleteBookDetails();
            result.Should().Be(1);
        }


        [Test]
        public void EditBookDetails_WhenCalled_ReturnsValue()
        {
            var _updatebookmock = new Mock<IBooks>();
            _updatebookmock.Setup(b => b.EditBookDetails()).Returns(1);
            var result = _updatebookmock.Object.EditBookDetails();
            result.Should().Be(1);
        }
        [Test]
        public void AddStudentDetails_WhenCalled_ReturnsValue()
        {
            var _studentmock = new Mock<IStudents>();
            _studentmock.Setup(s => s.AddStudentDetails()).Returns(1);
            var res = _studentmock.Object.AddStudentDetails();
            res.Should().Be(1);
        }


        [Test]
        public void DeleteStudentDetails_WhenCalled_ReturnsValue()
        {
            var _deletestudentmock = new Mock<IStudents>();
            _deletestudentmock.Setup(s => s.DeleteStudentDetails()).Returns(1);
            var res = _deletestudentmock.Object.DeleteStudentDetails();
            res.Should().Be(1);
        }

        [Test]
        public void EditStudentDetails_WhenCalled_RetursValue()
        {
            var _updatestudentmock = new Mock<IStudents>();
            _updatestudentmock.Setup(s => s.EditStudentDetails()).Returns(1);
            var res = _updatestudentmock.Object.EditStudentDetails();
            res.Should().Be(1);
        }
    }
}