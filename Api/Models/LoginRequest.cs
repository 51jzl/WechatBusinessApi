﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WB.Api.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}