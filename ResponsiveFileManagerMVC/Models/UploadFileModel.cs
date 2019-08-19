using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResponsiveFileManagerMVC.Models
{
    public class UploadFileModel
    {
        public class FileModel
        {
            [Required(ErrorMessage = "Please select file.")]
            [Display(Name = "Browse File")]
            public HttpPostedFileBase[] Files { get; set; }

        }
    }
}