using System;

namespace DataBaseLogger
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
    }
}
