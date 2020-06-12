using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.ViewModels
{
    public class MoveGoalViewModel
    {
        /// <summary>
        /// Goal to move
        /// </summary>
        public int GoalId { get; set; }

        /// <summary>
        /// Project where need to move current goal
        /// </summary>
        public int ProjectId { get; set; }
    }
}