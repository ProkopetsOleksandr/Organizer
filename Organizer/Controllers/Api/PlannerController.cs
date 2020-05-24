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

            _context.Goals.Add(goal);
            _context.SaveChanges();

            return Ok();
        }
    }
}
