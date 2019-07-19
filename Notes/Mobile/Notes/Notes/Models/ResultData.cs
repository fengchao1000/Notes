using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Notes.Models
{
    public class ResultData<TResult>
    {
        public bool IsSuccess { get; set; }

        public TResult Data { get; set; }

        public string Message { get; set; }
    }


}
