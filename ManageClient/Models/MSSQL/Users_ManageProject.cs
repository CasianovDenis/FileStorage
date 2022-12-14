using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Models
{
    public class Users_ManageProject
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        
        public string Password_verification { get; set; }

    }
}
