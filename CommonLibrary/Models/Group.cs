﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForum.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group(int id, string name)
        {
            Id = id;
            Name = name;
        }

        
    }
}