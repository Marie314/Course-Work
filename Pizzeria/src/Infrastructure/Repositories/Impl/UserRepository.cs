using System.Data.SqlClient;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Helpers.Converters;

namespace Pizzeria.Infrastructure.Repositories.Impl
{
    internal class UserRepository : IRepository<User>
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> AddAsync(User entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@Name", Value = entity.Name },
                new SqlParameter { ParameterName = "@UserName", Value = entity.UserName },
                new SqlParameter { ParameterName = "@Password", Value = entity.Password },
                new SqlParameter { ParameterName = "@RoleId", Value = entity.RoleId },
            };

            var sql = "INSERT INTO Users\n"
                          + "VALUES (@Name, @UserName, @Password, @RoleId)\n"
                          + "SELECT SCOPE_IDENTITY()";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parameters);

                    var reader = await command.ExecuteReaderAsync();
                    reader.Read();

                    var id = Convert.ToInt32(reader.GetValue(0), null);
                    entity.Id = id;

                    return entity;
                }
            }
        }

        public IQueryable<User> GetAll()
        {
            var request = "SELECT Id, Name, UserName, Password, RoleId\n"
                              + "FROM Users";

            IEnumerable<User> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    var dataConverter = new DataConverter<User>(reader);
                    result = dataConverter.GetObjects();
                }
            }

            return result.AsQueryable();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "SELECT Id, Name, UserName, Password, RoleId\n"
                              + "FROM Users\n"
                              + "WHERE Id = @Id";

            User area;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    command.Parameters.Add(parameter);
                    var reader = await command.ExecuteReaderAsync();

                    var dataConverter = new DataConverter<User>(reader);
                    area = dataConverter.GetObject();
                }
            }

            return area;
        }

        public async Task RemovedAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "DELETE FROM Users\n"
                              + "WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    command.Parameters.Add(parameter);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(User entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@Id", Value = entity.Id },
                new SqlParameter { ParameterName = "@Name", Value = entity.Name },
                new SqlParameter { ParameterName = "@UserName", Value = entity.UserName },
                new SqlParameter { ParameterName = "@Password", Value = entity.Password },
                new SqlParameter { ParameterName = "@RoleId", Value = entity.RoleId },
            };

            var request = "UPDATE Users\n"
                              + "SET Name = @Name, UserName = @UserName, "
                              + "Password = @Password, RoleId = @RoleId\n"
                              + "WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    command.Parameters.AddRange(parameters);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
