using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.ViewModels
{
    public class NewGoalViewModel
    {
        public int PlannerId { get; set; }
        public int ProjectId { get; set; }
        public string GoalName { get; set; }
    }
}