using TeacherComputerRetrieval.Core.Models;

namespace TeacherComputerRetrieval.Infrastructure.Repositories
{
    public class RouteRepository
    {
        private readonly Graph _graph;

        public RouteRepository(string routeData)
        {
            _graph = new Graph(routeData);
        }

        public Graph GetGraph()
        {
            return _graph;
        }
    }
}