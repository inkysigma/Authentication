using System.Collections.Generic;

namespace Authentication.Frame.Result
{
    public class ValidateResult
    {
        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; } 
    }
}
