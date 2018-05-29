using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class GroupDto
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }
    }
}
