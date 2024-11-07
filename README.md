# To-Do List API
A simple API for managing to-do lists using ASP.NET Core Web API.

## Features
* User Registration and Login
* Personal To-Do Lists: Each user can manage their list
* CRUD Operations: Create, view, update, and delete to-do items

## Prerequisites
* .Net 8.0 or above
* Sql server for database
* Postman(Recommended)
* Visual Studio(Recommended)

## Usage
* Register: Create a new user by sending a POST request to /register with Username, Email, and Password.
* Login: Log in to receive a JWT token, then include the token in the Authorization header as Bearer {token} for accessing protected routes.
* CRUD Operations: Once logged in, you can create, retrieve, update, and delete your to-do items through the endpoints under /api/todo.


## Endpoints

| Method | Endpoint             | Description                        |
|--------|----------------------|------------------------------------|
| POST   | /register            | Register a new user               |
| POST   | /login               | Log in and get a token            |
| GET    | /api/todo            | Get all to-do items               |
| GET    | /api/todo/{id}       | Get a single to-do item           |
| POST   | /api/todo            | Add a new to-do item              |
| PUT    | /api/todo/{id}       | Update a to-do item               |
| DELETE | /api/todo/{id}       | Delete a to-do item               |

## License
This project is open-source and available under the MIT License.
