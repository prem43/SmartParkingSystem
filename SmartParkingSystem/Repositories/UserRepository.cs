using Dapper;
using SmartParkingSystem.Models;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace SmartParkingSystem.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<ApplicationUser> FindByUsernameAsync(string username)
        {
            var query = "SELECT * FROM Users WHERE UserName = @UserName";
            return await _dbConnection.QuerySingleOrDefaultAsync<ApplicationUser>(query, new { UserName = username });
        }
    }
}
