using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing_proj004.Data
{
    public class Contact
    {

        public Contact()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        [Required]

        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
