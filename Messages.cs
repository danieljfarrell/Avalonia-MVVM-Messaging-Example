using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOneModelTwoViews.Models;
using TestOneModelTwoViews.ViewModels;

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
