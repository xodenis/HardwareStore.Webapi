using HardwareStore.Db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Core.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public double Price { get; set; }

        public Status Status { get; set; }

        public int UserId { get; set; }

        public List<OrdersProductsDto> Products { get; set; }
    }
}
