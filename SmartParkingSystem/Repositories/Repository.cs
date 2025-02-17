using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParkingSystem.Repositories
{
    public class Repository<T> where T : class
    {
        protected readonly IDbConnection _dbConnection; // Change from private to protected

        public Repository(IConfiguration configuration)
        {
            _dbConnection = new System.Data.SqlClient.SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        // Get all records
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {typeof(T).Name}s"; // Assuming your table names are the same as model names
            return await _dbConnection.QueryAsync<T>(query);
        }

        // Get by ID
        public async Task<T> GetByIdAsync(int id)
        {
            var query = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
        }

        // Add new record
        public async Task AddAsync(T entity)
        {
            var columns = string.Join(",", typeof(T).GetProperties().Select(p => p.Name));
            var parameters = string.Join(",", typeof(T).GetProperties().Select(p => "@" + p.Name));

            var query = $"INSERT INTO {typeof(T).Name}s ({columns}) VALUES ({parameters})";
            await _dbConnection.ExecuteAsync(query, entity);
        }

        // Update record
        public async Task UpdateAsync(T entity)
        {
            var setClause = string.Join(",", typeof(T).GetProperties().Select(p => $"{p.Name} = @{p.Name}"));
            var query = $"UPDATE {typeof(T).Name}s SET {setClause} WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, entity);
        }

        // Delete record
        public async Task DeleteAsync(int id)
        {
            var query = $"DELETE FROM {typeof(T).Name}s WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
