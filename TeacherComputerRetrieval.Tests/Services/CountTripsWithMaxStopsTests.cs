namespace TeacherComputerRetrieval.Tests.Services.RouteServiceTests
{
    public class CountTripsWithMaxStopsTests : RouteServiceTestBase
    {
        [Fact]
        public void GivenProblemCase_ReturnsCorrectCount()
        {
            // Act
            var result = Service.CountTripsWithMaxStops('C', 'C', 3);

            // Assert
            Assert.Equal(2, result);
        }
    }
}