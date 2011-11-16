using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjctMngmt.Models
{
    public class DashboardModel
    {
        //task
        public int TotalTasks { get; set; }
        public int FinishedTasks { get; set; }
        public int UnfinishedTasks { get; set; }

        //project
        public int TotalProjects { get; set; }
        public int FinishedProjects { get; set; }
        public int UnfinishedProjects { get; set; }

        //developer
        public int TotalDevelopers { get; set; }
    }
}