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
        //public UserServices(){ }



        //public int user_id(string email)
        //{

        //    try
        //    {
        //        var id = conection.users.Single(data => data.Email == email);
        //        return id.Id;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }

        //}

        //public IEnumerable<Users> GetAll()
        //{
        //    var getall = conection.users.Where(data => data.Id > 0).ToList();
        //    return getall;
        //}

        public UserData Get(string username)
        {
            try
            {
                return _conString.UserData.AsNoTracking().Single(data => data.Username == username);
            }
            catch
            {
                return null;
            }
        }
        //public Users Get(string email)
        //{
        //    try
        //    {
        //        return conection.users.AsNoTracking().Single(data => data.Email == email);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}


        public void Create(UserData user)
        {
            _conString.UserData.Add(user);
            _conString.SaveChanges();
        }

        public void Update(UserData user)
        {
            _conString.Update(user);
            _conString.SaveChanges();
        }

        //public void Delete(int id)
        //{
        //    Users users = conection.users.Find(id);
        //    if (users != null)
        //        conection.users.Remove(users);

        //    conection.SaveChanges();
        //}

        //public void Delete(string email)
        //{
        //    try
        //    {
        //        Users users = conection.users.Single(data => data.Email == email);
        //        conection.users.Remove(users);
        //        conection.SaveChanges();
        //    }
        //    catch
        //    {

        //    }

        //}
    }
}