# Transaction Management System

This project consists of two parts: frontend and backend. Together, they function as a whole and provide the ability to handle user requests for retrieving existing transactions and creating new ones.
The backend is implemented in ASP .NET 8, using Visual Studio 2022, while the frontend is developed with Node.js in combination with the React framework, using VS Code.
Backend

The backend component of the system represents the "brain of the application." It exposes an API with two methods:
- GetAllTransactions – used to fetch existing transactions.
- CreateTransaction – used to add new transactions (transactions are added one by one).

Transactions are stored in a CSV file located within the same solution as the project itself. New transactions are simply appended, without modifying the existing ones.

Architecture and project structure

The backend follows a three-layer architecture:

Repositories – Data Access Layer
It has its own contract (an interface) that defines the basic methods for reading data from the file and writing data into the file, along with an implementation of that contract that contains the concrete file access logic.

Service – Business Logic Layer
In this specific case, there is no advanced or complex business logic, since the functionalities are simple. However, in the case of more complex systems, this is where the processing of data received from the repository layer would take place.
Specifically, this service supplements the transactions created by the user by randomly assigning one of the three possible transaction statuses. It also has its own contract and its implementation.

Controllers – Endpoints Layer
In this layer, all functionalities developed in the previous two layers are exposed to the users through endpoints. There are two main endpoints: one for HTTP GET and one for HTTP POST. They do not contain any special logic beyond receiving user input, validating it, and forwarding it to the lower layers for further processing. Basic validations are handled using existing validation annotations.

Other helper folders: 

Models – Shared Models Library
A separate folder contains the models used across the application, accessible to all layers since the same models are used everywhere. This folder also includes the enum that defines the three possible transaction statuses, as well as the mappers that serve to map CSV columns to class properties.

Exceptions - global exception handling and exception types
This folder is being used to save different types of exception that could possibly happen while running this application, including exception handling middleware. This middleware wraps the execution of all incoming HTTP requests, and if something goes wrong while processing the request, it catches the exception before it reaches the client. 
