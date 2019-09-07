using System;
using System.IO;
using System.Xml.Serialization;

namespace CodingExercise
{

    public class Point
    {
        public int X, Y;

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }

    public class Line
    {
        public Point Start, End;

        internal Line DeepCopy()
        {
            using (var stream = new MemoryStream())
            {
                var xmlSerializer = new XmlSerializer(typeof(Line));
                xmlSerializer.Serialize(stream, this);
                stream.Position = 0;
                return (Line)xmlSerializer.Deserialize(stream);
            }
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Point sp = new Point{X = 1,Y= 1};
            Point ep = new Point {X = 2, Y = 2};
            Line line = new Line{End = ep, Start = sp};

            Line line2 = line.DeepCopy();
            Console.WriteLine(line);
            Console.WriteLine(line2);
        }
    }
}
