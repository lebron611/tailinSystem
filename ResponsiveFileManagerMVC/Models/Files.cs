namespace ResponsiveFileManagerMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Files
    {
        [Key]
        public int FileId { get; set; }

        [Required]
        public string FileName { get; set; }

        public DateTime ModifiedTime { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }
    }
}
