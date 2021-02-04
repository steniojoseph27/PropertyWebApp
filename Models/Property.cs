using System.ComponentModel.DataAnnotations;

namespace PropertyWebApp.Models
{
    public class address
    {
        [Required]
        [Display(Name = "Address")]
        public string address1 { get; set; }

        public string city { get; set; }

        public string country { get; set; }

        public string state { get; set; }

        public string zip { get; set; }
    }

    public class financial
    {
        [Required]
        [Display(Name = "List Price")]
        public decimal listPrice { get; set; }

        [Required]
        [Display(Name = "Monthly Rent")]
        public decimal monthlyRent { get; set; }

        [Display(Name = "Gross Yield")]
        public decimal grossYield
        {
            get
            {
                return monthlyRent * 12 / listPrice;
            }
        }
    }

    public class physical
    {
        [Required]
        [Display(Name = "Year Built")]
        public int yearBuilt { get; set; }
    }

}