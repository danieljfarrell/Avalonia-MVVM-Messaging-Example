using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOneModelTwoViews.Models;

namespace TestOneModelTwoViews.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private AxisLimitsViewModel _AxisLimitsViewModel;

        public SettingsViewModel(AxisLimitsViewModel axisLimitsViewModel)
        {
            AxisLimitsViewModel = axisLimitsViewModel;
        }
    }
}