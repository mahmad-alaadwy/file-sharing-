using File_Sharing_proj004.Data;
using File_Sharing_proj004.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing_proj004.Services
{
    public interface IUploadService
    {
        IQueryable<UploadsViewModel> GetAll();
        IQueryable<UploadsViewModel> Search(string term);
        Task Create(InputUpload model);
        Task<UploadsViewModel> Find(string Id);
        Task<UploadsViewModel> Find(string Id, string UserId);
        Task Delete(Uploads model, string UserId);
        public IQueryable<UploadsViewModel> GetAllBy(string UserId);
        Task IncrementDownloadCount(string id);
        Task<int> GetUploadsCount();


    }
}
