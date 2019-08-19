using ExcelDataReader;
using ResponsiveFileManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResponsiveFileManagerMVC.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index(string newLink, string searchName)
        {
            BrowseFileModel model = new BrowseFileModel();
            var path = "";
            var virtualPath = "";
            if (newLink == null)
            {
                path = Server.MapPath("~/生產資訊/");
            }
            else
            {
                var startIndex = newLink.IndexOf("生");
                var leng = newLink.Length;
                var finalIndex = newLink.Length - 1;
                virtualPath = newLink.Substring(startIndex);
                newLink = "~/" + newLink.Substring(startIndex);
                path = Server.MapPath(newLink);
            }
            var dir = new DirectoryInfo(path);
            ViewBag.fileName = dir.Name;
            ViewBag.parentName = dir.Parent;
            var attribute = dir.Attributes;
            if (attribute.ToString() == "Directory")
            {
                var files = dir.EnumerateFileSystemInfos().Select(f => f).OrderBy(f => f.Attributes);
                if (searchName != null)
                {
                    files = dir.EnumerateFileSystemInfos().Select(f => f).Where(f => f.Name.ToUpper().Contains(searchName.ToUpper())).OrderBy(f => f.Attributes);
                }
                model.DirectoryInfo = dir;
                model.FileInfo = files;
                return View(model);
            }
            else if (dir.Extension.ToString() == ".jpg" || dir.Extension.ToString() == ".png")
            {
                return RedirectToAction("Preview", "Home", new { path = newLink });
            }
            else
            {
                byte[] fileByte = System.IO.File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + virtualPath);
                return File(fileByte, System.Net.Mime.MediaTypeNames.Application.Octet, dir.Name);
            }
        }

        [Authorize(Roles ="admin")]
        public ActionResult DeleteFile(string name,string path,string folderName,string submit)
        {
            if (!User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Home", null);
            }
            if (submit != null)
            {
                var startIndex = path.IndexOf("生");
                var deletePath = "~/" + path.Substring(startIndex).Replace('\\', '/');
                if (System.IO.File.Exists(Request.MapPath(deletePath)) || System.IO.Directory.Exists(Request.MapPath(deletePath)))
                {
                    var dir = new DirectoryInfo(deletePath);
                    var attribute = dir.Extension;
                    if (attribute.ToString().Length == 0)
                    {
                        string mapPath = Server.MapPath(deletePath);
                        Directory.Delete(mapPath, true);
                    }
                    else
                    {
                        System.IO.File.Delete(Request.MapPath(deletePath));
                    }
                }
                return RedirectToAction("ViewFiles","Home", new {newLink = folderName });
            }
                var startIndex2 = path.IndexOf("生");
                FileModel model = new FileModel();
                model.fileName = name;
                model.filePath = path.Substring(startIndex2);
                model.DirectoryPath = folderName;
                return View(model);
        }

        [Authorize]
        public ActionResult UploadFile(string uploadLink,string fileName)
        {
            FileModel model = new FileModel();
            model.filePath = uploadLink;
            model.fileName = fileName;
            return View(model);
        }

        [Authorize]
        [HttpPost]  
        public ActionResult UploadFile(HttpPostedFileBase[] files,string uploadLink,string fileName)  
        {
            var path="";
            var newLink="";
            if (uploadLink == null)
            {
                path = Server.MapPath("~/生產資訊/");
            }
            else
            {
                var startIndex = uploadLink.IndexOf("生");
                newLink = "~/" + uploadLink.Substring(startIndex);
                path = Server.MapPath(newLink);
            }
            FileModel model = new FileModel
            {
                filePath = newLink,
                fileName = fileName
            };
            //Ensure model state is valid  
            if (ModelState.IsValid)  
            {   //iterating through multiple file collection   
                if (files.Length == 0)
                {
                    return View();
                }
                else
                {
                    foreach (HttpPostedFileBase file in files)
                    {

                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(path + "//" + InputFileName);
                            //Save file to server folder  
                            if (System.IO.File.Exists(ServerSavePath) && !User.IsInRole("admin"))
                            {
                                ViewBag.UploadStatus = "exist";
                                return View(model);
                            }
                            file.SaveAs(ServerSavePath);
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = "success";
                        }
                    }

                }
            }  
            return View(model);  
        } 

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Preview(string path)
        {
            PreviewImage image = new PreviewImage
            {
                ImagePath = path.Replace('\\', '/')
            };
            return View(image);
        }

        [Authorize(Roles = "admin")]
        public ActionResult IFrame()
        {
            return View();
        }

        [Authorize]
        public ActionResult ViewFiles(string newLink, string searchName,string folderName)
        {
            BrowseFileModel model = new BrowseFileModel();
            var path = "";
            var virtualPath = "";
            if (newLink == null) {
                path = Server.MapPath("~/生產資訊/");
            }
            else
            {
                var startIndex = newLink.IndexOf("生");
                var leng = newLink.Length;
                var finalIndex = newLink.Length - 1;
                virtualPath = newLink.Substring(startIndex);
                newLink = "~/" + newLink.Substring(startIndex);
                path = Server.MapPath(newLink);
            }
            var dir = new DirectoryInfo(path);
            ViewBag.fileName = dir.Name;
            ViewBag.parentName = dir.Parent;
            var attribute = dir.Attributes;
            if (folderName != null && folderName.Length > 0)
            {
                var newFolder = Server.MapPath(newLink+"\\"+folderName);
                if (!Directory.Exists(newFolder))
                {
                    Directory.CreateDirectory(newFolder);
                }
                else
                {
                    ViewBag.duplicateFolder = true;
                }
            }
            else if (folderName != null && folderName.Length == 0)
            {
                ViewBag.needFolderName = true;
            }
            if (attribute.ToString() == "Directory")
            {
                var files = dir.EnumerateFileSystemInfos().Select(f => f).OrderBy(f => f.Attributes);
                if (searchName != null && searchName.Length > 0)
                {
                    files = dir.EnumerateFileSystemInfos().Select(f => f).Where(f => f.Name.ToUpper().Contains(searchName.ToUpper())).OrderBy(f => f.Attributes);
                    ViewBag.isSearch = true;
                }
                else
                {
                    ViewBag.isSearch = false;
                }
                model.DirectoryInfo = dir;
                model.FileInfo = files;
                return View(model);
            }
            else if (dir.Extension.ToString() == ".pdf")
            {
                return File(newLink, "application.pdf");
            }
            else 
            {
                byte[] fileByte = System.IO.File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + virtualPath);
                return File(fileByte,System.Net.Mime.MediaTypeNames.Application.Octet,dir.Name);
            }
        }

        public ActionResult ReadExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReadExcel(HttpPostedFileBase upload)
       {
           if (ModelState.IsValid)
           {
               if (upload != null && upload.ContentLength > 0)
               {
                   // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                   // to get started. This is how we avoid dependencies on ACE or Interop:
                   Stream stream = upload.InputStream;
 
                   // We return the interface, so that
                   IExcelDataReader reader = null;
 
 
                   if (upload.FileName.EndsWith(".xls"))
                   {
                       reader = ExcelReaderFactory.CreateBinaryReader(stream);
                   }
                   else if (upload.FileName.EndsWith(".xlsx"))
                   {
                       reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                   }
                   else
                   {
                       ModelState.AddModelError("File", "This file format is not supported");
                       return View();
                   }
 
                    
                   DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                   {
                       ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                       {
                           UseHeaderRow = true
                       }
                   });
                    reader.Close();
 
                   return View(result.Tables[0]);
               }
               else
               {
                   ModelState.AddModelError("File", "Please Upload Your file");
               }
           }
           return View();
       }
    }
}