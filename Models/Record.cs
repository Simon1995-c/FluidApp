using System;

namespace Models
{
    public class Record
    {
        public DateTime Name { get; set; }
        public string NameString { get; set; }
        public double? Amount { get; set; }
        public double? Max { get; set; }
        public double? Min { get; set; }
    }
}
