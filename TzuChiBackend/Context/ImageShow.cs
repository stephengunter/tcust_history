namespace TzuChiBackend.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImageShow")]
    public partial class ImageShow
    {
        [Required]
        [StringLength(36)]
        public string TypeID { get; set; }

        [StringLength(36)]
        public string ImageShowID { get; set; }

        [StringLength(36)]
        public string ContentID { get; set; }

        [StringLength(200)]
        public string ImageName { get; set; }

        [StringLength(200)]
        public string ImageUrl { get; set; }

        public int? ImageWidth { get; set; }

        public int? ImageHeight { get; set; }

        [StringLength(36)]
        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(36)]
        public string Updater { get; set; }

        public DateTime? UpdateTime { get; set; }

        public virtual Content Content { get; set; }
    }
}
