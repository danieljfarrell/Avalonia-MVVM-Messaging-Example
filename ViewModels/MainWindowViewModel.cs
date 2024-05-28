using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TestOneModelTwoViews.Models;

namespace TestOneModelTwoViews.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        // The model object containing the shared axis values
        [ObservableProperty]
        private AxisLimits _AxisLimits = new AxisLimits((float)0.0, (float)1.0);

        // A view-model of the axis values, this is shared by all view-models
        public AxisLimitsViewModel AxisLimitsViewModel { get; set; }

        // A representation of the axis model as a settings view
        public SettingsViewModel SettingsViewModel { get; set; }

        // A representation of the axis model as a plot view
        public PlotViewModel PlotViewModel { get; set; }

        public MainWindowViewModel()
        {
            AxisLimitsViewModel = new AxisLimitsViewModel(AxisLimits);
            SettingsViewModel = new SettingsViewModel(AxisLimitsViewModel);
            PlotViewModel = new PlotViewModel(AxisLimitsViewModel);
        }

    }
}
