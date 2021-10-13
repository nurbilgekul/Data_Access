using ECommercialDb.Models.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercialDb.Models.Entities.Concrete
{
    public class Customer:BaseEntity
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public int Phone { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
