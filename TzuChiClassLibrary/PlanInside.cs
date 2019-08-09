namespace TzuChiClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlanInside")]
    public partial class PlanInside
    {
        [Key]
        [StringLength(36)]
        public string ContentID { get; set; }

        [StringLength(36)]
        public string CategoryPrepID { get; set; }

        [StringLength(36)]
        public string CategoryYearID { get; set; }

        [StringLength(36)]
        public string CategoryPartitionID { get; set; }

        [StringLength(36)]
        public string CategorySiteID { get; set; }

        [StringLength(50)]
        public string Department { get; set; }

        [StringLength(36)]
        public string ImageXY { get; set; }

        public DateTime? EndTime { get; set; }

        [StringLength(250)]
        public string Agencies { get; set; }

        public virtual Content Content { get; set; }
    }
}
