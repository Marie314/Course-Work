using System.Data.SqlClient;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Helpers.Converters;

namespace Pizzeria.Infrastructure.Repositories.Impl
{
    internal class RoleRepository : IRepository<Role>
    {
        private readonly string _connectionString;

        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Role> AddAsync(Role entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@Name", Value = entity.Name },
            };

            var sql = "INSERT INTO Roles\n"
                          + "VALUES (@Name)\n"
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

        public IQueryable<Role> GetAll()
        {
            var request = "SELECT Id, Name\n"
                              + "FROM Roles";

            IEnumerable<Role> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    var dataConverter = new DataConverter<Role>(reader);
                    result = dataConverter.GetObjects();
                }
            }

            return result.AsQueryable();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "SELECT Id, Name\n"
                              + "FROM Roles\n"
                              + "WHERE Id = @Id";

            Role area;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    command.Parameters.Add(parameter);
                    var reader = await command.ExecuteReaderAsync();

                    var dataConverter = new DataConverter<Role>(reader);
                    area = dataConverter.GetObject();
                }
            }

            return area;
        }

        public async Task RemovedAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "DELETE FROM Roles\n"
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

        public async Task UpdateAsync(Role entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@Id", Value = entity.Id },
                new SqlParameter { ParameterName = "@Name", Value = entity.Name },
            };

            var request = "UPDATE Roles\n"
                              + "SET Name = @Name"
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
