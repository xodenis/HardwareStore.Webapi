using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Db.Models
{
    public class Order
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public double Price { get; set; }

        public Status Status { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<OrdersProducts> Products { get; set; }

    }

    public enum Status
    {
        InDelivery,
        PendingInStore,
        Completed
    }
}
