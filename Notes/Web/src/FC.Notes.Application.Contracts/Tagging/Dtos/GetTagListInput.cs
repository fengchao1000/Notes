using System;
using System.Collections.Generic;

namespace FC.Notes.Tagging.Dtos
{
    public class GetTagListInput
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}