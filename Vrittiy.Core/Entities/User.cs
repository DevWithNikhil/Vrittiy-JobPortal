using System;
using System.Collections.Generic;
using System.Text;

namespace Vrittiy.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // USER or RECRUITER

        public ICollection<JobApplication> Applications { get; set; }
    }
}
