using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class KeyValueTable
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
