using System;
using System.Data;
using System.Security.Cryptography;
using Dapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SmartParkingSystem.Models;
using SmartParkingSystem.Repositories.IRepositories;
using SmartParkingSystem.Infrastructure; // Assuming IDatabaseHelper is defined here

namespace SmartParkingSystem.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IDatabaseHelper _databaseHelper;

        public AuthenticationRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public RegisterViewModel register(RegisterViewModel user)
        {
            // Hash the password before saving
            string hashedPassword = HashPassword(user.Password);
            user.Password = hashedPassword;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            using IDbConnection db = _databaseHelper.GetConnection;
            string query = @"
                INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role, CreatedAt, UpdatedAt)
                VALUES (@FirstName, @LastName, @Email, @PasswordHash, @Role, @CreatedAt, @UpdatedAt);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = hashedPassword,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            user.UserID = db.ExecuteScalar<int>(query, parameters);
            return user;
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));

            return Convert.ToBase64String(salt) + ":" + hashed;
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string[] parts = storedHash.Split(':');
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            string enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));

            return enteredHash == parts[1];
        }
    }
}
