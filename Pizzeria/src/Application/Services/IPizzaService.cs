using Pizzeria.Application.Common.DTOs;

namespace Pizzeria.Application.Services
{
    /// <summary>
    /// Represents a wrapper, in other words - service, to get a list of pizzas.
    /// </summary>
    public interface IPizzaService
    {

        /// <summary>
        /// Updates and validates a specific pizza.
        /// </summary>
        /// <param name="pizza">Pizza to update.</param>
        public Task UpdatePizzaAsync(PizzaDto pizza);

        /// <summary>
        /// Removes and validates a specific pizza.
        /// </summary>
        /// <param name="pizza">Pizza to remove.</param>
        public Task RemovePizzaAsync(PizzaDto pizza);

        /// <summary>
        /// Addds and validates a specific pizza.
        /// </summary>
        /// <param name="pizza">Pizza to add.</param>
        /// <returns></returns>
        public Task AddPizzaAsync(PizzaDto pizza);

        /// <summary>
        /// Gets pizzas.
        /// </summary>
        /// <returns>Returns a list of pizzas.</returns>
        public IEnumerable<PizzaDto> GetPizzas();
    }
}
