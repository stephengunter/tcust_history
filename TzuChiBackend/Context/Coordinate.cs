namespace TzuChiBackend.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coordinate")]
    public partial class Coordinate
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(36)]
        public string PointID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        public string TypeID { get; set; }

        [Key]
        [Column(Order = 2)]
        public double PointX { get; set; }

        [Key]
        [Column(Order = 3)]
        public double PointY { get; set; }
    }
}
