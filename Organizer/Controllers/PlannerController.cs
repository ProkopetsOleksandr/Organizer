using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Organizer.ViewModels;

namespace Organizer.Controllers
{
    public class PlannerController : Controller
    {
        private ApplicationDbContext _context;
        public PlannerController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Planer
        public ActionResult Index()
        {
            return View();
        }

        [Route("planner/year-planner")]
        public ActionResult YearPlanner()
        {
            var currentPlanner = GetCurrentPlanner(PlannerPeriod.YEAR);

            var inboxGoals = _context.Goals
                .Where(i => i.StatusId == (int)GoalStatus.NEW).ToList();

            var viewModel = new PlannerViewModel
            {
                Planner = currentPlanner,
                InboxGoals = inboxGoals
            };

            return View(viewModel);
        }


        private Planner GetCurrentPlanner(PlannerPeriod plannerType)
        {
            var todayDate = DateTime.Today;

            var plannerInDb = _context.Planners
                .SingleOrDefault(p => p.ValidFrom <= todayDate &&
                                      p.ValidTo >= todayDate &&
                                      p.PlannerTypeId == (int)plannerType);

            if (plannerInDb == null)
            {
                var newPlanner = new Planner
                {
                    PlannerTypeId = (int)plannerType,
                    ValidFrom = todayDate,
                    ValidTo = new DateTime(todayDate.Year, 12, 31)
                };
                _context.Planners.Add(newPlanner);
                _context.SaveChanges();

                plannerInDb = _context.Planners
                .SingleOrDefault(p => p.ValidFrom <= todayDate &&
                                      p.ValidTo >= todayDate &&
                                      p.PlannerTypeId == (int)plannerType);

                switch(plannerType)
                {
                    case PlannerPeriod.YEAR:
                        var categories = new List<int> { 1, 2 };
                        foreach(var category in categories)
                        {
                            var project = new Project
                            {
                                CategoryId = category,
                                PlannerId = plannerInDb.Id
                            };
                            _context.Projects.Add(project);
                        }
                        break;
                }

                _context.SaveChanges();
            }

            return plannerInDb;
        }
    }
}