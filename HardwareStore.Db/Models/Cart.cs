﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Db.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
