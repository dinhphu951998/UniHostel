namespace UniHostel.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            Renters = new HashSet<Renter>();
        }

        [StringLength(255)]
        [DisplayName("Room ID")]
        public string ID { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Room Name")]
        public string Name { get; set; }

        public float Square { get; set; }

        public float Price { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public bool? isAvailable { get; set; }

        [Required]
        [StringLength(255)]
        public string HostID { get; set; }

        public virtual Host Host { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Renter> Renters { get; set; }
    }
}
