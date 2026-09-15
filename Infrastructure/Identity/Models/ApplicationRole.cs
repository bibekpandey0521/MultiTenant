using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Identity.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}