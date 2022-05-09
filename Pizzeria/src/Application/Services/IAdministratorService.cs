using Pizzeria.Application.Common;
using Pizzeria.Application.Common.DTOs;

namespace Pizzeria.Application.Services
{
    /// <summary>
    /// Represents a wrapper, in other words - service, for administration operations, such as "Modification of pizzas", "Viewing order list", etc...
    /// </summary>
    public interface IAdministratorService
    {
        /// <summary>
        /// Gets a list of orders.
        /// ** List can be filtered by date/price.
        /// </summary>
        /// <param name="filterByDate">Parameter to filter orders by date.</param>
        /// <param name="filterByPrice">Parameter to filter orders by price.</param>
        public IEnumerable<OrderDto> GetOrders(bool filterByDate, bool filterByPrice);

        /// <summary>
        /// Generates HTML report (as a string) on the work of a pizzeria for a certain period of time.
        /// </summary>
        /// <param name="fromDate">The date from which orders will be included in the report.</param>
        /// <param name="toDate">The date until which orders will be included in the report.</param>
        /// <returns>Returns a string containing HTML markup.</returns>
        public string GenerateReport(DateTimeOffset fromDate, DateTimeOffset toDate);
    }
}
