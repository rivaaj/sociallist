using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialList.Models
{
    public class TaskModel
    {
        [Display(Name = "Task")]
        [Required(ErrorMessage = "Please enter a task")]
        public string TaskDescription { get; set; }
    }
}
