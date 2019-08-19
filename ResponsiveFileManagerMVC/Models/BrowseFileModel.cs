using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ResponsiveFileManagerMVC.Models
{
    public class BrowseFileModel
    {
        public DirectoryInfo DirectoryInfo { get; set; }
        public IOrderedEnumerable<FileSystemInfo> FileInfo { get; set; }
    }
}