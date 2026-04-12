# Job Portal API

A simple backend project for a job portal where recruiters can post jobs and users can apply.

## Features

- User and Recruiter login (JWT authentication)
- Recruiter can create and manage job posts
- Users can view jobs and apply
- Resume upload (basic file storage)
- APIs built with ASP.NET Core

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication

## Project Structure

- Controllers → Handle API requests
- Models → Database entities
- DTOs → Data transfer objects
- Services → Business logic

## How to Run

1. Clone the repository
2. Open in Visual Studio
3. Update database connection string in `appsettings.json`
4. Run `Update-Database` (Package Manager Console)
5. Start the project

## API Testing

- Use Postman to test endpoints
- First login to get JWT token
- Use token in Authorization header: `Bearer <token>`

## Future Improvements

- Store files in cloud (AWS / Azure)
- Add pagination for job listings
- Improve validation and error handling

## Author

Nikhil