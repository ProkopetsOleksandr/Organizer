using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum GoalStatus
    {
        NEW = 1,
        INPROCESS = 2,
        DONE = 3,
        FAILED = 4,
        EVERYDAY = 5
    }
}