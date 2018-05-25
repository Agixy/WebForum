using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebForum.Repositories;
using CommonLibrary.Models;

//using System.Text.RegularExpressions;

namespace WebForum.Controllers
{
    public class GroupController : ApiController
    {
        public IEnumerable<Group> GetAllGroups()
        {
            using (var context = new Context())
            {
                return context.Groups.ToList();
            }
        }

        [HttpPost()]
        public async Task<Group> AddNewGroup(Group group)
        {
            using (var context = new Context())
            {
                context.Groups.Add(group);
                await context.SaveChangesAsync();
            }

            return group;

        }

        public async Task DeleteGroup(int id)
        {
            using (var context = new Context())
            {
                var group = context.Groups.FirstOrDefault(u => u.Id == id);

                context.Groups.Remove(group);
                await context.SaveChangesAsync();
            }
        }
    }
}