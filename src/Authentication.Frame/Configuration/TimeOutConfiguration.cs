using System;

namespace Authentication.Frame.Configuration
{
    public class TimeOutConfiguration
    {
        public TimeSpan DelayScale { get; set; } = TimeSpan.FromSeconds(5);

        public TimeSpan LockoutTime { get; set; } = TimeSpan.FromMinutes(5);
    }
}
