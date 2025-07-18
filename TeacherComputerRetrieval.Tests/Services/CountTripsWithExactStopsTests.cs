namespace TeacherComputerRetrieval.Tests.Services.RouteServiceTests
{
    public class CountTripsWithExactStopsTests : RouteServiceTestBase
    {
        [Fact]
        public void GivenProblemCase_ReturnsCorrectCount()
        {
            // Act
            var result = Service.CountTripsWithExactStops('A', 'C', 4);

            // Assert
            Assert.Equal(3, result);
        }
    }
}