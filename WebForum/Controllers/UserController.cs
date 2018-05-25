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

        public async Task DeleteUser(int id)
        {
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == id);

                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        //private User GetUserById(int id)      // czy wyciagac to i tutaj przekazywac context?
        //{
            
        //}
    }
}