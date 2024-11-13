
using System;

namespace JobOpenings.Models
{
    public class JobOpeningsModel
    {
        public int Id { get; set; }
        public string Code => $"JOB-{Id:D2}";
        public string Title { get; set; }              // Job title
        public string Description { get; set; }        // Job description
        public Location Location { get; set; }         // Nested Location object
        public Department Department { get; set; }     // Nested Department object
        public DateTime PostedDate { get; set; }       // Date the job was posted
        public DateTime ClosingDate { get; set; }      // Application closing date
    }

}