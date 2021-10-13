using ECommercialDb.Models.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercialDb.Models.Entities.Concrete
{
    public class Order:BaseEntity
    {

        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal Freight { get; set; }



        public int ShipperId { get; set; }
        public virtual Shipper Shipper { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
