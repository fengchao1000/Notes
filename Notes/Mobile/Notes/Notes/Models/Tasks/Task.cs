using Notes.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json.Serialization;

namespace Notes.Models
{ 
    public class TaskModel  
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Remark { get; set; }

        public TaskPriority Priority { get; set; }
        public TaskType TaskType { get; set; }
        public int Status { get; set; }

        public Guid? TenantId { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


        [JsonIgnore]
        [Ignore]
        public string EndTimeFormat 
        {
            get 
            {
                if (EndTime.Date == DateTime.UtcNow.Date)
                {
                    return $"今天{EndTime.Hour}点截止";
                }
                else if(EndTime.Date < DateTime.UtcNow.Date)
                {
                    return $"{EndTime.ToString(ConstanceHelper.DefaultTimeFormat)}截止,已逾期";
                }
                else
                {
                    return $"{EndTime.ToString(ConstanceHelper.DefaultTimeFormat)}截止";
                }
            }
        }
    }
}
