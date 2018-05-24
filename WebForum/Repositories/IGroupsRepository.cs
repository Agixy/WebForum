using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebForum.Repositories
{
    public interface IGroupsRepository
    {
       IEnumerable<Group> GetAll();
    }
}