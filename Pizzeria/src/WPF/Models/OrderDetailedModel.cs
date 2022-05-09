using System;

namespace WPF.Models
{
    internal class OrderDetailedModel
    {
        public string UserName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string FormattedPrice
        {
            get => Price + " BYN";
        }

        public DateTimeOffset OrderDate { get; set; }
    }
}
