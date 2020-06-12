using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class PlannerType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public enum PlannerPeriod
    {
        YEAR = 1,
        MONTH = 2,
        WEEK = 3,
        DAY = 4,
        INBOX = 5,
        EVERYDAY = 6
    }
}