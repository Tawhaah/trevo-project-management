using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjctMngmt.Models
{
    public class TeamDeveloperModel
    {
        public IEnumerable<Team> teams { get; set; }
        public IEnumerable<Developer> developers { get; set; }
    }
}