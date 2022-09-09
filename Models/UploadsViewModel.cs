using System;
using System.ComponentModel.DataAnnotations;

namespace File_Sharing_proj004.Models
{
    public class UploadsViewModel
    {

        [Display(Name = "File Path")]
        public string FileName { get; set; }

        [Required]

        [Display(Name = "File name")]
        public string OriginalFileName { get; set; }

        [Required]
        [Display(Name = "File type")]
        public string ContentType { get; set; }

        [Required]
        [Display(Name = "file size")]
        public decimal Size { get; set; }

        [Required]
        [Display(Name = "Upload time")]
        public DateTime UploadDate { get; set; }

        [Display(Name = "item Id")]
        public string Id { get;  set; }

        [Display(Name = "Created user id")]
        public string UserId { get;  set; }
        [Display(Name="Download times")]
        public long DownloadCount { get;  set; }
    }
}
