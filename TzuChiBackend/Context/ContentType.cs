namespace TzuChiBackend.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentType")]
    public partial class ContentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContentType()
        {
            Contents = new HashSet<Content>();
        }

        [Key]
        [Column("ContentType")]
        [StringLength(36)]
        public string Id { get; set; }

        [StringLength(50)]
        public string TypeName { get; set; }

        [StringLength(100)]
        public string BackendLink { get; set; }

        [StringLength(100)]
        public string FrontendLink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Content> Contents { get; set; }
    }
}
