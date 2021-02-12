using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactivePropertySample_M
{
    public class Settings
    {
        private static Settings? instance = new();
        public static Settings Instance
        {
            get
            {
                if(instance is null)
                {
                    Load();
                }
                return instance!;
            }
        }

        public Rectangle MainViewRect { get; set; } = new(0, 0, 800, 450);

        public string Name { get; set; } = "ぽんた";

        private Settings() { }

        /// <summary>
        /// 設定ファイルをデシリアライズしてインスタンスを生成
        /// </summary>
        /// <returns></returns>
        private static void Load()
        {
            // 今回は面倒なのでnew
            instance = new();
        }

        /// <summary>
        /// 設定ファイルをシリアライズして保存
        /// </summary>
        internal static void Save()
        {
            Debug.WriteLine("保存したよ");
        }
    }
}
