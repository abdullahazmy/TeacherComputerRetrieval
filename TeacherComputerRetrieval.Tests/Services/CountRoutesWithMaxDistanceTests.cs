namespace TeacherComputerRetrieval.Tests.Services.RouteServiceTests
{
    public class CountRoutesWithMaxDistanceTests : RouteServiceTestBase
    {
        [Fact]
        public void GivenProblemCase_ReturnsCorrectCount()
        {
            // Act
            var result = Service.CountRoutesWithMaxDistance('C', 'C', 30);

            // Assert
            Assert.Equal(7, result);
        }
    }
}