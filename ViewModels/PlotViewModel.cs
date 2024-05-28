﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOneModelTwoViews.Models;

namespace TestOneModelTwoViews.ViewModels
{
    public partial class PlotViewModel : ViewModelBase
    {
        [ObservableProperty]
        private AxisLimitsViewModel _AxisLimitsViewModel;

        public PlotViewModel(AxisLimitsViewModel axisLimitsViewModel)
        {
            AxisLimitsViewModel = axisLimitsViewModel;
        }
    }
}
