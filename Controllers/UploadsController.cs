using File_Sharing_proj004.Data;
using File_Sharing_proj004.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace File_Sharing_proj004.Controllers
{
    [Authorize]
    public class UploadsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment env;

        public UploadsController(ApplicationDbContext context,IWebHostEnvironment env )
        {
            this.context = context;
            this.env = env;
        }
        public string UserId { 
            get
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            } 
        }
        public IActionResult Index()
        {
            var results = context.Uploads.Where(u => u.UserId == UserId).Select(u=>new UploadsViewModel { 
            Name=u.OriginalFileName,
            ContentType=u.ContentType,
            Size=u.Size,
            UploadDate=u.UploadDate ,
            PathStr=u.FileName,
            Id=u.Id
            });
            return View(results);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Results(string Term)
        {
            var results = context.Uploads.Where(u => u.OriginalFileName.Contains(Term)).Select(u => new UploadsViewModel
            {
                Name = u.OriginalFileName,
                ContentType = u.ContentType,
                Size = u.Size,
                UploadDate = u.UploadDate,
                PathStr = u.FileName
            });

            return View(results);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(InputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var NewName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(model.File.FileName);
                var FileName = string.Concat(NewName, extension);
                var root = env.WebRootPath;
                var path = Path.Combine(root, "uploads", FileName);

                using ( var files =System.IO.File.Create(path))
                {
                await model.File.CopyToAsync(files);
                }
                await context.Uploads.AddAsync(new Uploads
                {
                    OriginalFileName =model.File.FileName,
                    FileName = FileName,
                    ContentType = model.File.ContentType,
                    Size = model.File.Length,
                    UserId = UserId
                }) ;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            var Result = await context.Uploads.FindAsync(Id);
            if (Result == null)
            {
                return NotFound();
            }
            if (Result.UserId != UserId)
            {
                return NotFound();
            }
            var resNM = new UploadsViewModel
            {
                Id = Result.Id,
                Name = Result.OriginalFileName,
                ContentType = Result.ContentType,
                UploadDate = Result.UploadDate,
                Size = Result.Size,
                PathStr = Result.FileName,
                UsId=Result.UserId
            };
            return View(resNM);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Uploads model)
        {
            if (ModelState.IsValid)
            {
                var Result = await context.Uploads.FindAsync(model.Id);

                if (Result.UserId != UserId)
                {
                    return NotFound();
                }
                context.Uploads.Remove(Result);
                await context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View();
        }
    }
}
