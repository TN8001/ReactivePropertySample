using Reactive.Bindings;

namespace ReactivePropertySample.Core
{
    public class SettingModel
    {
        public ReactivePropertySlim<int> Width { get; }
        public ReactivePropertySlim<int> Height { get; }
        public ReactivePropertySlim<int> X { get; }
        public ReactivePropertySlim<int> Y { get; }
        public ReactivePropertySlim<string> Name { get; }


        public SettingModel() : this(SettingsHelper.LoadOrDefault<Settings>()) { }
        public SettingModel(Settings settings)
        {
            var ((x, y, w, h), n) = settings;
            (X, Y, Width, Height, Name) = (new(x), new(y), new(w), new(h), new(n));
        }

        public void Save()
        {
            var s = new Settings(new(X.Value, Y.Value, Width.Value, Height.Value), Name.Value);
            SettingsHelper.Save(s);
        }
    }
}
