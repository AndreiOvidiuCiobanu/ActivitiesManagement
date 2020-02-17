using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace ActivitiesManagement.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Continutul este obligatoriu")]
        public string Point { get; set; }
        public string ApplicationUserId { get; set; }
        public int TodoId{ get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }
        public virtual Todo Todos { get; set; }
    }
}