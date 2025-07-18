using TeacherComputerRetrieval.Core.Models;
using TeacherComputerRetrieval.Services;

namespace TeacherComputerRetrieval.Tests.Services
{
    /// <summary>
    /// A base class for all RouteService tests. It handles the common setup
    /// of creating the graph and the service instance.
    /// </summary>
    public abstract class RouteServiceTestBase
    {
        protected readonly RouteService Service;

        protected RouteServiceTestBase()
        {
            // This setup is now shared across all test files that inherit from this class.
            const string graphInput = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            var graph = new Graph(graphInput);
            Service = new RouteService(graph);
        }
    }
}