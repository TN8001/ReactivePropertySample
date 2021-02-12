using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReactivePropertySample_VVM.Behaviors
{
    public class OnlyNumberTextBoxBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += Loaded;
            AssociatedObject.InputBindings.Add(new KeyBinding(ApplicationCommands.NotACommand, Key.Space, ModifierKeys.None));
            AssociatedObject.InputBindings.Add(new KeyBinding(ApplicationCommands.NotACommand, Key.Space, ModifierKeys.Shift));
            InputMethod.SetIsInputMethodEnabled(AssociatedObject, false);
            AssociatedObject.ContextMenu = null;
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Unloaded += Unloaded;
            AssociatedObject.PreviewTextInput += PreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        private void Unloaded(object sender, RoutedEventArgs e)
        {
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
            AssociatedObject.PreviewTextInput -= PreviewTextInput;
            AssociatedObject.Unloaded -= Unloaded;
        }

        protected override void OnDetaching()
        {
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
            AssociatedObject.PreviewTextInput -= PreviewTextInput;
            AssociatedObject.Unloaded -= Unloaded;
            AssociatedObject.Loaded -= Loaded;
            base.OnDetaching();
        }

        private static void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var c in e.Text)
            {
                if (IsDigit(c) is false)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true) is false ||
                e.SourceDataObject.GetData(DataFormats.UnicodeText) is not string text)
            {
                return;
            }
            foreach (var c in text)
            {
                if (IsDigit(c) is false)
                {
                    e.CancelCommand();
                    e.Handled = true;
                    return;
                }
            }
        }

        private static bool IsDigit(char c) => c is >= '0' and <= '9';
    }
}
