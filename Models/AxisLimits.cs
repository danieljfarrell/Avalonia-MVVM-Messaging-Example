using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOneModelTwoViews.Models
{
    public class AxisLimits(float minValue, float maxValue)
    {
        public float Min { get; set; } = minValue;

        public float Max { get; set; } = maxValue;
    }
}
