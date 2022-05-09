using Pizzeria.Application.Common.ReportModels;

namespace Pizzeria.Application.Common
{
    public class Report
    {
        public decimal TotalRevenue { get; set; }

        public PizzaRevenue PizzaRevenue { get; set; }

        public DayRevenue DayRevenue { get; set; }
    }
}
