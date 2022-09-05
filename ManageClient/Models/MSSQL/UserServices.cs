using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Models
{
    public class UserServices
    {


        private readonly ConString _conString;

        public UserServices(ConString conection)
        {
            _conString = conection;

        }
        

        public Users_ManageProject Get(string username)
        {
            try
            {
                return _conString.Users_ManageProject.AsNoTracking().Single(data => data.Username == username);
            }
            catch
            {
                return null;
            }
        }
        


        public void Create(Users_ManageProject user)
        {
            _conString.Users_ManageProject.Add(user);
            _conString.SaveChanges();
        }

        public void Update(Users_ManageProject user)
        {
            _conString.Update(user);
            _conString.SaveChanges();
        }

    }
}