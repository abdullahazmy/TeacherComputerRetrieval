namespace TeacherComputerRetrieval.Core.Models
{
    /// <summary>
    /// Represents the directed graph of academies and routes.
    /// </summary>
    public class Graph
    {
        // Adjacency list: Key is the starting node, Value is a dictionary of (end_node, distance)
        public Dictionary<char, Dictionary<char, int>> AdjacencyList { get; } = new Dictionary<char, Dictionary<char, int>>();

        // A set of all unique nodes in the graph
        public HashSet<char> Nodes { get; } = new HashSet<char>();

        public Graph(string routeData)
        {
            ParseAndBuildGraph(routeData);
        }

        private void ParseAndBuildGraph(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            var routes = input.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var route in routes)
            {
                if (route.Length < 3 || !char.IsLetter(route[0]) || !char.IsLetter(route[1]) || !int.TryParse(route.AsSpan(2), out int distance))
                {
                    // Skip malformed entries
                    continue;
                }

                char start = route[0];
                char end = route[1];

                if (!AdjacencyList.ContainsKey(start))
                {
                    AdjacencyList[start] = new Dictionary<char, int>();
                }
                AdjacencyList[start][end] = distance;

                // Keep track of all unique nodes
                Nodes.Add(start);
                Nodes.Add(end);
            }
        }
    }
}