using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace Music8
{


    [DataContract]
    public class RandomData
    {
        [DataMember(Name = "Max")]
        public double Max { get; set; }
        [DataMember(Name = "Min")]
        public double Min { get; set; }
    }

    [DataContract]
    public class To
    {
        [DataMember(Name = "Random")]
        public bool Random { get; set; }
        [DataMember(Name = "Value")]
        public double Value { get; set; }
        [DataMember(Name = "RandomData")]
        public RandomData RandomData { get; set; }
    }
    [DataContract]
    public class From
    {

        [DataMember(Name = "Random")]
        public bool Random { get; set; }
        [DataMember(Name = "Value")]
        public double Value { get; set; }
        [DataMember(Name = "RandomData")]
        public RandomData RandomData { get; set; }
    }

    [DataContract]
    public class ShapeTransform
    {
        [DataMember(Name = "Name")]
        public String Name { get; set; }

        [DataMember(Name = "Enabled")]
        public bool Enabled { get; set; }

        [DataMember(Name = "To")]
        public To To { get; set; }

        [DataMember(Name = "From")]
        public From From { get; set; }
    }

    [DataContract]
    public class ShapeTransformTime
    {
        [DataMember(Name = "Name")]
        public String Name { get; set; }

        [DataMember(Name = "Time")]
        public double Time { get; set; }

        [DataMember(Name = "Random")]
        public bool Random { get; set; }

        [DataMember(Name = "RandomData")]
        public RandomData RandomData { get; set; }
    }

    [DataContract]
    public class ShapeDefinition
    {
        [DataMember(Name = "Name")]
        public String Name { get; set; }

        [DataMember(Name = "ShapeCount")]
        public int ShapeCount { get; set; }

        [DataMember(Name = "OpacityStart")]
        public double OpacityStart { get; set; }

        [DataMember(Name = "OpacityEnd")]
        public double OpacityEnd { get; set; }

        [DataMember(Name = "Spawn")]
        public string SpawnPosition { get; set; }

        [DataMember(Name = "RandomizedDefinedSpawn")]
        public bool RandomizeDefinedSpawn { get; set; }

        [DataMember(Name = "RenderOrigin")]
        public Point RenderOrigin { get; set; }

        [DataMember(Name = "Times")]
        public List<ShapeTransformTime> Times { get; set; }

        [DataMember(Name = "Points")]
        public List<Point> Points { get; set; }

        [DataMember(Name = "Transforms")]
        public List<ShapeTransform> Transforms { get; set; }

        [DataMember(Name = "SpawnPoints")]
        public List<int> SpawnPoints { get; set; }
    }

    public class ContainerShape
    {
        public Path Path { get; set; }
        public Storyboard Animation { get; set; }
    }

}

