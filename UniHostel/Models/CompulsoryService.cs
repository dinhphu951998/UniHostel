namespace UniHostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CompulsoryService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompulsoryService()
        {
            BillCompulsoryServiceDetails = new HashSet<BillCompulsoryServiceDetail>();
        }

        [StringLength(255)]
        public string ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public float Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public bool? isActive { get; set; }

        [Required]
        [StringLength(255)]
        public string HostID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillCompulsoryServiceDetail> BillCompulsoryServiceDetails { get; set; }

        public virtual Host Host { get; set; }
    }
}
