using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Frame
{
    public class ValidateResult
    {
        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; } 
    }
}
