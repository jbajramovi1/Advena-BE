﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Api.Models
{
    public class UserModel
    {
        public UserModel() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
    }
}
