namespace UniHostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [StringLength(255)]
        public string ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public bool? isActive { get; set; }

        public int RoleID { get; set; }

        public virtual Host Host { get; set; }

        public virtual Renter Renter { get; set; }

        public virtual Role Role { get; set; }
    }
}
