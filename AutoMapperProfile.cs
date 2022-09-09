using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing_proj004
{
    public class UploadProfile : Profile
    {
        public UploadProfile()
        {
            CreateMap<Models.InputUpload, Data.Uploads>().
                ForMember(u => u.Id, u => u.Ignore()).
                ForMember(u => u.UploadDate, u => u.Ignore());
            CreateMap<Data.Uploads, Models.UploadsViewModel>();
            
        }
        
    }
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Data.ApplicationUser, Models.UserViewModel>();

        }
    }
}
