namespace TzuChiClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlanOutside")]
    public partial class PlanOutside
    {
        [Key]
        [StringLength(36)]
        public string ContentID { get; set; }

        [StringLength(36)]
        public string CategoryOutsideID { get; set; }

        [StringLength(36)]
        public string CategoryCountryID { get; set; }

        [StringLength(36)]
        public string CategoryDepartmentID { get; set; }

        [StringLength(50)]
        public string EnglishName { get; set; }

        public DateTime? EndTime { get; set; }

        public string IntroCh { get; set; }

        public string IntroEn { get; set; }

        [StringLength(36)]
        public string ImageXY { get; set; }

        public virtual Content Content { get; set; }
    }
}
