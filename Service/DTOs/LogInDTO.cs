﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class LogInDTO
    {

        public string username { get; set; }

        public string password { get; set; }

        public LogInDTO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

    }
}
