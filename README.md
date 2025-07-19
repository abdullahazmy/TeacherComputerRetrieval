# Teacher Computer Retrieval System

This document provides a complete overview of the Teacher Computer Retrieval System, a command-line application designed to calculate route information between different school locations.

## 1. The Problem: Planning a Retrieval Route

Imagine you are on an IT team responsible for picking up broken computers from schools in a region. The roads in this region can be tricky:
*   A road from School A to School B might not mean there's a road back from B to A.
*   Even if there is a road back, it might be a different length.
*   We call these one-way, directed routes.

This program helps the IT team by answering critical questions about the routes, such as:
1.  What is the distance of a specific, pre-planned path (e.g., A → B → C)?
2.  How many different routes exist between two schools?
3.  What is the shortest possible route between two schools?
4.  How many round trips exist that are shorter than a certain distance?

## 2. Project Structure

The project is logically separated into different projects, each with a single responsibility. This is a standard practice known as Separation of Concerns, which makes the application easier to understand, test, and maintain.

```text
TeacherComputerRetrieval/
├── TeacherComputerRetrieval.Core/ # Core business models and interfaces
│ ├── Models/Graph.cs
│ ├── Interfaces/IRouteService.cs
│ └── Exceptions/RouteNotFoundException.cs
│
├── TeacherComputerRetrieval.Infrastructure/ # Data layer
│ └── Repositories/RouteRepository.cs
│
├── TeacherComputerRetrieval.Services/ # Business logic
│ └── RouteService.cs
│
├── TeacherComputerRetrieval.CLI/ # Command-line interface
│ └── Program.cs
│
└── TeacherComputerRetrieval.Tests/ # Unit tests
├── Models/GraphTests.cs
└── Services/RouteServiceTests/
```

---

**Sample of the planning process**


<img width="1357" height="700" alt="image" src="https://github.com/user-attachments/assets/c1c6b228-587a-4257-a29c-e21c0fe13b26" />


---

## ▶️ 3. How to Run the Program

### 🔧 Clone the Repository inside Visual Studio
```bash
git clone https://github.com/abdullahazmy/TeacherComputerRetrieval
```

> ℹ️ After cloning, make sure to set `TeacherComputerRetrieval.CLI` as the Startup Project to run the app without Docker.


Or you can use any other IDE or text editor like VScode for sure.


---

## 4. Technical Deep Dive: The Code Walkthrough

The application's journey begins in `Program.cs` and flows through the `RouteService` to perform its calculations.

### Entry Point: `Program.cs`

This file is the starting point. Its job is simple:
1.  Get the route data from the user.
2.  Set up the necessary services.
3.  Call the service methods to answer the 8 required questions.
4.  Print the results to the console.

```csharp
// TeacherComputerRetrieval.CLI/Program.cs
class Program
{
    static void Main(string[] args)
    {
        // ... (Get user input) ...

        // 1. Initialize the services with user input
        var repository = new RouteRepository(userInput);
        var routeService = new RouteService(repository.GetGraph());

        // 2. Execute and print answers to all 8 questions
        Console.WriteLine($"1. Output #1: {routeService.GetDistanceOfRoute("A-B-C")}");
        Console.WriteLine($"2. Output #2: {routeService.GetDistanceOfRoute("A-E-B-C-D")}");
        // ... and so on for all 8 outputs ...
        Console.WriteLine($"7. Output #7: {routeService.FindShortestRoute('B', 'B')}");
    }
}
```

Now, let's dive into the RouteService methods that Program.cs calls.

**Method** 1: GetDistanceOfRoute
- Goal: To calculate the total distance of a specific, fixed route like "A-B-C".
Code (RouteService.cs):

```cs
public string GetDistanceOfRoute(string route)
{
    var nodes = route.Split('-');
    int totalDistance = 0;
    for (int i = 0; i < nodes.Length - 1; i++)
    {
        // ... (error handling) ...
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
```

**Chosen Approach**: A simple loop and sum. The input string is split into individual nodes. The code then iterates through each pair of consecutive nodes (A-B, then B-C, etc.), looks up the distance for that specific hop in the graph's data structure, and adds it to a running total.

**Why This Approach?** This is the most direct and efficient solution. The path is already known, so no complex graph traversal or path-finding algorithm is needed. We are simply performing a series of lookups and additions.

**Alternative Approaches:** There are no realistic alternatives for this specific task. Any other method would be unnecessarily complex for what is fundamentally a simple summation problem.


---

