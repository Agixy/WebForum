using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class UserDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }

        public virtual ICollection<GroupDto> Groups { get; set; }
    }
}
