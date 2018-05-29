using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLibrary.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
            return $"Nazwa grupy: {Name}";
        }
    }
}