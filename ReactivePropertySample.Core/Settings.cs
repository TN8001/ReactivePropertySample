using System.Xml;
using System.Xml.Serialization;

namespace ReactivePropertySample.Core
{
    public record Rectangle([property: XmlAttribute] int X, [property: XmlAttribute] int Y, [property: XmlAttribute] int Width, [property: XmlAttribute] int Height)
    {
        public Rectangle() : this(0, 0, 0, 0) { }
    }

    public record Settings(Rectangle Rect, [property: XmlAttribute] string Name)
    {
        public Settings() : this(new(0, 0, 800, 450), "ぽんた") { }
    }
}
