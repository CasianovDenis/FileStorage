using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Models.MSSQL
{
    public class TempData
    {
       
        public string OldPassword { get; set; }

        public string NewUsername { get; set; }
        public string NewEmail { get; set; }
        public string NewPassword { get; set; }

        public string Verification_pass { get; set; }
    }
}
