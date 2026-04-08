using System;
using System.Collections.Generic;
using System.Text;

namespace Vrittiy.Core.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public int RecruiterId { get; set; }
        public User Recruiter { get; set; }

        public ICollection<JobApplication> Applications { get; set; }
    }
}
