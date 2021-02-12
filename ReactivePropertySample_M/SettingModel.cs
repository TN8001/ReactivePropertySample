using Reactive.Bindings;
using System;
using System.Threading.Tasks;

namespace ReactivePropertySample_M
{
    public class SettingModel
    {
        private Settings settings => Settings.Instance;

        public ReactivePropertySlim<int> ViewWidth { get; }

        public ReactivePropertySlim<int> ViewHeight { get; }

        public ReactivePropertySlim<int> ViewX { get; }

        public ReactivePropertySlim<int> ViewY { get; }

        public ReactivePropertySlim<string> Name { get; }

        public SettingModel()
        {
            ViewWidth = new ReactivePropertySlim<int>(settings.MainViewRect.Width);

            ViewHeight = new ReactivePropertySlim<int>(settings.MainViewRect.Height);

            ViewX = new ReactivePropertySlim<int>(settings.MainViewRect.X);

            ViewY = new ReactivePropertySlim<int>(settings.MainViewRect.Y);

            Name = new ReactivePropertySlim<string>(settings.Name);
        }

        /// <summary>
        /// 設定を保存する
        /// </summary>
        public void SaveSettings()
        {
            settings.MainViewRect = new(ViewX.Value, ViewY.Value, ViewWidth.Value, ViewHeight.Value);
            settings.Name = Name.Value;
            Settings.Save();
        }
    }
}
