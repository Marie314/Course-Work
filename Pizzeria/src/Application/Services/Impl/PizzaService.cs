using Pizzeria.Application.Common.DTOs;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Repositories;

namespace Pizzeria.Application.Services.Impl
{
    public class PizzaService : IPizzaService
    {
        private readonly IRepository<Pizza> _pizzaRepository;

        public PizzaService(IRepository<Pizza> pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public async Task AddPizzaAsync(PizzaDto pizza)
        {
            await _pizzaRepository.AddAsync(new Pizza
            {
                Description = pizza.Description,
                ImagePath = pizza.ImagePath ?? String.Empty,
                Name = pizza.Name,
                TotalPrice = pizza.TotalPrice,
            });
        }

        public async Task RemovePizzaAsync(PizzaDto pizza)
        {
            await _pizzaRepository.RemoveAsync(pizza.Id);
        }

        public async Task UpdatePizzaAsync(PizzaDto pizza)
        {
            await _pizzaRepository.UpdateAsync(new Pizza
            {
                Id = pizza.Id,
                Description = pizza.Description,
                ImagePath = pizza.ImagePath,
                Name = pizza.Name,
                TotalPrice = pizza.TotalPrice,
            });
        }

        public IEnumerable<PizzaDto> GetPizzas()
        {
            var pizzas = _pizzaRepository.GetAll().Select(pizza =>
                new PizzaDto
                {
                    Id = pizza.Id,
                    Description = pizza.Description,
                    Name = pizza.Name,
                    TotalPrice = pizza.TotalPrice,
                    ImagePath = pizza.ImagePath,
                });

            return pizzas;
        }
    }
}
