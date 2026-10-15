# Warehouse API

ASP.NET Core API for warehouse products. Products are stored in PostgreSQL. Anyone can read them. Creating and updating requires a logged-in user. Deleting requires an admin.

Passwords are stored as hashes. Login returns a JWT. An admin account is created on startup from configuration.

## Run

Copy `Warehouse.Api/example.appsettings.json` to `Warehouse.Api/appsettings.json` and add your connection string, JWT key, and admin email and password.

Apply the database schema and start the API:

```powershell
dotnet ef database update --project Warehouse.Api
dotnet run --project Warehouse.Api
```

The API listens on `http://localhost:5140`. Swagger is at `http://localhost:5140/swagger` in Development.

## Auth

| Action | Who |
|---|---|
| Register and login | Anyone |
| Read products | Anyone |
| Create or update a product | Logged-in user |
| Delete a product | Admin |

Register creates a user. Login returns the token. In Swagger, paste that token into **Authorize**.
