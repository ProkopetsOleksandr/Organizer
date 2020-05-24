using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Organizer;
using Organizer.Models;

namespace Organizer.Controllers
{
    public class InboxController : Controller
    {
        private ApplicationDbContext _context;
        public InboxController()
        {
             _context = new ApplicationDbContext();
        }

        // GET: Goals
        public ActionResult Index()
        {
            var planner = _context.Planners
                .SingleOrDefault(p => p.PlannerTypeId == (int)PlannerPeriod.INBOX);
            if(planner == null)
            {
                var newPlanner = new Planner
                {
                    PlannerTypeId = (int)PlannerPeriod.INBOX,
                    ValidFrom = DateTime.Today,
                    ValidTo = DateTime.MaxValue
                };
                _context.Planners.Add(newPlanner);
                _context.SaveChanges();

                planner = _context.Planners
                .SingleOrDefault(p => p.PlannerTypeId == (int)PlannerPeriod.INBOX);

                int unsortedGoals = 12;
                var project = new Project
                {
                    PlannerId = planner.Id,
                    CategoryId = unsortedGoals
                };
                _context.Projects.Add(project);
                _context.SaveChanges();
            }


            var goals = _context.Goals
                .Where(g => g.StatusId == (int)GoalStatus.NEW)
                .ToList();

            return View(goals);
        }


        // GET: Goals/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Goal goal)
        {
            if (ModelState.IsValid)
            {
                goal.StatusId = (int)GoalStatus.NEW;
                goal.DateAdded = DateTime.Today;

                int unsortedGoals = 12;
                var project = _context.Projects
                    .SingleOrDefault(p => p.CategoryId == unsortedGoals);

                goal.ProjectId = project.Id;

                _context.Goals.Add(goal);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(goal);
        }
    }
}
