namespace TzuChiBackend.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Content()
        {
            FileUplaods = new HashSet<FileUplaod>();
            ImageShows = new HashSet<ImageShow>();
        }

        [StringLength(36)]
        public string ContentID { get; set; }

        [Required]
        [StringLength(36)]
        public string TypeID { get; set; }

        [StringLength(100)]
        public string SerialNo { get; set; }

        public DateTime? ContentTime { get; set; }

        [StringLength(50)]
        public string ContentName { get; set; }

        public string Description { get; set; }

        public bool? IsPublish { get; set; }

        public bool? ShowDate { get; set; }

        public DateTime? OpenTime { get; set; }

        public DateTime? CloseTime { get; set; }

        [StringLength(100)]
        public string RelatedLink { get; set; }

        [StringLength(36)]
        public string ContentCreator { get; set; }

        public DateTime? ContentCreateTime { get; set; }

        [StringLength(36)]
        public string ContentUpdater { get; set; }

        public DateTime? ContentUpdateTime { get; set; }

        public string ContentText { get; set; }

        [StringLength(256)]
        public string Author { get; set; }

        public int DisplayOrder { get; set; }

        public virtual ContentType ContentType { get; set; }

        public virtual Educated Educated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileUplaod> FileUplaods { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImageShow> ImageShows { get; set; }

        public virtual PlanInside PlanInside { get; set; }

        public virtual PlanOutside PlanOutside { get; set; }



        
    }
}
