# 🧭 Teacher Computer Retrieval System

This command-line application helps IT teams calculate route information between school locations to efficiently retrieve broken computers.

---

## 🧠 1. The Problem: Planning a Retrieval Route

Imagine you're on an IT team tasked with collecting broken computers from schools in a region where roads:
- Are **one-way** (A → B does *not* mean B → A),
- Have **different lengths** depending on the direction.

The application answers critical logistical questions like:
- What is the **distance** of a specific route (e.g., A → B → C)?
- How many **routes exist** between two schools with certain constraints?
- What is the **shortest** route between two schools?
- How many **round trips** exist under a specific distance?

---

## 🏗️ 2. Project Structure

The project follows **Separation of Concerns**, dividing responsibilities into modular projects:

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



---

## ▶️ 3. How to Run the Program

### 🔧 Clone the Repository inside Visual Studio
```bash
git clone https://github.com/abdullahazmy/TeacherComputerRetrieval
```

Or you can use any other IDE or text editor like VScode for sure.


