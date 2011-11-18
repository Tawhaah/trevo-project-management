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

        //issue
        public int TotalIssues { get; set; }
        public int FinishedIssues { get; set; }
        public int UnfinishedIssues { get; set; }

        //milestones
        public List<Milestone> MilestonesDuringWeek { get; set; }
        public List<Milestone> MilestonesNextWeek { get; set; }
        public List<Milestone> MilestonesDuringMonth { get; set; }

        //conferences
        public List<Conference> ConfsDuringWeek { get; set; }
        public List<Conference> ConfsNextWeek { get; set; }
        public List<Conference> ConfsDuringMonth { get; set; }
    }
}