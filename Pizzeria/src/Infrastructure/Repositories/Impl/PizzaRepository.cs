using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Helpers.Converters;
using Pizzeria.Infrastructure.Repositories.Options;
using System.Data.SqlClient;

namespace Pizzeria.Infrastructure.Repositories.Impl
{
    public class PizzaRepository : IRepository<Pizza>
    {
        private readonly string _connectionString;

        public PizzaRepository(IRepositoryOptions options)
        {
            _connectionString = options.ConnectionString;
        }

        public async Task<Pizza> AddAsync(Pizza entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@Description", Value = entity.Description },
                new SqlParameter { ParameterName = "@TotalPrice", Value = entity.TotalPrice },
                new SqlParameter { ParameterName = "@ImagePath", Value = entity.ImagePath },
                new SqlParameter { ParameterName = "@Name", Value = entity.Name },
            };

            var sql = "INSERT INTO Pizzas (Description, TotalPrice, Name, ImagePath)\n"
                          + "VALUES (@Description, @TotalPrice, @Name, @ImagePath)\n"
                          + "SELECT SCOPE_IDENTITY()";

            try
            {
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IQueryable<Pizza> GetAll()
        {
            var request = "SELECT Id, Description, ImagePath, TotalPrice, Name\n"
                              + "FROM Pizzas";

            IEnumerable<Pizza> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    var dataConverter = new DataConverter<Pizza>(reader);
                    result = dataConverter.GetObjects();
                }
            }

            return result.AsQueryable();
        }

        public async Task<Pizza> GetByIdAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "SELECT Id, Description, TotalPrice, Name, ImagePath\n"
                              + "FROM Pizzas\n"
                              + "WHERE Id = @Id";

            Pizza area;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(request, connection))
                {
                    command.Parameters.Add(parameter);
                    var reader = await command.ExecuteReaderAsync();

                    var dataConverter = new DataConverter<Pizza>(reader);
                    area = dataConverter.GetObject();
                }
            }

            return area;
        }

        public async Task RemoveAsync(int id)
        {
            var parameter = new SqlParameter { ParameterName = "@Id", Value = id };

            var request = "DELETE FROM Pizzas\n"
                              + "WHERE Id = @Id";

            try
            {
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateAsync(Pizza entity)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@Id", Value = entity.Id },
                new SqlParameter { ParameterName = "@Description", Value = entity.Description },
                new SqlParameter { ParameterName = "@TotalPrice", Value = entity.TotalPrice },
                new SqlParameter { ParameterName = "@ImagePath", Value = entity.ImagePath },
                new SqlParameter { ParameterName = "@Name", Value = entity.Name },
            };

            var request = "UPDATE Pizzas\n"
                              + "SET Description = @Description, TotalPrice = @TotalPrice, "
                              + "Name = @Name, ImagePath = @ImagePath\n"
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
