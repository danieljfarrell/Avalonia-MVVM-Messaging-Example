using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOneModelTwoViews.Models;

namespace TestOneModelTwoViews.ViewModels
{
    public partial class AxisLimitsViewModel: ViewModelBase
    {
        [ObservableProperty]
        private AxisLimits _AxisLimits;

        [ObservableProperty]
        private float _Min;

        [ObservableProperty]
        private float _Max;
        public AxisLimitsViewModel(AxisLimits axisLimits)
        {
            AxisLimits = axisLimits;
            Min = axisLimits.Min;
            Max = axisLimits.Max;
        }


    }
}
