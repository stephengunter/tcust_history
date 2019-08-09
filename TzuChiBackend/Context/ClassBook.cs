namespace TzuChiBackend.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassBook")]
    public partial class ClassBook
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(36)]
        public string ClassBookID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string AcademicYear { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Department { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string Class { get; set; }

        [Key]
        [Column(Order = 4)]
        public string StudentId { get; set; }

        [Key]
        [Column(Order = 5)]
        public string StudentName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(200)]
        public string PictureUrl { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(5)]
        public string Page { get; set; }

        public bool? IsTurnLeft { get; set; }
    }
}
