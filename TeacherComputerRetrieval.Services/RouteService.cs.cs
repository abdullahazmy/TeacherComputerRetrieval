using TeacherComputerRetrieval.Core.Interfaces;
using TeacherComputerRetrieval.Core.Models;

namespace TeacherComputerRetrieval.Services
{
    public class RouteService : IRouteService
    {
        private readonly Graph _graph;

        public RouteService(Graph graph)
        {
            _graph = graph;
        }

        public string GetDistanceOfRoute(string route)
        {
            var nodes = route.Split('-');
            int totalDistance = 0;
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                if (!char.TryParse(nodes[i], out var startNode) ||
                    !char.TryParse(nodes[i + 1], out var endNode))
                {
                    return "NO SUCH ROUTE";
                }

                if (_graph.AdjacencyList.TryGetValue(startNode, out var edges) && edges.TryGetValue(endNode, out var distance))
                {
                    totalDistance += distance;
                }
                else
                {
                    return "NO SUCH ROUTE";
                }
            }
            return totalDistance.ToString();
        }

        public int CountTripsWithMaxStops(char start, char end, int maxStops)
        {
            return MaxStopsRecursive(start, end, 0, maxStops);
        }

        private int MaxStopsRecursive(char current, char end, int stops, int maxStops)
        {
            if (stops > maxStops) return 0;

            int count = 0;
            if (current == end && stops > 0)
            {
                count++;
            }

            if (_graph.AdjacencyList.TryGetValue(current, out var neighbors))
            {
                foreach (var neighbor in neighbors.Keys)
                {
                    count += MaxStopsRecursive(neighbor, end, stops + 1, maxStops);
                }
            }
            return count;
        }

        public int CountTripsWithExactStops(char start, char end, int exactStops)
        {
            return ExactStopsRecursive(start, end, 0, exactStops);
        }

        private int ExactStopsRecursive(char current, char end, int stops, int exactStops)
        {
            if (stops > exactStops) return 0;

            if (stops == exactStops)
            {
                return current == end ? 1 : 0;
            }

            int count = 0;
            if (_graph.AdjacencyList.TryGetValue(current, out var neighbors))
            {
                foreach (var neighbor in neighbors.Keys)
                {
                    count += ExactStopsRecursive(neighbor, end, stops + 1, exactStops);
                }
            }
            return count;
        }

        public int CountRoutesWithMaxDistance(char start, char end, int maxDistance)
        {
            return MaxDistanceRecursive(start, end, 0, maxDistance);
        }

        private int MaxDistanceRecursive(char current, char end, int currentDistance, int maxDistance)
        {
            int count = 0;
            if (_graph.AdjacencyList.TryGetValue(current, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                {
                    int newDistance = currentDistance + neighbor.Value;
                    if (newDistance < maxDistance)
                    {
                        if (neighbor.Key == end)
                        {
                            count++;
                        }
                        count += MaxDistanceRecursive(neighbor.Key, end, newDistance, maxDistance);
                    }
                }
            }
            return count;
        }

        public string FindShortestRoute(char start, char end)
        {
            if (start != end)
            {
                int distance = FindShortestPath(start, end);
                return distance == int.MaxValue ? "NO SUCH ROUTE" : distance.ToString();
            }

            // Logic for shortest cycle (start == end)
            int shortestCycle = int.MaxValue;
            if (!_graph.AdjacencyList.TryGetValue(start, out var neighbors))
            {
                return "NO SUCH ROUTE"; // Cannot leave, so cannot cycle
            }

            // For each neighbor, find the shortest path from it back to the start
            foreach (var neighbor in neighbors)
            {
                int outboundEdgeWeight = neighbor.Value;
                // Find shortest path from the neighbor back to the original start node
                int returnPathDistance = FindShortestPath(neighbor.Key, start);

                if (returnPathDistance != int.MaxValue)
                {
                    shortestCycle = Math.Min(shortestCycle, outboundEdgeWeight + returnPathDistance);
                }
            }

            return shortestCycle == int.MaxValue ? "NO SUCH ROUTE" : shortestCycle.ToString();
        }

        // Private helper to run a standard Dijkstra's algorithm for A->B paths.
        private int FindShortestPath(char start, char end)
        {
            var priorityQueue = new SortedSet<(int distance, char vertex)>();
            var distances = new Dictionary<char, int>();
            foreach (var node in _graph.Nodes)
            {
                distances[node] = int.MaxValue;
            }

            if (!_graph.Nodes.Contains(start)) return int.MaxValue;

            distances[start] = 0;
            priorityQueue.Add((0, start));

            while (priorityQueue.Count > 0)
            {
                var (_, currentNode) = priorityQueue.First();
                priorityQueue.Remove(priorityQueue.First());

                if (currentNode == end)
                {
                    // Found the shortest path to the destination
                    return distances[end];
                }

                if (distances[currentNode] == int.MaxValue) continue;

                if (_graph.AdjacencyList.TryGetValue(currentNode, out var neighbors))
                {
                    foreach (var neighbor in neighbors)
                    {
                        int newDist = distances[currentNode] + neighbor.Value;
                        if (newDist < distances[neighbor.Key])
                        {
                            distances[neighbor.Key] = newDist;
                            priorityQueue.Add((newDist, neighbor.Key));
                        }
                    }
                }
            }

            return distances.ContainsKey(end) ? distances[end] : int.MaxValue;
        }
    }
}