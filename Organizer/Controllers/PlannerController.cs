using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Organizer.ViewModels;
using System.Globalization;

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

        [Route("planner/year-planner/{id:int?}")]
        public ActionResult YearPlanner(int? id = null)
        {
            Planner currentPlanner;
            List<Goal> inboxGoals;

            if (id != null)
            {
                currentPlanner = _context.Planners.Find(id);
                inboxGoals = null;
            }
            else
            {
                currentPlanner = GetCurrentPlanner(PlannerPeriod.YEAR);
                inboxGoals = _context.Goals
                .Where(i => i.StatusId == (int)GoalStatus.NEW).ToList();
            }

            var viewModel = new PlannerViewModel
            {
                Planner = currentPlanner,
                InboxGoals = inboxGoals
            };

            return View(viewModel);
        }

        [Route("planner/month-planner/{id:int?}")]
        public ActionResult MonthPlanner(int? id = null)
        {
            Planner currentPlanner;
            List<Goal> inboxGoals;

            if (id != null)
            {
                currentPlanner = _context.Planners.Find(id);
                inboxGoals = null;
            }
            else
            {
                currentPlanner = GetCurrentPlanner(PlannerPeriod.MONTH);
                inboxGoals = _context.Goals
                .Where(i => i.StatusId == (int)GoalStatus.NEW).ToList();
            }

            var viewModel = new PlannerViewModel
            {
                Planner = currentPlanner,
                InboxGoals = inboxGoals
            };

            return View(viewModel);
        }

        [Route("planner/week-planner/{id:int?}")]
        public ActionResult WeekPlanner(int? id = null)
        {
            Planner currentPlanner;
            List<Goal> inboxGoals;

            if (id != null)
            {
                currentPlanner = _context.Planners.Find(id);
                inboxGoals = null;
            }
            else
            {
                currentPlanner = GetCurrentPlanner(PlannerPeriod.WEEK);
                inboxGoals = _context.Goals
                .Where(i => i.StatusId == (int)GoalStatus.NEW).ToList();
            }

            var viewModel = new PlannerViewModel
            {
                Planner = currentPlanner,
                InboxGoals = inboxGoals
            };

            return View(viewModel);
        }

        [Route("planner/day-planner/{id:int?}")]
        public ActionResult DayPlanner(int? id = null)
        {
            Planner currentPlanner;
            List<Goal> inboxGoals;

            if (id != null)
            {
                currentPlanner = _context.Planners.Find(id);
                inboxGoals = null;
            }
            else
            {
                currentPlanner = GetCurrentPlanner(PlannerPeriod.DAY);
                inboxGoals = _context.Goals
                .Where(i => i.StatusId == (int)GoalStatus.NEW).ToList();
            }
            
            var viewModel = new PlannerViewModel
            {
                Planner = currentPlanner,
                InboxGoals = inboxGoals
            };

            return View(viewModel);
        }

        [Route("planner/day-planner/all")]
        public ActionResult DayPlannerAll()
        {
            return View();
        }

        [Route("planner/week-planner/all")]
        public ActionResult WeekPlannerAll()
        {
            return View();
        }

        [Route("planner/month-planner/all")]
        public ActionResult MonthPlannerAll()
        {
            return View();
        }

        [Route("planner/year-planner/all")]
        public ActionResult YearPlannerAll()
        {
            return View();
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
                Planner newPlanner = null;
                switch (plannerType)
                {
                    case PlannerPeriod.YEAR:
                        newPlanner = new Planner
                        {
                            PlannerTypeId = (int)plannerType,
                            ValidFrom = todayDate,
                            ValidTo = new DateTime(todayDate.Year, 12, 31)
                        };
                        break;
                    case PlannerPeriod.MONTH:
                        newPlanner = new Planner
                        {
                            PlannerTypeId = (int)plannerType,
                            ValidFrom = todayDate,
                            ValidTo = new DateTime(todayDate.Year, todayDate.Month, 31)
                        };
                        break;
                    case PlannerPeriod.WEEK:
                        newPlanner = new Planner
                        {
                            PlannerTypeId = (int)plannerType,
                            ValidFrom = todayDate,
                            ValidTo = LastDayOfWeek(todayDate)
                        };
                        break;
                    case PlannerPeriod.DAY:
                        newPlanner = new Planner
                        {
                            PlannerTypeId = (int)plannerType,
                            ValidFrom = todayDate,
                            ValidTo = todayDate
                        };
                        break;
                }
                _context.Planners.Add(newPlanner);
                _context.SaveChanges();

                plannerInDb = _context.Planners
                .SingleOrDefault(p => p.ValidFrom <= todayDate &&
                                      p.ValidTo >= todayDate &&
                                      p.PlannerTypeId == (int)plannerType);


                List<int> categories = new List<int>();

                switch(plannerType)
                {
                    case PlannerPeriod.YEAR:
                        categories = new List<int> { 1, 2 };
                        break;
                    case PlannerPeriod.MONTH:
                        categories = new List<int> { 3, 7, 8 };
                        break;
                    case PlannerPeriod.WEEK:
                        categories = new List<int> { 4, 7, 8 };
                        break;
                    case PlannerPeriod.DAY:
                        categories = new List<int> { 5, 7, 8, 9, 6 };


                        break;
                }

                foreach (var category in categories)
                {
                    var project = new Project
                    {
                        CategoryId = category,
                        PlannerId = plannerInDb.Id
                    };
                    _context.Projects.Add(project);
                }
                _context.SaveChanges();


                var goals = _context.Goals.Where(g => g.StatusId == (int)GoalStatus.EVERYDAY);
                int everydayCategory = 6;
                var projectEveryday = _context.Projects
                    .SingleOrDefault(p => p.PlannerId == plannerInDb.Id && p.CategoryId == everydayCategory);
                if(projectEveryday != null)
                {
                    foreach(var goal in goals)
                    {
                        var newGoal = new Goal
                        {
                            Name = goal.Name,
                            StatusId = (int)GoalStatus.INPROCESS,
                            DateAdded = goal.DateAdded,
                            DateFinished = null,
                            ProjectId = projectEveryday.Id,
                            Description = goal.Description
                        };

                        _context.Goals.Add(newGoal);
                    }
                    _context.SaveChanges();
                }
            }

            return plannerInDb;
        }



        private static DateTime FirstDayOfWeek(DateTime date)
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate;
        }

        private static DateTime LastDayOfWeek(DateTime date)
        {
            DateTime ldowDate = FirstDayOfWeek(date).AddDays(6);
            return ldowDate;
        }
    }
}