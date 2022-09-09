using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing_proj004.Models
{
    public class InputViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }

    public class UploadsViewModel
    {
        public string PathStr { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public decimal Size { get; set; }

        [Required] 
        public DateTime UploadDate { get; set; }
        public string Id { get;  set; }
        public string UsId { get;  set; }
    }
}
