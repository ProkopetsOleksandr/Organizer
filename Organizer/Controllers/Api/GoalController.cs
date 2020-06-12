using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Organizer.Controllers.Api
{
    public class GoalController : ApiController
    {
        private ApplicationDbContext _context;
        public GoalController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult ChangeGoalStatus(int id)
        {
            var goalInDb = _context.Goals.Find(id);
            if (goalInDb == null)
                return NotFound();

            if(goalInDb.StatusId == (int)GoalStatus.INPROCESS)
            {
                goalInDb.StatusId = (int)GoalStatus.DONE;
                goalInDb.DateFinished = DateTime.Today;
            }
            else
            {
                goalInDb.StatusId = (int)GoalStatus.INPROCESS;
                goalInDb.DateFinished = null;
            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteGoal(int id)
        {
            var goalInDb = _context.Goals.Find(id);
            if(goalInDb == null)
            {
                return NotFound();
            }

            _context.Goals.Remove(goalInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
