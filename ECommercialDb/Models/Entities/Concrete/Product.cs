using ECommercialDb.Models.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercialDb.Models.Entities.Concrete
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public int QuantityPerUnit { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }
}
