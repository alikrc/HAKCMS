namespace HAKCMS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_Media
    {
        [Key]
        public int MediaID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(3000)]
        public string Path { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
