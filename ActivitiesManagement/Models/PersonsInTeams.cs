using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActivitiesManagement.Models
{
    public class PersonsInTeams
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int TeamId { get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }
        public virtual Team Teams { get; set; }
        [NotMapped]
       public ICollection<SelectListItem> UsersForTeams { get; set; }
    }
}