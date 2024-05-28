using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOneModelTwoViews.Messages;
using TestOneModelTwoViews.Models;

namespace TestOneModelTwoViews.ViewModels
{
    public partial class PlotViewModel : ViewModelBase
    {
        [ObservableProperty]
        private AxisLimits _AxisLimits;

        [ObservableProperty]
        private float _Min;

        [ObservableProperty]
        private float _Max;

        partial void OnMinChanged(float value)
        {
            AxisLimits = new(value, Max);
            WeakReferenceMessenger.Default.Send(new AxisLimitsChangedMessage(AxisLimits));
        }

        partial void OnMaxChanged(float value)
        {
            AxisLimits = new(Min, value);
            WeakReferenceMessenger.Default.Send(new AxisLimitsChangedMessage(AxisLimits));
        }

        partial void OnAxisLimitsChanged(AxisLimits? oldValue, AxisLimits newValue)
        {
            if (ReferenceEquals(oldValue, newValue))
            {
                return;
            }
            else
            {
                AxisLimits = newValue;
                Min = newValue.Min;
                Max = newValue.Max;
            }
        }

        public PlotViewModel(AxisLimits axisLimits)
        {
            AxisLimits = axisLimits;
            Min = AxisLimits.Min;
            Max = AxisLimits.Max;
        }
    }
}
