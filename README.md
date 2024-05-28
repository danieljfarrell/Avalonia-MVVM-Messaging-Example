# Avaloina MVVM Messaging Example

This example project shows how to keep view syncrhonised with the same model object. 

# The problem

Play along. Imagine the view on the left is some "settings" view and the view on the right is a "plot" view.

The settings wants to display the current axis limits of the plot. However! The use can edit the settings text boxes to update the plot and the user can pan the plot to update the text boxes.

Thus the view controller that has been mutated need to communicate this change to the other view controller. In this example, this is done using messages and the MVVM Community Toolkit.

# Views

View bind the view model attributes which mirror the model attributes (this is pretty much standard MVVM).

```axaml
<UserControl xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
			 xmlns:vm="using:TestOneModelTwoViews.ViewModels"
             mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
             x:Class="TestOneModelTwoViews.Views.SettingsView"
	         x:DataType="vm:SettingsViewModel">

	<StackPanel Margin="20">
		<TextBlock Margin="0 5">Min:</TextBlock>
		<TextBox Text="{Binding Min}"/>
		<TextBlock Margin="0 5">Max:</TextBlock>
		<TextBox Text="{Binding Max}"/>
	</StackPanel>
</UserControl>
```
## View-models

```C#
public partial class SettingsViewModel: ViewModelBase
 {
    // This model is shared across all views it contains the min and max values of a single axis.
    [ObservableProperty]
    private AxisLimits _AxisLimits;

    // The view-model representation of the axis minimum, we bind to this value in the view.
    [ObservableProperty]
    private float _Min;

    // The view-model representation of the axis maximum, we bind to this value in the view.
    [ObservableProperty]
    private float _Max;

    // Constructor takes the axis model and setups the values on the bindable properties.
    public SettingsViewModel(AxisLimits axisLimits)
    {
        AxisLimits = axisLimits;
        Min = AxisLimits.Min;
        Max = AxisLimits.Max;
    }
}
```

So far there is nothing special here.

Let's look at the change notification handlers.

```C#
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
```

when `Min` or `Max` change value these handlers will be called and simply create a new model object and set it. Notice the line,

```C#
WeakReferenceMessenger.Default.Send(new AxisLimitsChangedMessage(AxisLimits));
```

This using the MVVM Community Toolkit messaging to send a message to all subscribers. Moreover, the view-controller has updated itself with a new version of the model object and the message will be used to send this new object to other view-controller in the application. In this way they can stay synchronised.

More on how this is setup later, for now let's continue describing the application at a high-level.

We still need to handle change notification in the other direction, from the Plot view-model to the Settings view-model. We can do this by implementing a change handler for `AxisModel`.

```C#
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
```
Notice this updates the model and the properties. If only the model is updated the view will not be notified of the change.

In this example, the code in the Settings and Plot view-model is actually the same, the only difference is the name of the class! There might be scope for refactoring this and having the Settings and Plot view-model own a more general "AxisViewModel". I need to think about this a bit more. Sometimes a little bit of repeating yourself is fine. Thinking about it the Settings and Plot are representing the data in very different ways to it seems legitimate they are different objects, even if in this example, they seem the same because the example is so limited.

## Main view-model

The Window's view-model contains the shared model and both view-models, it also implements the message receiving.

```C#
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
        Messenger.RegisterAll(this);  // This line is very important! Without it the message will not be received.
    }

    // This method is called when either of the view-model sends the AxisLimitsChangedMessage message.
    public void Receive(AxisLimitsChangedMessage message)
    {              
        AxisLimits = message.Value;  // Update the axis limits model
    }

    // When the axis limits model changes distribute values to all view-models.
    partial void OnAxisLimitsChanged(AxisLimits value)
    {
        SettingsViewModel.AxisLimits = value;
        PlotViewModel.AxisLimits = value;
    }
}
```

## Messaging

Create a message object that takes the new axis limits model as the value, this way when the message is sent the Recipient gets model too.
```C#
namespace TestOneModelTwoViews.Messages
{
    // Create a message
    public class AxisLimitsChangedMessage : ValueChangedMessage<AxisLimits>
    {
        public AxisLimitsChangedMessage(AxisLimits limits) : base(limits)
        {
        }
    }
}
```

## Improvements
* I suspect there might be some message echo that, with a little bit more effort, can be removed.
* Are efficient is message sending as a mechanism, is it dynamic and slow?
* Should the Settings and Plot view model be unified into a single view-model: AxisLimitsViewModel?
    * Should the Settings and Plot view model own an AxisLimitsViewModel for the generic parts and implement non-logic shared logic.
