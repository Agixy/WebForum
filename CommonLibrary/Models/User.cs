using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Imie: {Name}, e-mail: {Email}";
        }
    }
}