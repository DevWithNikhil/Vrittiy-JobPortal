using System;
using System.Collections.Generic;
using System.Text;

namespace Vrittiy.Core.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string ResumePath { get; set; }

        public User User { get; set; }
        public Job Job { get; set; }
    }
}
