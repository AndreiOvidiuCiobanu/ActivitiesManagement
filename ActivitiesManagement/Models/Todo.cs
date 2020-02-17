using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActivitiesManagement.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Titlul este obligatoriu")]
        [StringLength(40, ErrorMessage = "Titlul nu poate avea mai mult de 40 de caractere")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Statusul este obligatoriu")]
        public int StatusId { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Campul trebuie sa contina data si ora")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Campul trebuie sa contina data si ora")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateEnd { get; set; }
        public virtual Status Status { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Project Project { get; set; }
        public int ProjectId { get; set; }
        public string ApplicationUserId { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> StatusesForTasks { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> PersonsForTasks { get; set; }
        [NotMapped]
        public string SelectedValue { get; set; }
    }
}