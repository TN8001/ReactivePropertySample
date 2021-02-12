using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactivePropertySample_M;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactivePropertySample_VVM.ViewModels
{
    public class SettingViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Disposeまとめてしてくれるやつ
        private readonly CompositeDisposable disposable = new CompositeDisposable();

        public void Dispose()
        {
            disposable.Clear();
            disposable.Dispose();
        }

        private readonly SettingModel model;

        public ReactiveProperty<int> ViewWidth { get; }
        public ReactiveProperty<int> ViewHeight { get; }
        public ReactiveProperty<int> ViewX { get; }
        public ReactiveProperty<int> ViewY { get; }

        public ReactiveProperty<string> Name { get; }

        public ReadOnlyReactiveProperty<bool> IsGod { get; }
        public ReadOnlyReactiveProperty<string> Rank { get; }
        public ReadOnlyReactiveProperty<int> FontSize { get; }

        public ReadOnlyReactiveProperty<string> ViewRectInfo { get; }

        public ReactiveCommand SaveCommand { get; }

        public SettingViewModel()
        {
            model = new();

            ViewWidth = model.ViewWidth.ToReactiveProperty().AddTo(disposable);
            ViewHeight = model.ViewHeight.ToReactiveProperty().AddTo(disposable);
            ViewX = model.ViewX.ToReactiveProperty().AddTo(disposable);
            ViewY = model.ViewY.ToReactiveProperty().AddTo(disposable);

            ViewRectInfo = 
                ViewWidth.CombineLatest(ViewHeight, ViewX, ViewY, (w, h, x, y) =>  $"W:{w}, H:{h}, X:{x}, Y{y}")
                .ToReadOnlyReactiveProperty<string>().AddTo(disposable);

            Name = model.Name.ToReactiveProperty<string>().AddTo(disposable);

            IsGod = Name.Select(name => name == "ぽんた").ToReadOnlyReactiveProperty().AddTo(disposable);
            Rank = IsGod.Select(isGod => isGod ? "神" : "✨🤪✨").ToReadOnlyReactiveProperty<string>().AddTo(disposable);
            FontSize = IsGod.Select(isGod => isGod ? 80: 50).ToReadOnlyReactiveProperty().AddTo(disposable);

            SaveCommand = IsGod.ToReactiveCommand().WithSubscribe(model.SaveSettings).AddTo(disposable);
        }
    }
}
