using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactivePropertySample.Core;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace ReactivePropertySample.ViewModels
{
    public class SettingViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067

        private readonly CompositeDisposable disposable = new();

        public SettingModel View { get; } = new();
        public ReadOnlyReactiveProperty<bool> IsGod { get; }
        public ReactiveCommand SaveCommand { get; }
        public ReactiveCommand ClosingCommand { get; } = new();

        public SettingViewModel()
        {
            IsGod = View.Name.Select(x => x == "ぽんた").ToReadOnlyReactiveProperty().AddTo(disposable);
            SaveCommand = IsGod.ToReactiveCommand().WithSubscribe(View.Save).AddTo(disposable);
            ClosingCommand.Subscribe(() =>
            {
                if (SaveCommand.CanExecute()) SaveCommand.Execute();
                disposable.Dispose();
            });
        }
    }
}
