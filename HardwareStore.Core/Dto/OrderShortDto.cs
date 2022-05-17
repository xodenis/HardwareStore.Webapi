using System.Collections.Generic;

namespace HardwareStore.Core.Dto
{
    public class OrderShortDto
    {
        public string PaymentMethod { get; set; }

        public string DeliveryType { get; set; }

        public List<OrdersProductsShortDto> Products { get; set; }
    }
}
