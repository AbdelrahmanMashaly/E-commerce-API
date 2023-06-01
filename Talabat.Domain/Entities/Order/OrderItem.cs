using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.Entities.Order
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {

        }
        public OrderItem(ProductItemOrdered product, decimal price, int qunatity)
        {
            Product = product;
            Price = price;
            Qunatity = qunatity;
        }

        public ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Qunatity { get; set; }
    }
}
