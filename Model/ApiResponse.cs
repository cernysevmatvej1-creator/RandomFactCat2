using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDeepSeekl.Model
{
    public class ApiResponse
    {
        public List<Fact> Data { get; set; }
        public bool Success { get; set; }
    }
}
