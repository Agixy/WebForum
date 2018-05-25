using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebForum.Repositories;

namespace WebForum.Controllers
{
    public class UserController : ApiController
    {

        public IEnumerable<User> GetAllUsers()
        {

            //return new[]
            //{
            //    new User
            //    {
            //        Name = "Adam",
            //        Id = 0,
            //        Email = "adam@gmail.com"
            //    }
            //};

            using (var context = new Context())
            {
                return context.Users.ToList();
            }

        }


        [HttpPost()]
        public async Task<User> RegisterUser(User user)
        {
            using (var context = new Context())
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            return user;

        }

        //public User GetUserById(int id)
        //{

        //}
    }
}