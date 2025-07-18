namespace TeacherComputerRetrieval.Tests.Services.RouteServiceTests
{
    // This class inherits the setup logic from our base class.
    public class GetDistanceOfRouteTests : RouteServiceTestBase
    {
        [Theory]
        [InlineData("A-B-C", "9")]
        [InlineData("A-E-B-C-D", "22")]
        [InlineData("A-E-D", "NO SUCH ROUTE")]
        public void GivenVariousRoutes_ReturnsExpectedResult(string route, string expectedResult)
        {
            // Act
            var actualResult = Service.GetDistanceOfRoute(route);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}