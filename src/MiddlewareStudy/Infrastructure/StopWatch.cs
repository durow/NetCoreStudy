using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareStudy.Infrastructure
{
    public class StopWatch
    {
        public DateTime StartTime { get; private set; } = DateTime.Now;

        public void Start()
        {
            StartTime = DateTime.Now;
        }

        public double GetMillionSeconds()
        {
            return (DateTime.Now - StartTime).TotalMilliseconds;
        }
    }
}
