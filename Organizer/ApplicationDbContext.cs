using Microsoft.AspNet.Identity.EntityFramework;
using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Organizer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Goals and ideas
        /// </summary>
        public DbSet<Goal> Goals { get; set; }

        /// <summary>
        /// Current status of goal, or idea
        /// </summary>
        public DbSet<Status>  Statuses { get; set; }

        /// <summary>
        /// Potential categories of planner
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Planner
        /// </summary>
        public DbSet<Planner>  Planners { get; set; }

        /// <summary>
        /// Type planner: year, month, week, day
        /// </summary>
        public DbSet<PlannerType> PlannerTypes { get; set; }

        /// <summary>
        /// Goals in categories that belong to planner
        /// </summary>
        public DbSet<Project> Projects { get; set; }

    public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}