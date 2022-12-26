﻿using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace ERP.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]

    public class WebCamController : Controller
    {
        private readonly IHostingEnvironment _env;


        public WebCamController(IHostingEnvironment hostingEnvironment)
        {
            _env = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult CaptureImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CaptureImage(string name)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var fileNameToStore = string.Concat(Convert.ToString(Guid.NewGuid()), Path.GetExtension(fileName));
                            //  Path to store the snapshot in local folder
                            var filepath = Path.Combine(_env.WebRootPath, "VisitorScanData") + $@"\{fileNameToStore}";

                            // Save image file in local folder
                            if (!string.IsNullOrEmpty(filepath))
                            {
                                using (FileStream fileStream = System.IO.File.Create(filepath))
                                {
                                    file.CopyTo(fileStream);
                                    fileStream.Flush();
                                }
                            }

                            //// Save image file in database
                            //var imgBytes = System.IO.File.ReadAllBytes(filepath);
                            //if (imgBytes != null)
                            //{
                            //	if (imgBytes != null)
                            //	{
                            //		string base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                            //		string imageUrl = string.Concat("data:image/jpg;base64,", base64String);

                            //		// Code to store into database
                            //		// save filename and image url(base 64 string) to the database
                            //	}
                            //}
                        }
                    }
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
