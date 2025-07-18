namespace TeacherComputerRetrieval.Infrastructure
{
    public interface IRouteService
    {
        int CalculateRouteDistance(string path);
        int CountRoutesWithMaxStops(char start, char end, int maxStops);
        int CountRoutesWithExactStops(char start, char end, int exactStops);
        int FindShortestPath(char start, char end);
        int FindShortestCycle(char node);
        int CountRoutesWithinDistance(char start, char end, int maxDistance);
    }
}
