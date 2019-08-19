using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResponsiveFileManagerMVC.Models
{
    public class FileModel
    {
        [Required(ErrorMessage = "請選擇檔案後上傳")]
        [Display(Name = "檔案名稱")]
        public HttpPostedFileBase[] files { get; set; }

        [Display(Name = "檔案路徑")]
        public string filePath { get; set; }

        [Display(Name = "檔案名稱")]
        public string fileName { get; set; }

        public string DirectoryPath { get; set; }
    }
}