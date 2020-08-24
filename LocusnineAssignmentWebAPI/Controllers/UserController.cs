using LocusnineAssignmentWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace LocusnineAssignmentWebAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public List<User> GetUsersList()
        {
            List<User> userList = new List<User>();
            try
            {
                UserDBContext userDBContext = new UserDBContext();
                userList = userDBContext.Users.ToList();
            }
            catch(Exception ex)
            {

            }

            return userList;
        }

        [HttpPost]
        public bool SaveUser(User user)
        {
            bool status = true;
            try
            {
                UserDBContext userDBContext = new UserDBContext();
                if (user.Id > 0)
                {
                    if (userDBContext.Users.Where(obj => obj.Id == user.Id).Any())
                    {
                        userDBContext.Entry(user).State = EntityState.Modified;
                        userDBContext.SaveChanges();
                    }
                    else
                        status = false;
                }
                else
                {
                    userDBContext.Users.Add(user);
                    userDBContext.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                status = false;
            }
            
            return status;
        }

        [HttpDelete]
        public bool DeleteUser(int userId)
        {
            bool status = true;
            try
            {
                UserDBContext userDBContext = new UserDBContext();
                if (userId > 0)
                {
                    User user = userDBContext.Users.Where(obj => obj.Id == userId).FirstOrDefault();
                    if (user != null)
                    {
                        userDBContext.Users.Remove(user);
                        userDBContext.SaveChanges();
                    }
                    else
                        status = false;
                }
                else
                {
                    status = false; 
                }
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }
    }
}
