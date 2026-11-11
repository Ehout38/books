# Books

ASP.NET Core MVC coursework with a library, user registration, and a small shop.

## Run

Open `Books.sln` in Visual Studio 2022 version 17.8 or later with the ASP.NET and web development workload and the .NET 8 SDK. Select Books and press F5.

From a terminal:

```text
dotnet run --project Books.csproj
```

Open `http://localhost:5101`.

## Features

- Book model, BookViewModel, singleton BookService, and BookController with Index and Add actions.
- Server validation for required fields, string lengths, publication year, email, password length, and age.
- UserController with Register and RegisterPost actions and ModelState.IsValid checks.
- Successful registration page, duplicate checks, and password hashing.
- Product names, available quantities, prices, and a session cart with a live item count after each request.
- Cart details, product removal, clear cart, checkout, and customer and order summaries.
- Server-side prices and stock checks at checkout.

Books, users, and stock are stored in memory and reset when the application restarts. Each browser session has its own cart and latest order. This is a coursework application; registration does not implement sign-in or payments.
