namespace TzuChiClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Educated")]
    public partial class Educated
    {
        [Key]
        [StringLength(36)]
        public string ContentID { get; set; }

        [StringLength(36)]
        public string CategoryYearID { get; set; }

        [StringLength(36)]
        public string CategoryTermID { get; set; }

        [StringLength(36)]
        public string CategoryEducatedID { get; set; }

        [StringLength(36)]
        public string CategoryDoingID { get; set; }

        public virtual Content Content { get; set; }
    }
}
