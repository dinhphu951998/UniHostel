namespace UniHostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            BillAdvancedServiceDetails = new HashSet<BillAdvancedServiceDetail>();
            BillCompulsoryServiceDetails = new HashSet<BillCompulsoryServiceDetail>();
        }

        [StringLength(255)]
        public string ID { get; set; }

        public DateTime Time { get; set; }

        public float? OtherFee { get; set; }

        public float Total { get; set; }

        public bool? isPaid { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string RoomID { get; set; }

        [Required]
        [StringLength(255)]
        public string RenterID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillAdvancedServiceDetail> BillAdvancedServiceDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillCompulsoryServiceDetail> BillCompulsoryServiceDetails { get; set; }

        public virtual Room Room { get; set; }

        public virtual Renter Renter { get; set; }
    }
}