**Methods 2 & 3**: CountTripsWithMaxStops & CountTripsWithExactStops
- Goal: To find how many distinct paths exist between two nodes, constrained by the number of "stops" (or edges) in the path.

Code (RouteService.cs):

```cs
public int CountTripsWithExactStops(char start, char end, int exactStops)
{
    return ExactStopsRecursive(start, end, 0, exactStops);
}

private int ExactStopsRecursive(char current, char end, int stops, int exactStops)
{
    if (stops > exactStops) return 0; // Pruning condition

    if (stops == exactStops)
    {
        return current == end ? 1 : 0; // Base case: Did we land on the target?
    }
    
    int count = 0;
    // ... (Recursive step: explore neighbors) ...
    return count;
}
```

**Chosen Approach:** Depth-First Search (DFS) implemented with recursion. Recursion provides a very clean and intuitive way to explore a graph's paths. The function ExactStopsRecursive represents the state of being at a current node after a certain number of stops. From there, it recursively calls itself for all neighboring nodes, incrementing the stop count. The process stops (the "base case") when the number of stops matches the target.

**Why This Approach?** DFS is perfectly suited for problems that require exploring all possible paths from a start to an end node. Recursion makes the code for exploring each branch of the graph elegant and easy to read.

**Alternative Approaches:**
- Breadth-First Search (BFS): One could also use BFS with a Queue to solve this. You would store tuples of (currentNode, currentStops) in the queue. While this works, the recursive DFS approach is often considered more natural for exhaustively exploring every possible path to its conclusion.


---

**Method 4:** FindShortestRoute
- **Goal:** To find the route with the minimum total distance between two nodes. This is a classic computer science problem.

Code (RouteService.cs):

```cs
public string FindShortestRoute(char start, char end)
{
    if (start != end)
    {
        // Find a simple A->C path
        int distance = FindShortestPath(start, end);
        return distance == int.MaxValue ? "NO SUCH ROUTE" : distance.ToString();
    }
    
    // Logic for shortest cycle (e.g., B->B)
    // ...
}

private int FindShortestPath(char start, char end)
{
    // ... (Implementation of Dijkstra's algorithm) ...
}
```

**Chosen Approach:** Dijkstra's Algorithm. This is the standard, industry-wide algorithm for finding the shortest path in a weighted graph where edge weights are non-negative (which our distances are). It works by intelligently exploring the graph, always prioritizing the path with the lowest current total distance. We also added special logic to handle the "cycle" case (like B to B) by finding the shortest path from B's neighbors back to B.

**Why This Approach?** Dijkstra's is both correct and highly efficient for this specific problem type. It is significantly faster than naively checking every single possible path.

**Alternative Approaches:**
- Breadth-First Search (BFS): This algorithm is excellent for finding the shortest path in terms of the number of stops, but it cannot be used here because it doesn't account for the distance (weight) of each hop.
- Bellman-Ford Algorithm: This algorithm is more powerful than Dijkstra's because it can handle graphs with negative edge weights. However, it is slower. Since our distances are always positive, using the more specialized and faster Dijkstra's algorithm is the better engineering choice.

---


Method 5: CountRoutesWithMaxDistance
- Goal: To count the number of different routes from a start to an end node where the total distance is less than a given limit.

Code (RouteService.cs):

```cs
public int CountRoutesWithMaxDistance(char start, char end, int maxDistance)
{
    return MaxDistanceRecursive(start, end, 0, maxDistance);
}

private int MaxDistanceRecursive(char current, char end, int currentDistance, int maxDistance)
{
    int count = 0;
    // ... (explore neighbors) ...
    int newDistance = currentDistance + neighbor.Value;
    if (newDistance < maxDistance)
    {
        if (neighbor.Key == end) count++;
        count += MaxDistanceRecursive(neighbor.Key, end, newDistance, maxDistance);
    }
    // ...
    return count;
}
```

Chosen Approach: A modified Depth-First Search (DFS) via recursion. This is very similar to how we counted trips by stops. The key difference is that the state we track includes the currentDistance of the path taken so far. We continue exploring a path only as long as its newDistance does not exceed the maxDistance limit.

Why This Approach? Just like before, DFS is the natural choice for exploring all possible paths. Modifying the "pruning" condition from "number of stops" to "total distance" is a straightforward change that makes the algorithm fit this new requirement perfectly.


Alternative Approaches: Any valid solution must fundamentally perform a DFS-like exploration of the graph, so there are no significantly different high-level algorithmic approaches.


---

Will update it to inlcude a documentation for unit testing In Sh'a Allah
