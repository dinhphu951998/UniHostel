namespace UniHostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BillCompulsoryServiceDetail
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string BillID { get; set; }

        [Required]
        [StringLength(255)]
        public string CompulsoryServiceID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Time { get; set; }

        public int PreNum { get; set; }

        public int CurNum { get; set; }

        public float Amount { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual CompulsoryService CompulsoryService { get; set; }
    }
}
