namespace TeacherComputerRetrieval.Tests.Services.RouteServiceTests
{
    public class FindShortestRouteTests : RouteServiceTestBase
    {
        [Theory]
        [InlineData('A', 'C', "9")]
        [InlineData('B', 'B', "9")]
        [InlineData('A', 'F', "NO SUCH ROUTE")]
        public void GivenVariousCases_ReturnsExpectedDistance(char start, char end, string expectedResult)
        {
            // Act
            var actualResult = Service.FindShortestRoute(start, end);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}