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
        [HttpGet()]
        public IEnumerable<Group> GetAllGroups()
        {
            using (var context = new Context())
            {
                var groupsDto = context.Groups.ToList();
                List<Group> groups = new List<Group>();

                foreach (var group in groupsDto)
                {
                    groups.Add(FFMapToGroup(group));
                }

                return groups;
            }
        }

        private Group FFMapToGroup(GroupDto dto)
        {
            return new Group { Name = dto.Name, Id = dto.Id };
        }

        private GroupDto SDMapToGroupDto(Group group)
        {
            return new GroupDto { Name = group.Name, Id = group.Id };
        }

        [HttpPost()]
        public async Task<Group> AddNewGroup(Group group)
        {
            using (var context = new Context())
            {
                context.Groups.Add(SDMapToGroupDto(group));
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