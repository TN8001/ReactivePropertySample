using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReactivePropertySample_VVM.Behaviors
{
    public class ViewModelDisposeBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closed += WindowClosed;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Closed -= WindowClosed;
        }

        private void WindowClosed(object? sender, EventArgs e)
        {
            if(AssociatedObject.DataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
