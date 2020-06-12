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
    public class HabbitController : Controller
    {
        private ApplicationDbContext _context;
        public HabbitController()
        {
             _context = new ApplicationDbContext();
        }

        // GET: Goals
        public ActionResult Index()
        {
            var planner = _context.Planners
                .SingleOrDefault(p => p.PlannerTypeId == (int)PlannerPeriod.EVERYDAY);
            
            if(planner == null)
            {
                var newPlanner = new Planner
                {
                    PlannerTypeId = (int)PlannerPeriod.EVERYDAY,
                    ValidFrom = DateTime.Today,
                    ValidTo = DateTime.MaxValue
                };

                _context.Planners.Add(newPlanner);
                _context.SaveChanges();

                planner = _context.Planners
                .SingleOrDefault(p => p.PlannerTypeId == (int)PlannerPeriod.EVERYDAY);

                int everydayGoals = 6;
                var project = new Project
                {
                    PlannerId = planner.Id,
                    CategoryId = everydayGoals
                };
                _context.Projects.Add(project);
                _context.SaveChanges();
            }


            var goals = _context.Goals
                .Where(g => g.StatusId == (int)GoalStatus.EVERYDAY)
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
                goal.StatusId = (int)GoalStatus.EVERYDAY;
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
