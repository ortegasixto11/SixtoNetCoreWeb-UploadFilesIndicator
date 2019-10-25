using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SixtoUploadFilesProgress.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _env;

        public UploadController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            string fileNameWithExt = "";
            var filePath = $"{_env.WebRootPath}\\Attached\\";
            var DirInfo = new System.IO.DirectoryInfo(filePath);
            if (DirInfo.Exists == false) DirInfo.Create();

            if (files != null && files.Count > 0)
            {
                foreach (var formFile in files)
                {
                    //var formFile = files[0];

                    if (formFile != null && formFile.Length > 0)
                    {
                        System.IO.FileInfo myFile = new System.IO.FileInfo(formFile.FileName);
                        fileNameWithExt = $"{DateTime.Now.ToString("MMddyyyy")}_{DateTime.Now.ToString("HHmmss")}_{myFile.Name}";
                        filePath += fileNameWithExt;
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}