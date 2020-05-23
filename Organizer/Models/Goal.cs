using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organizer.Models
{
    public class Goal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateFinished { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        public int StatusId { get; set; }


        public Project Project { get; set; }
        public Status Status { get; set; }
    }
}