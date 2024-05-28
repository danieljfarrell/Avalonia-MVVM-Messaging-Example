using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TestOneModelTwoViews.Messages;
using TestOneModelTwoViews.Models;

namespace TestOneModelTwoViews.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, IRecipient<AxisLimitsChangedMessage>
    {
        [ObservableProperty]
        AxisLimits _AxisLimits = new AxisLimits((float)0.0, (float)1.0);

        public SettingsViewModel SettingsViewModel { get; set; }

        public PlotViewModel PlotViewModel { get; set; }

        public MainWindowViewModel()
        {
            SettingsViewModel = new SettingsViewModel(AxisLimits);
            PlotViewModel = new PlotViewModel(AxisLimits);
            Messenger.RegisterAll(this);
        }

        public void Receive(AxisLimitsChangedMessage message)
        {              
            AxisLimits = message.Value;
        }

        partial void OnAxisLimitsChanged(AxisLimits value)
        {
            SettingsViewModel.AxisLimits = value;
            PlotViewModel.AxisLimits = value;
        }
    }
}
