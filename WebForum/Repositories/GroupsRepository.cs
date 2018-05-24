using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebForum.Repositories
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly IList<Group> actualGroups = new List<Group>
        {
                // jak tu dodac grupy? nowe?
        };

        public IEnumerable<Group> GetAll()
        {
            return actualGroups;
        }
    }
}