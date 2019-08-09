namespace TzuChiClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [StringLength(36)]
        public string CategoryID { get; set; }

        [StringLength(10)]
        public string CategoryTypeID { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        public int? Sort { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(36)]
        public string Creator { get; set; }

        public DateTime? UpdateTime { get; set; }

        [StringLength(36)]
        public string Updater { get; set; }
    }
}
