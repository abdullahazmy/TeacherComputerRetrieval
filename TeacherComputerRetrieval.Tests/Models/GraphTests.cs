using TeacherComputerRetrieval.Core.Models;

namespace TeacherComputerRetrieval.Tests.Models
{
    /// <summary>
    /// Contains all unit tests for the Graph class, focusing on its
    /// constructor and parsing logic.
    /// </summary>
    public class GraphTests
    {
        [Fact]
        public void Constructor_WithValidInput_ParsesGraphCorrectly()
        {
            // Arrange: A simple, well-formed input string.
            var input = "AB1, BC99, CA5";

            // Act: Create the graph object.
            var graph = new Graph(input);

            // Assert: Verify the internal state of the graph.
            Assert.Equal(3, graph.Nodes.Count);
            Assert.Contains('A', graph.Nodes);
            Assert.Contains('B', graph.Nodes);
            Assert.Contains('C', graph.Nodes);

            // Check that the adjacency list was built as expected.
            Assert.True(graph.AdjacencyList.ContainsKey('A'));
            Assert.True(graph.AdjacencyList.ContainsKey('B'));
            Assert.True(graph.AdjacencyList.ContainsKey('C'));

            // Spot-check a few of the distances.
            Assert.Equal(1, graph.AdjacencyList['A']['B']);
            Assert.Equal(99, graph.AdjacencyList['B']['C']);
            Assert.Equal(5, graph.AdjacencyList['C']['A']);
        }

        [Fact]
        public void Constructor_WithMalformedInput_GracefullyIgnoresBadEntries()
        {
            // Arrange: A mixed string containing valid, invalid, and incomplete entries.
            var input = "AB1, XY, Z9, QW2, , B C 4, DE";

            // Act: Create the graph object. The parser should be resilient.
            var graph = new Graph(input);

            // Assert: Verify that only the valid routes were parsed.
            // Expected valid routes: AB1, QW2.
            Assert.Equal(4, graph.Nodes.Count); // Should contain A, B, Q, W.
            Assert.Contains('A', graph.Nodes);
            Assert.Contains('W', graph.Nodes);

            // Verify that only nodes with outbound routes are keys in the AdjacencyList.
            Assert.Equal(2, graph.AdjacencyList.Count);
            Assert.True(graph.AdjacencyList.ContainsKey('A'));
            Assert.True(graph.AdjacencyList.ContainsKey('Q'));

            // Verify that malformed entries were NOT added.
            Assert.False(graph.AdjacencyList.ContainsKey('X'));
            Assert.False(graph.AdjacencyList.ContainsKey('Z'));
            Assert.False(graph.AdjacencyList.ContainsKey('D'));
        }

        [Theory]
        [InlineData(null)]      // Test case for a null string
        [InlineData("")]        // Test case for an empty string
        [InlineData(" ")]       // Test case for a whitespace string
        [InlineData(", ,")]     // Test case for separators but no data
        public void Constructor_WithEmptyOrNullInput_CreatesEmptyGraph(string input)
        {
            // Act: Create a graph from various "empty" inputs.
            var graph = new Graph(input);

            // Assert: In all these cases, the resulting graph should be empty.
            Assert.Empty(graph.AdjacencyList);
            Assert.Empty(graph.Nodes);
        }
    }
}