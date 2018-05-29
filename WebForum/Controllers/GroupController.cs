using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebForum.Repositories;
using CommonLibrary.Models;

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
                    groups.Add(MapToGroup(group));
                }

                return groups;
            }
        }

        private Group MapToGroup(GroupDto dto)
        {
            return new Group { Name = dto.Text, Id = dto.Id };
        }

        private GroupDto MapToGroupDto(Group group)
        {
            return new GroupDto { Text = group.Name, Id = group.Id };
        }

        [HttpPost()]
        public async Task<Group> AddNewGroup(Group group)
        {
            using (var context = new Context())
            {
                context.Groups.Add(MapToGroupDto(group));
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