using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hostel1.Models
{
    public class Rooms
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 100)]
        public float Square { get; set; }

        [Range(0, 30_000_000)]
        [DataType(DataType.Currency)]
        public float Price { get; set; }


        public string Description { get; set; }

        public bool IsAvailable { get; set; }

        public string Image { get; set; }
    }

   
}