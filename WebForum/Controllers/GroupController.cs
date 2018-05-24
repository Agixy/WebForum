using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebForum.Repositories;

namespace WebForum.Controllers
{
    public class GroupController : ApiController
    {
        private readonly IGroupsRepository groupsRepository;

        public GroupController(IGroupsRepository groupRepository)
        {
            this.groupsRepository = groupRepository;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return groupsRepository.GetAll();
        }

        public IHttpActionResult GetGroup(int id)
        {
            return null;
            //var product = products.FirstOrDefault((p) => p.Id == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}
            //return Ok(product);
        }
    }
}