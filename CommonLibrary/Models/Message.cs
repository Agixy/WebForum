using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLibrary.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Text}";
        }
    }
}