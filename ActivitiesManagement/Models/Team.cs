using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActivitiesManagement.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Numele este obligatoriu")]
        [StringLength(40, ErrorMessage = "Numele nu poate avea mai mult de 40 de caractere")]
        public string Name { get; set; }
    }
}