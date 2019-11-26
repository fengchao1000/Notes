using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models.Categorys
{
    public class Category  
    {
        [PrimaryKey]
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual int UsageCount { get; set; }
    }
}
