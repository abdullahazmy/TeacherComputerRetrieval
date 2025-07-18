namespace TeacherComputerRetrieval.Core.Interfaces
{
    public interface IRouteService
    {
        string GetDistanceOfRoute(string route);
        int CountTripsWithMaxStops(char start, char end, int maxStops);
        int CountTripsWithExactStops(char start, char end, int exactStops);
        string FindShortestRoute(char start, char end);
        int CountRoutesWithMaxDistance(char start, char end, int maxDistance);
    }
}