using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMBusinessLayer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeCustomAttribute : Attribute
    {
        public double Min {  get; set; }
        public double Max { get; set; }
        public string ErrorMessage { get; set; }

        public RangeCustomAttribute(double min, double max) 
        { 
            Max = max;
            Min = min;
        }
    }
}
