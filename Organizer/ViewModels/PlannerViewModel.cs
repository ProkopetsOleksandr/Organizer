using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.ViewModels
{
    public class PlannerViewModel
    {
        public Planner Planner { get; set; }
        public List<Goal> InboxGoals { get; set; }
    }
}