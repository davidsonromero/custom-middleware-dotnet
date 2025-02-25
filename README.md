# Custom Middleware API

This project demonstrates the implementation of custom middleware in a .NET 8 API. The goal is to apply recent studies in building middlewares for .NET APIs.

## Project Structure

- **Extensions**
  - `MiddlewareServicesExtensions.cs`: Contains extension methods to add custom middleware services to the service collection.
  - `AuthenticationServicesExtensions.cs`: Contains extension methods to add custom authentication services to the service collection.

- **Services**
  - **Authentication**
    - `JwtService.cs`: Implements JWT token generation.
  - **Middlewares**
    - `GenerateRequestGuidMiddlewareService.cs`: Middleware service to generate a request GUID.
    - `ApiKeyMiddlewareService.cs`: Middleware service to handle API key validation.
    - `ExceptionHandlingMiddlewareService.cs`: Middleware service to handle untreated exceptions.

- **Controllers**
  - `AuthController.cs`: Handles authentication-related endpoints, such as login.
  - `TestController.cs`: A test controller to verify the middleware functionality.

- **Program.cs**
  - Configures services, logging, authentication, CORS, and middleware pipeline.

## Key Features

- **Custom Middleware**
  - `GenerateRequestGuidMiddleware`: Generates a unique GUID for each request.
  - `LogMiddleware`: Logs request and response details.
  - `ApiKeyMiddleware`: Validates API keys for incoming requests.
  - `ExceptionHandlingMiddleware`: Treats untreated exceptions.

- **Authentication**
  - JWT-based authentication.

- **CORS Configuration**
  - Configured to allow specific origins and HTTP methods.

- **Swagger Integration**
  - Integrated Swagger for API documentation and testing.

## Usage

1. **Configure Services**
   - Add custom middleware and authentication services in `Program.cs`.

2. **Authentication**
   - Use `AuthController` to handle login and generate JWT tokens.

3. **Middleware**
   - Custom middlewares are added to the pipeline in `Program.cs`.

4. **Run the Application**
   - Use `dotnet run` to start the application.

## Endpoints

- **AuthController**
  - `POST /api/auth/login`: Authenticates a user and returns a JWT token. The authentication uses mock user data. Send "testuser" as username and "password" as password to authenticate.

- **TestController**
  - `GET /api/test`: A protected endpoint that requires a valid JWT token.
  - `GET /api/test/exception`: A protected endpoint that requires a valid JWT token used to test the exception handling middleware.

## Configuration

- **appsettings.json**
  - Configure JWT settings, logging, and other application settings.

## Dependencies

### ImplementingCustomMiddleware

- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.Data.SqlClient`
- `Serilog.AspNetCore`
- `Serilog.Sinks.Console`
- `Serilog.Sinks.File`
- `Swashbuckle.AspNetCore`

### ImplementingCustomMiddlewareApplication

- `Microsoft.Extensions.Configuration.Abstractions`
- `Microsofit.AspNetCore.Http.Abstractions`
- `System.IdentityModel.Tokens.Jwt`

## Conclusion

This project serves as a practical example of implementing custom middleware in a .NET 8 API, demonstrating key concepts such as middleware services, JWT authentication, and API documentation with Swagger.
