using Organizer.Models;
using Organizer.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Organizer.Controllers.Api
{
    public class PlannerController : ApiController
    {
        private ApplicationDbContext _context;
        public PlannerController()
        {
            _context = new ApplicationDbContext();
        }


        /// <summary>
        /// Get goals of planner
        /// </summary>
        /// <param name="id">planner id</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetGoals(int id)
        {
            var plannerInDb = _context.Planners.Find(id);
            if (plannerInDb == null)
                return NotFound();

            var goals = _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Planner)
                .Where(p => p.Planner.Id == id)
                .Select(p => new
                {
                    projectId = p.Id,
                    category = p.Category.Name,
                    goals = from goal in _context.Goals
                            where (goal.ProjectId == p.Id)
                            select goal
                })
                .ToList();

            return Ok(goals);
        }


        [HttpGet]
        [Route("api/planner/get-year-goals")]
        public IHttpActionResult GetYearGoals()
        {

            var todayDate = DateTime.Today;
            var plannerInDb = _context.Planners
                .SingleOrDefault(p => p.ValidFrom <= todayDate &&
                                      p.ValidTo >= todayDate &&
                                      p.PlannerTypeId == (int)PlannerPeriod.YEAR);

            if (plannerInDb == null)
                return NotFound();

            var goals = _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Planner)
                .Where(p => p.Planner.Id == plannerInDb.Id)
                .Select(p => new
                {
                    projectId = p.Id,
                    category = p.Category.Name,
                    goals = from goal in _context.Goals
                            where (goal.ProjectId == p.Id)
                            select goal
                })
                .ToList();

            return Ok(goals);
        }

        [HttpGet]
        [Route("api/planner/get-month-goals")]
        public IHttpActionResult GetMonthGoals()
        {

            var todayDate = DateTime.Today;
            var plannerInDb = _context.Planners
                .SingleOrDefault(p => p.ValidFrom <= todayDate &&
                                      p.ValidTo >= todayDate &&
                                      p.PlannerTypeId == (int)PlannerPeriod.MONTH);

            if (plannerInDb == null)
                return NotFound();

            var goals = _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Planner)
                .Where(p => p.Planner.Id == plannerInDb.Id)
                .Select(p => new
                {
                    projectId = p.Id,
                    category = p.Category.Name,
                    goals = from goal in _context.Goals
                            where (goal.ProjectId == p.Id)
                            select goal
                })
                .ToList();

            return Ok(goals);
        }

        [HttpGet]
        [Route("api/planner/get-week-goals")]
        public IHttpActionResult GetWeekGoals()
        {

            var todayDate = DateTime.Today;
            var plannerInDb = _context.Planners
                .SingleOrDefault(p => p.ValidFrom <= todayDate &&
                                      p.ValidTo >= todayDate &&
                                      p.PlannerTypeId == (int)PlannerPeriod.WEEK);

            if (plannerInDb == null)
                return NotFound();

            var goals = _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Planner)
                .Where(p => p.Planner.Id == plannerInDb.Id)
                .Select(p => new
                {
                    projectId = p.Id,
                    category = p.Category.Name,
                    goals = from goal in _context.Goals
                            where (goal.ProjectId == p.Id)
                            select goal
                })
                .ToList();

            return Ok(goals);
        }


        [HttpPost]
        public IHttpActionResult SetNewGoal(NewGoalViewModel viewModel)
        {
            var plannerInDb = _context.Planners.Find(viewModel.PlannerId);
            if (plannerInDb == null)
            {
                return NotFound();
            }

            var projectInDb = _context.Projects.Find(viewModel.ProjectId);
            if (projectInDb == null)
            {
                return NotFound();
            }

            var goal = new Goal
            {
                DateAdded = DateTime.Today,
                ProjectId = viewModel.ProjectId,
                Name = viewModel.GoalName,
                StatusId = (int)GoalStatus.INPROCESS
            };

            var addedGoal = _context.Goals.Add(goal);
            _context.SaveChanges();

            return Ok(addedGoal);
        }

        
        [Route("api/planner/move-planner")]
        [HttpPost]
        public IHttpActionResult MoveGoal(MoveGoalViewModel viewModel)
        {
            var goalInDb = _context.Goals.Find(viewModel.GoalId);
            if(goalInDb == null)
            {
                return NotFound();
            }


            if(viewModel.ProjectId == -1)
            {
                var planner = _context.Planners
                    .SingleOrDefault(p => p.PlannerTypeId == (int)PlannerPeriod.INBOX);

                var project = _context.Projects
                    .SingleOrDefault(p => p.PlannerId == planner.Id);

                goalInDb.ProjectId = project.Id;
                goalInDb.StatusId = (int)GoalStatus.NEW;
            }
            else
            {
                goalInDb.ProjectId = viewModel.ProjectId;
                goalInDb.StatusId = (int)GoalStatus.INPROCESS;
            }

            
            _context.SaveChanges();
            return Ok();
        }


        [Route("api/planner/dayplanners")]
        [HttpGet]
        public IHttpActionResult GetAllDayPlanners()
        {
            var plannersInDb = _context.Planners
                .Where(p => p.PlannerTypeId == (int)PlannerPeriod.DAY)
                .OrderByDescending(p => p.Id)
                .ToList();

            return Ok(plannersInDb);
        }

        [Route("api/planner/weekplanners")]
        [HttpGet]
        public IHttpActionResult GetAllWeekPlanners()
        {
            var plannersInDb = _context.Planners
                .Where(p => p.PlannerTypeId == (int)PlannerPeriod.WEEK)
                .OrderByDescending(p => p.Id)
                .ToList();

            return Ok(plannersInDb);
        }

        [Route("api/planner/monthplanners")]
        [HttpGet]
        public IHttpActionResult GetAllMonthPlanners()
        {
            var plannersInDb = _context.Planners
                .Where(p => p.PlannerTypeId == (int)PlannerPeriod.MONTH)
                .OrderByDescending(p => p.Id)
                .ToList();

            return Ok(plannersInDb);
        }

        [Route("api/planner/yearplanners")]
        [HttpGet]
        public IHttpActionResult GetAllYearPlanners()
        {
            var plannersInDb = _context.Planners
                .Where(p => p.PlannerTypeId == (int)PlannerPeriod.YEAR)
                .OrderByDescending(p => p.Id)
                .ToList();

            return Ok(plannersInDb);
        }
    }
}
