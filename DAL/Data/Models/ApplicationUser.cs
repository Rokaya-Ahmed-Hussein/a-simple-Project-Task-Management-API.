using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DAL.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Department {  get; set; }
    }
}
