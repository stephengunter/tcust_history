namespace TzuChiClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Directors
    {
        [Key]
        [StringLength(20)]
        public string SessionNumber { get; set; }

        [StringLength(10)]
        public string StartYear { get; set; }

        [StringLength(10)]
        public string EndYear { get; set; }

        public string Names { get; set; }
    }
}
