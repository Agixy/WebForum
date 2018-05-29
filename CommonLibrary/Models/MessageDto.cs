using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public virtual GroupDto Group { get; set; }
    }
}
