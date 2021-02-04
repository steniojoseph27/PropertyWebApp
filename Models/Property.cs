using System.ComponentModel.DataAnnotations;

namespace PropertyWebApp.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }
        public long YearBuilt { get; set; }
        public decimal ListPrice { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal GrossYield { get; set; }
    }
}