using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyWebApp.ViewModels
{
    public class ListPropertyViewModel
    {
        public long PropertyId { get; set; }
        public string Address { get; set; }
        public long YearBuilt { get; set; }
        public decimal ListPrice { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal GrossYield
        {
            get {
                return (MonthlyRent * 12 / ListPrice) * 100;
            }
        }
    }
}
