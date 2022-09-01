using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Models
{
    public class FileModel
    {
        public IFormFile file { get; set; }
        public FileStream mydata { get; set; }
    }
}
