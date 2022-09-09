using AutoMapper;
using AutoMapper.QueryableExtensions;
using File_Sharing_proj004.Data;
using File_Sharing_proj004.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing_proj004.Services
{
    public class UploadService : IUploadService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UploadService(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task Create(InputUpload model)
        {
            var res = mapper.Map<Uploads>(model);   
            await context.Uploads.AddAsync(res
                //new Uploads
            //{
            //    OriginalFileName = model.OriginalFileName,
            //    FileName = model.FileName,
            //    ContentType = model.ContentType,
            //    Size = model.Size,
            //    UserId = model.UserId
            //}
            );
            await context.SaveChangesAsync();
        }

        public async Task Delete(Uploads model, string UserId)
        {
            //var Result =await context.Uploads.FindAsync(model.Id);

            context.Uploads.Remove(model);
            await context.SaveChangesAsync();
        }

        public async Task<UploadsViewModel> Find(string Id,string UserId)
        {
            var Result = await context.Uploads.FindAsync(Id);
            if (Result.UserId != UserId)
            {
                return null;
            }
            if (Result != null)
            {
                return mapper.Map<UploadsViewModel>(Result);
                //return new UploadsViewModel
                //{
                //    Id = Result.Id,
                //    OriginalFileName = Result.OriginalFileName,
                //    ContentType = Result.ContentType,
                //    UploadDate = Result.UploadDate,
                //    Size = Result.Size,
                //    FileName = Result.FileName,
                //    UserId = Result.UserId
                //};
            }
            return null;
        }
        public IQueryable<UploadsViewModel> GetAllBy(string UserId)
        {
            var result = context.Uploads.Where(u => u.UserId == UserId).OrderByDescending(u => u.UploadDate).ProjectTo<UploadsViewModel>(mapper.ConfigurationProvider);
            //    Select(u => new UploadsViewModel
            //{
            //    OriginalFileName = u.OriginalFileName,
            //    ContentType = u.ContentType,
            //    Size = u.Size,
            //    UploadDate = u.UploadDate,
            //    FileName = u.FileName,
            //    Id = u.Id,
            //    DownloadCount = u.DownloadCount
            //});
            return result;
        }
        public IQueryable<UploadsViewModel> GetAll()
        {
            var result = context.Uploads.OrderByDescending(u => u.DownloadCount).ProjectTo<UploadsViewModel>(mapper.ConfigurationProvider);
            //    Select(u => new UploadsViewModel
            //{
            //    OriginalFileName = u.OriginalFileName,
            //    ContentType = u.ContentType,
            //    Size = u.Size,
            //    UploadDate = u.UploadDate,
            //    FileName = u.FileName,
            //    DownloadCount = u.DownloadCount,
            //    Id=u.Id
            //});
            return result;
        }

        public IQueryable<UploadsViewModel> Search(string term)
        {
            var result = context.Uploads.Where(u => u.OriginalFileName.Contains(term)).ProjectTo<UploadsViewModel>(mapper.ConfigurationProvider);
            //    Select(u => new UploadsViewModel
            //{
            //    OriginalFileName = u.OriginalFileName,
            //    ContentType = u.ContentType,
            //    Size = u.Size,
            //    UploadDate = u.UploadDate,
            //    FileName = u.FileName
            //});
            return result;
        }

        public async Task<UploadsViewModel> Find(string Id)
        {
            var Result = await context.Uploads.FindAsync(Id);
           
            if (Result != null)
            {
                return mapper.Map<UploadsViewModel>(Result);
                //return new UploadsViewModel
                //{
                //    Id = Result.Id,
                //    OriginalFileName = Result.OriginalFileName,
                //    ContentType = Result.ContentType,
                //    UploadDate = Result.UploadDate,
                //    Size = Result.Size,
                //    FileName = Result.FileName,
                //    UserId = Result.UserId
                //};
            }
            return null;
        }
        public async Task IncrementDownloadCount(string id)
        {
            var selectedFile = await context.Uploads.FindAsync(id);
            selectedFile.DownloadCount++;
            context.Uploads.Update(selectedFile);
            await context.SaveChangesAsync();
        }
        public async Task<int> GetUploadsCount()
        {
            return await context.Uploads.CountAsync();
        }
    }
}
