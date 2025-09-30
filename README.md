# Transaction Management System

This project consists of two parts: frontend and backend. Together, they function as a whole and provide the ability to handle user requests for retrieving existing transactions and creating new ones.

## Getting Started

Prerequisites

Backend:
- Visual Studio 2022
- .NET 8 SDK
- No database required (uses CSV files)

Frontend:
- Node.js v20.11.1 or higher
- npm or yarn

## Installation 

1. Clone the repository
- git clone https://github.com/sanjat80/Transaction-Management-System.git

2. Backend Setup
- Open TransactionManagementSystem.sln in Visual Studio 2022
- The solution should automatically restore NuGet packages
All required .NET packages are included in the project file.

The backend shpuld be available at http://localhost:5211, with API endpoints accessible through Swagger at /swagger.

3. Frontend Setup
- cd frontend
- npm install (This will install all dependencies including React and other libraries (all required packages are in package.json))

The frontend should be available at http://localhost:3000.

## Configuration
Backend:
- No environment variables needed
- CSV file is located within the solution at: Repositories/Data/data.csv
All configuration is handled in appsettings.json. Transactions are stored in a CSV file located within the same solution as the project itself. New transactions are simply appended, without modifying the existing one

Frontend:
- No special configuration required
- Transactions Management API URL is configured in src/config.js
New transactions should be displayed inside existing table as soon as they are added.

## Running the Application
Backend:
- In Visual Studio: Press F5 or Ctrl+F5
- OR using command line: 
  - cd backend
  - dotnet run

Frontend:
- using command line:
  - cd frontend
  - npm start
 
## API Documentation
Base URL: https://localhost:5211/api/transactions
Endpoints:
1. GET
Returns all data from CSV file.

Response:
```json
[
  {
    "transactionDate": "2025-03-01T00:00:00",
    "accountNumber": "7289-3445-1121",
    "accountHolderName": "Maria Johnson",
    "amount": 150,
    "status": "Settled"
  },
  {
    "transactionDate": "2025-03-02T00:00:00",
    "accountNumber": "1122-3456-7890",
    "accountHolderName": "John Smith",
    "amount": 75.5,
    "status": "Pending"
  },
  {
    "transactionDate": "2025-03-03T00:00:00",
    "accountNumber": "3344-5566-7788",
    "accountHolderName": "Robert Chen",
    "amount": 220.25,
    "status": "Settled"
  },
  {
    "transactionDate": "2025-03-04T00:00:00",
    "accountNumber": "8899-0011-2233",
    "accountHolderName": "Sarah Williams",
    "amount": 310.75,
    "status": "Failed"
  },
  {
    "transactionDate": "2025-03-04T00:00:00",
    "accountNumber": "9988-7766-5544",
    "accountHolderName": "David Garcia",
    "amount": 45.99,
    "status": "Pending"
  },
  {
    "transactionDate": "2025-03-05T00:00:00",
    "accountNumber": "2233-4455-6677",
    "accountHolderName": "Emily Taylor",
    "amount": 500,
    "status": "Settled"
  },
  {
    "transactionDate": "2025-03-06T00:00:00",
    "accountNumber": "1357-2468-9012",
    "accountHolderName": "Michael Brown",
    "amount": 99.95,
    "status": "Settled"
  },
  {
    "transactionDate": "2025-03-07T00:00:00",
    "accountNumber": "5551-2345-6789",
    "accountHolderName": "Jennifer Lee",
    "amount": 175.25,
    "status": "Pending"
  },
  {
    "transactionDate": "2025-03-08T00:00:00",
    "accountNumber": "7890-1234-5678",
    "accountHolderName": "Thomas Wilson",
    "amount": 62.5,
    "status": "Failed"
  },
  {
    "transactionDate": "2025-03-08T00:00:00",
    "accountNumber": "1212-3434-5656",
    "accountHolderName": "Jessica Martin",
    "amount": 830,
    "status": "Settled"
  },
  {
    "transactionDate": "2025-03-09T00:00:00",
    "accountNumber": "9876-5432-1011",
    "accountHolderName": "Christopher Davis",
    "amount": 124.75,
    "status": "Pending"
  },
  {
    "transactionDate": "2025-03-10T00:00:00",
    "accountNumber": "4646-8282-1919",
    "accountHolderName": "Amanda Robinson",
    "amount": 300.5,
    "status": "Settled"
  }
]
```
2. POST (new transaction data are being sent through RequestBody)
Creates new transaction and adds it to the csv file.

Request example:

```json
[
{
  "transactionDate": "2025-09-30T11:24:11.878Z",
  "accountNumber": "0822-3121-2678",
  "accountHolderName": "Example account holder",
  "amount": 100.00
}
]
```
## Manual Testing:
1. Start both backend and frontend
2. Open http://localhost:3000 in browser
3. Verify data loads from CSV file
4. Test adding new data through the interface
5. Confirm CSV file updates accordingly (new data should be automatically displayed inside existing table)

## Architecture and project structure
The main project is composed of two parts: the backend and the frontend.

### Backend

The backend component of the system represents the "brain of the application." It exposes an API with two methods:
- GetAllTransactions – used to fetch existing transactions.
- CreateTransaction – used to add new transactions (transactions are added one by one).
The backend follows a three-layer architecture:

#### Repositories – Data Access Layer
It has its own contract (an interface) that defines the basic methods for reading data from the file and writing data into the file, along with an implementation of that contract that contains the concrete file access logic.

#### Services – Business Logic Layer
In this specific case, there is no advanced or complex business logic, since the functionalities are simple. However, in the case of more complex systems, this is where the processing of data received from the repository layer would take place.
Specifically, this service supplements the transactions created by the user by randomly assigning one of the three possible transaction statuses. It also has its own contract and its implementation.

#### Controllers – Endpoints Layer
In this layer, all functionalities developed in the previous two layers are exposed to the users through endpoints. There are two main endpoints: one for HTTP GET and one for HTTP POST. They do not contain any special logic beyond receiving user input, validating it, and forwarding it to the lower layers for further processing. Basic validations are handled using existing validation annotations.

Other helper folders: 

#### Models – Shared Models Library
A separate folder contains the models used across the application, accessible to all layers since the same models are used everywhere. This folder also includes the enum that defines the three possible transaction statuses, as well as the mappers that serve to map CSV columns to class properties.

#### Exceptions - global exception handling and exception types
This folder is being used to save different types of exception that could possibly happen while running this application, including exception handling middleware. This middleware wraps the execution of all incoming HTTP requests, and if something goes wrong while processing the request, it catches the exception before it reaches the client. 

### Frontend
```text
frontend/
├── public/                 # Static assets served by web server
├── src/                   # Source code - main application logic
├── package.json           # Project dependencies and scripts
├── package-lock.json      # Exact dependency versions
└── README.md              # Project documentation

#### Configurations
```text
src/
├── config/
│   └── api.js             # Backend API configuration (base URL, endpoints)

Purpose: Centralized configuration for API communication. Contains:
Base URL of the backend (http://localhost:5211)

```text
src/
├── api/
│   └── transactionApi.js  # Axios-based API calls to backend

Purpose: Handles all HTTP communication with the backend using Axios. Contains:
1. getAllTransactions() - Fetches all transactions from backend
2. createTransaction() - Sends new transaction to backend
Automatic error handling and response parsing.

```text
src/
├── components/            # Reusable UI components
│   ├── TransactionList.js    # Displays list of transactions
│   ├── TransactionForm.js    # Form for adding new transactions
│   └── (other components)    # Additional UI elements

Purpose: Modular React components following component-based architecture.
