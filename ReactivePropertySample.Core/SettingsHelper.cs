using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ReactivePropertySample.Core
{
    internal static class SettingsHelper
    {
        public static T Load<T>(string? path = null) where T : new()
        {
            path ??= GetDefaultPath();
            using var xr = XmlReader.Create(path);
            return (T)new XmlSerializer(typeof(T)).Deserialize(xr) ?? throw new InvalidOperationException();
        }

        public static T LoadOrDefault<T>(string? path = null) where T : new()
        {
            try
            {
                return Load<T>(path);
            }
            catch
            {
                Debug.WriteLine("fail Deserialize");
                return new T();
            }
        }

        public static void Save<T>(T obj, string? path = null) where T : new()
        {
            path ??= GetDefaultPath();
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            using var sw = new StreamWriter(path, false, Encoding.UTF8);
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            new XmlSerializer(typeof(T)).Serialize(sw, obj, ns);
        }

        public static string GetDefaultPath()
        {
            var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
            return Path.Combine(dir!, "user.config");
        }
    }
}
