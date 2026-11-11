using Books.Models;
using Microsoft.AspNetCore.Identity;

namespace Books.Services;

public class UserService
{
    private readonly object sync = new();
    private readonly List<User> users = new();
    private readonly PasswordHasher<User> hasher = new();

    public bool Register(User model)
    {
        lock (sync)
        {
            if (users.Any(user => user.Username.Equals(model.Username.Trim(), StringComparison.OrdinalIgnoreCase)
                || user.Email.Equals(model.Email.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            var user = new User
            {
                Id = users.Count + 1,
                Username = model.Username.Trim(),
                Email = model.Email.Trim(),
                Age = model.Age
            };
            user.Password = hasher.HashPassword(user, model.Password);
            users.Add(user);
            return true;
        }
    }
}
