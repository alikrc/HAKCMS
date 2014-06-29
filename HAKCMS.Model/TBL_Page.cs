namespace HAKCMS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_Page
    {
        [Key]
        public int PageID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(2000)]
        public string Url { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Content { get; set; }
    }
}
