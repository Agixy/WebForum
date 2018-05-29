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
                var usersDto = context.Users.ToList();
                List<User> users = new List<User>();

                foreach (var user in usersDto)
                {
                    users.Add(MapToUser(user));
                }

                return users;
            }
        }

        private User MapToUser(UserDto dto)
        {
            return new User { Name = dto.Name, Id = dto.Id, Email = dto.Email };
        }

        private UserDto MapToUserDto(User user)
        {
            return new UserDto { Name = user.Name, Id = user.Id, Email = user.Email };
        }

        [HttpPost()]
        public async Task<User> RegisterUser(User user)
        {
            using (var context = new Context())
            {
                context.Users.Add(MapToUserDto(user));
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

        [HttpPost]
        [Route("api/user/{userid}/joingroup/{groupid}")]
        public async Task JoinTheGroup(int userId, int groupId)
        {
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);
                var group = context.Groups.FirstOrDefault(g => g.Id == groupId);
            
                user.Groups.Add(group);

                await context.SaveChangesAsync();
            }
        }

        [HttpPost]
        [Route("api/user/{userid}/leavegroup/{groupid}")]
        public async Task LeaveTheGroup(int userId, int groupId)
        {
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);
                var group = context.Groups.FirstOrDefault(g => g.Id == groupId);

                user.Groups.Remove(group);

                await context.SaveChangesAsync();
            }
        }

        //private User GetUserById(int id)      // czy wyciagac to i tutaj przekazywac context?
        //{

        //}
    }
}