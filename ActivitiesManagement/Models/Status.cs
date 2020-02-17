using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActivitiesManagement.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}