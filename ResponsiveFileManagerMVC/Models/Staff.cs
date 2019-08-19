namespace ResponsiveFileManagerMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        public int Id { get; set; }

        [Required]
        public string StaffId { get; set; }

        [Required]
        public string StaffName { get; set; }
    }
}
