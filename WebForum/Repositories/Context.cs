using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebForum.Repositories
{
    public class Context : DbContext
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<GroupDto> Groups { get; set; }
        public DbSet<MessageDto> Messages { get; set; }
    }
}