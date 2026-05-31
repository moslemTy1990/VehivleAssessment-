# E-Mobility Charging Session Assessment

## Introduction
This assessment is set in the context of E-Mobility, focusing on the management of EV (Electric Vehicle) charging sessions.
You will build a simple API to handle charging sessions, simulating interactions between Charge Point Operators (CPOs) and end users.

Try to spend no more than **1 hour** on this assessment. The goal is to demonstrate and show off your coding skills, not to complete every feature.

## Objective
Take the provided ASP.NET Core project and develop a minimal RESTful API that allows:
- Submitting new charging sessions.
- Users to retrieve their own charging sessions.
- **Note:** The provided code contains some intentionally incomplete validation logic that is not well-written. Your task is to improve upon it.

## Requirements
1. **Receive Charging Sessions**
    - Implement an endpoint to accept new charging session data.
        - Sequential calls for the same session should be viewed and stored as updates.
    - Use in-memory storage (e.g., a static list) for simplicity. 
    - Each session should include: session ID, user ID, charge point ID, start time, and energy consumed (kWh).
    - Example JSON:
```json
 {
   "sessionId": "string",
   "userId": "string",
   "chargePointId": "string",
   "startTime": "2025-05-01T08:00:00Z",
   "endTime": "2025-05-02T08:00:00Z",
   "energyKwh": 00.0
 }
```

2. **Retrieve Charging Sessions for a User**
    - Implement an endpoint for users to retrieve all their charging sessions.
    - Charging sessions should be returned newest first.

## Bonus (Optional, if time allows)
If you have time, consider implementing one or more of the following features, or suggest your own improvements:
- Add error handling.
- Support filtering by date or other properties.
- Add caching.
- Implement unit tests.
- Use a database (e.g., SQLite or EF-core) instead of in-memory storage.
- Add authentication (e.g., JWT or static key) to secure the endpoints.
- Implement rate limiting to prevent abuse of the API.
- Add logging to track API usage and errors.

## Getting Started
- The project is already set up with ASP.NET Core and Swagger for API documentation.
- Add your models, controllers, and logic in the provided structure.
- To run the project:
  1. Restore dependencies: `dotnet restore`
  2. Build the project: `dotnet build`
  3. Run the project: `dotnet run`
  4. Access Swagger UI at `https://localhost:5001/swagger` (or the port shown in the console)

## Submission
- Ensure your code builds and runs.
- Submit your solution as a Git repository or a zip file.

---
Good luck! If you have any questions, feel free to ask.
