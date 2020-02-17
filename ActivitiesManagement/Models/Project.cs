using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActivitiesManagement.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Titlul este obligatoriu")]
        [StringLength(40, ErrorMessage = "Titlul nu poate avea mai mult de 40 de caractere")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Echipa este obligatorie")]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> TeamsForProjects { get; set; }
    }
}