namespace TzuChiClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CategoryModels
    {
        [Key]
        public string CategoryID { get; set; }

        public string CategoryTypeID { get; set; }

        public string CategoryName { get; set; }

        public int Sort { get; set; }

        public DateTime CreateTime { get; set; }

        public string Creator { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Updater { get; set; }
    }
}
