namespace ResponsiveFileManagerMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActionLog")]
    public partial class ActionLog
    {
        [Key]
        public long LogId { get; set; }

        [Required]
        [StringLength(10)]
        public string Operator { get; set; }

        [StringLength(300)]
        public string Refer { get; set; }

        [Required]
        [StringLength(300)]
        public string Destination { get; set; }

        [StringLength(5)]
        public string Method { get; set; }

        public bool MobleDevices { get; set; }

        [StringLength(40)]
        public string IPAddress { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime RequestTime { get; set; }
    }
}
