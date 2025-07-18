using TeacherComputerRetrieval.Infrastructure.Repositories;
using TeacherComputerRetrieval.Services;

namespace TeacherComputerRetrieval.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Teacher Computer Retrieval System ===");
            Console.WriteLine("Please enter the route data (e.g., AB5, BC4, CD8, ...):");

            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("No input provided. Exiting.");
                return;
            }

            try
            {
                // Initialize services with user input
                var repository = new RouteRepository(userInput);
                var routeService = new RouteService(repository.GetGraph());

                Console.WriteLine("\n--- Calculating Results ---");

                // Execute and print answers to all 8 questions
                Console.WriteLine($"1. Output #1: {routeService.GetDistanceOfRoute("A-B-C")}");
                Console.WriteLine($"2. Output #2: {routeService.GetDistanceOfRoute("A-E-B-C-D")}");
                Console.WriteLine($"3. Output #3: {routeService.GetDistanceOfRoute("A-E-D")}");
                Console.WriteLine($"4. Output #4: {routeService.CountTripsWithMaxStops('C', 'C', 3)}");
                Console.WriteLine($"5. Output #5: {routeService.CountTripsWithExactStops('A', 'C', 4)}");
                Console.WriteLine($"6. Output #6: {routeService.FindShortestRoute('A', 'C')}");
                Console.WriteLine($"7. Output #7: {routeService.FindShortestRoute('B', 'B')}");
                Console.WriteLine($"8. Output #8: {routeService.CountRoutesWithMaxDistance('C', 'C', 30)}");

            }
            catch (Exception ex)
            {
                // Catch any unexpected errors during processing
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nProcessing complete. Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}