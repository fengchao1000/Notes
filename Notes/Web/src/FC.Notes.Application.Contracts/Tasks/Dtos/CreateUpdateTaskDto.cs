using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FC.Notes.Tasks.Dtos
{ 
     public class CreateUpdateTaskDto
    { 

        [Required]
        [StringLength(TaskConsts.MaxTitleLength)]
        public string Title { get; set; }
         
        [StringLength(TaskConsts.MaxRemarkLength)]
        public string Remark { get; set; }

        [Required] 
        public TaskPriority Priority { get; set; }

        [Required]
        public TaskType TaskType { get; set; }

        [Required]
        public int Status { get; set; } 

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }
}
