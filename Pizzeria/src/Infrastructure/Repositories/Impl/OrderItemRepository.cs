using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Helpers.Converters;
using Pizzeria.Infrastructure.Repositories.Options;
using System.Data.SqlClient;

namespace Pizzeria.Infrastructure.Repositories.Impl
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly string _connectionString;

        public OrderItemRepository(IRepositoryOptions options)
        {
            _connectionString = options.ConnectionString;
        }

        public async Task<OrderItem> AddAsync(OrderItem entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@OrderId", Value = entity.OrderId },
                new SqlParameter { ParameterName = "@PizzaId", Value = entity.PizzaId },
                new SqlParameter { ParameterName = "@Price", Value = entity.Price },
                new SqlParameter { ParameterName = "@Quantity", Value = entity.Quantity },
                new SqlParameter { ParameterName = "@PizzaName", Value = entity.PizzaName },
            };

            var sql = "INSERT INTO OrderItems (OrderId, PizzaId, Price, Quantity, PizzaName)\n"
                          + "VALUES (@OrderId, @PizzaId, @Price, @Quantity, @PizzaName)\n"
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

        public IQueryable<OrderItem> GetAll()
        {
            var request = "SELECT Id, OrderId, PizzaId, Price, Quantity, PizzaName\n"
                              + "FROM OrderItems";

            IEnumerable<OrderItem> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    var dataConverter = new DataConverter<OrderItem>(reader);
                    result = dataConverter.GetObjects();
                }
            }

            return result.AsQueryable();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "SELECT Id, OrderId, PizzaId, Price, Quantity, PizzaName\n"
                              + "FROM OrderItems\n"
                              + "WHERE Id = @Id";

            OrderItem area;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    command.Parameters.Add(parameter);
                    var reader = await command.ExecuteReaderAsync();

                    var dataConverter = new DataConverter<OrderItem>(reader);
                    area = dataConverter.GetObject();
                }
            }

            return area;
        }

        public async Task RemoveAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "DELETE FROM OrderItems\n"
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

        public async Task UpdateAsync(OrderItem entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@Id", Value = entity.Id },
                new SqlParameter { ParameterName = "@OrderId", Value = entity.OrderId },
                new SqlParameter { ParameterName = "@PizzaId", Value = entity.PizzaId },
                new SqlParameter { ParameterName = "@Price", Value = entity.Price },
                new SqlParameter { ParameterName = "@Quantity", Value = entity.Quantity },
                new SqlParameter { ParameterName = "@PizzaName", Value = entity.PizzaName },
            };

            var request = "UPDATE OrderItems\n"
                              + "SET OrderId = @OrderId, Price = @Price, "
                              + "PizzaId = @PizzaId, Quantity = @Quantity, PizzaName = @PizzaName\n"
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
