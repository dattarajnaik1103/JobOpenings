
using System;

namespace JobOpenings.Models
{
    public class JobOpeningsModel
    {
        public int Id { get; set; }
        public string Code => $"JOB-{Id:D2}";
        public string Title { get; set; }           
        public string Description { get; set; }        
        public Location Location { get; set; }        
        public Department Department { get; set; }     
        public DateTime PostedDate { get; set; }       
        public DateTime ClosingDate { get; set; }     
    }

}