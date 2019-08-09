namespace TzuChiBackend.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileUplaod")]
    public partial class FileUplaod
    {
        [Key]
        [StringLength(36)]
        public string ItemOID { get; set; }

        [StringLength(36)]
        public string ContentID { get; set; }

        [StringLength(50)]
        public string FunctionOID { get; set; }

        [StringLength(10)]
        public string FunctionType { get; set; }

        [StringLength(50)]
        public string ItemName { get; set; }

        public string ItemInfo { get; set; }

        [StringLength(200)]
        public string FileName { get; set; }

        [StringLength(200)]
        public string Path { get; set; }

        [StringLength(10)]
        public string Bit { get; set; }

        public int? Sort { get; set; }

        public int? ImageWidth { get; set; }

        public int? ImageHeight { get; set; }

        [StringLength(36)]
        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }

        public long? ClickCount { get; set; }

        public bool CoverImage { get; set; }

        [StringLength(200)]
        public string PreviewPath { get; set; }

        public virtual Content Content { get; set; }
    }
}
