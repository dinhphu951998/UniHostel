namespace UniHostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BillAdvancedServiceDetail
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string AdvancedServiceID { get; set; }

        [Required]
        [StringLength(255)]
        public string BillID { get; set; }

        public DateTime? Time { get; set; }

        public int Quantity { get; set; }

        public int Amount { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public virtual AdvancedService AdvancedService { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
