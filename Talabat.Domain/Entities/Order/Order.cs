using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order() { }
        public Order(string buyerEmail, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            this.shippingAddress = shippingAddress;
            this.deliveryMethod = deliveryMethod;
            this.orderItems = orderItems;
            this.subTotal = subTotal;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus orderStatus { get; set; } = OrderStatus.Pending;
        public ShippingAddress shippingAddress { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }
        public ICollection<OrderItem> orderItems { get; set; } = new HashSet<OrderItem>();
        public decimal subTotal { get; set; }

        public decimal GetTotal()
            => subTotal + deliveryMethod.Cost;

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
