using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Helpers.Converters;
using System.Data.SqlClient;

namespace Pizzeria.Infrastructure.Repositories.Impl
{
    internal class OrderRepository : IRepository<Order>
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Order> AddAsync(Order entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@OrderDate", Value = entity.OrderDate },
                new SqlParameter { ParameterName = "@TotalPrice", Value = entity.TotalPrice },
                new SqlParameter { ParameterName = "@UserId", Value = entity.UserId },
            };

            var sql = "INSERT INTO Orders\n"
                          + "VALUES (@OrderDate, @TotalPrice, @UserId)\n"
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

        public IQueryable<Order> GetAll()
        {
            var request = "SELECT Id, OrderDate, TotalPrice, UserId\n"
                              + "FROM Orders";

            IEnumerable<Order> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    var dataConverter = new DataConverter<Order>(reader);
                    result = dataConverter.GetObjects();
                }
            }

            return result.AsQueryable();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "SELECT Id, OrderDate, TotalPrice, UserId\n"
                              + "FROM Orders\n"
                              + "WHERE Id = @Id";

            Order area;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    command.Parameters.Add(parameter);
                    var reader = await command.ExecuteReaderAsync();

                    var dataConverter = new DataConverter<Order>(reader);
                    area = dataConverter.GetObject();
                }
            }

            return area;
        }

        public async Task RemovedAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "DELETE FROM Orders\n"
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

        public async Task UpdateAsync(Order entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@Id", Value = entity.Id },
                new SqlParameter { ParameterName = "@OrderDate", Value = entity.OrderDate },
                new SqlParameter { ParameterName = "@TotalPrice", Value = entity.TotalPrice },
                new SqlParameter { ParameterName = "@UserId", Value = entity.UserId },
            };

            var request = "UPDATE Orders\n"
                              + "SET OrderDate = @OrderDate, TotalPrice = @TotalPrice, "
                              + "UserId = @UserId\n"
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
