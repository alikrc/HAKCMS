namespace HAKCMS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_Gallery
    {
        [Key]
        public int GalleryID { get; set; }

        [StringLength(150)]
        public string Name { get; set; }
    }
}
