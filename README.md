# Tower_Of_Hanoi_Reflex_Framework
A Tower of Hanoi puzzle game built in Unity using the Reflex Dependency Injection framework.

# What I Built:
-Drag and drop disk movement
-Move validation (can't place big disk on small disk)
-Undo/Redo system (go back and forward through moves)
-Auto-solver (watch it solve recursively)
-UI feedback (shows valid/invalid moves)

# What I Learned:
- Dependency Injection with Reflex
- Service architecture (separating game logic from Unity code)
- Stateless vs Stateful design patterns
- Screen/World space conversions for dragging
- Recursive algorithms (Tower of Hanoi solution)
- Coroutines in services using a CoroutineRunner

# Architecture
The game uses 3 main services:
-DiskSpawnerService - Spawns and configures disks
-SettingsService - Handles undo/redo and auto-solve
-ValidationMovementUIService - Validates moves and triggers UI

All services are injected through Reflex, making the code:
-Easy to test
-Easy to modify
-Loosely coupled


https://github.com/user-attachments/assets/8eef12e4-3a13-431c-b9cf-7240fbe00831

