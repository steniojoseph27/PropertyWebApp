using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyWebApp.ViewModels
{
    public class ListPropertyViewModel
    {
        public long propertyId { get; set; }
        public string address { get; set; }
        public long yearBuilt { get; set; }
        public decimal listPrice { get; set; }
        public decimal monthlyRent { get; set; }
        public decimal grossYield
        {
            get
            {
                return (monthlyRent * 12 / listPrice) * 100;
            }
        }
    }
}
