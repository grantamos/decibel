using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;


namespace Music8
{
    public class ShapeManager
    {
        Random random;
        DispatcherTimer timer;

        public const int SCALE_TIME_INDEX = 0,
                        FADE_TIME_INDEX = 1;

        Dictionary<int, String> ShapeDefinitionsJSON;
        Dictionary<int, List<ShapeDefinition>> ShapeDefinitions;
        List<ContainerShape> Shapes;
        SHAPE currentShape;
        int currentAnin;

        public enum SHAPE : int
        {
            TRIANGLE = 0
        };

        public Grid Container
        {
            get;
            set;
        }

        public Point ContainerSize
        {
            get;
            set;
        }

        public ShapeManager()
        {
            random = new Random(DateTime.Now.Millisecond);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2.5);
            timer.Tick += timer_Tick;

            ShapeDefinitionsJSON = new Dictionary<int, String>()
            {
                {(int)SHAPE.TRIANGLE, @"[{""Name"":""Triangles0"",""Description"":""Shoots triangles from top of screen"",""ShapeCount"":5,""OpacityStart"":0,""OpacityEnd"":0.03,""Spawn"":""Defined"",""RandomizedDefinedSpawn"":false,""RenderOrigin"":{""X"":0.5,""Y"":0.5},""Times"":[{""Name"":""ScaleTime"",""Time"":7,""Random"":false,""RandomData"":{""Min"":2,""Max"":6}},{""Name"":""FadeTime"",""Time"":2,""Random"":false,""RandomData"":{}}],""Points"":[{""X"":1366,""Y"":768},{""X"":0,""Y"":768},{""X"":682,""Y"":0}],""Transforms"":[{""Name"":""ScaleTransform"",""Enabled"":true,""To"":{""Value"":1,""Random"":true,""RandomData"":{""Min"":0.5,""Max"":1}},""From"":{""Value"":0.25,""Random"":true,""RandomData"":{""Min"":0.25,""Max"":0.35}}},{""Name"":""ReflectVertically"",""Enabled"":true,""To"":{},""From"":{}}],""SpawnPoints"":[11,11,12,12,13,13]},{""Name"":""Triangles1"",""Description"":""Spawns random triangles at random positions"",""ShapeCount"":3,""OpacityStart"":0,""OpacityEnd"":0.3,""Spawn"":""Random"",""RandomizedDefinedSpawn"":true,""RenderOrigin"":{""X"":0.5,""Y"":0.5},""Times"":[{""Name"":""ScaleTime"",""Time"":7,""Random"":false,""RandomData"":{""Min"":6,""Max"":2}},{""Name"":""FadeTime"",""Time"":2,""Random"":false,""RandomData"":{}}],""Points"":[{""X"":1366,""Y"":768},{""X"":0,""Y"":768},{""X"":682,""Y"":0}],""Transforms"":[{""Name"":""ScaleTransform"",""Enabled"":true,""To"":{""Value"":1,""Random"":false,""RandomData"":{""Min"":0.5,""Max"":1}},""From"":{""Value"":0.25,""Random"":false,""RandomData"":{""Min"":0.25,""Max"":0.35}}},{""Name"":""ReflectVertically"",""Enabled"":true,""To"":{},""From"":{}}],""SpawnPoints"":[0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24]},{""Name"":""Triangles2"",""Description"":""Spawns random triangles at random positions"",""ShapeCount"":3,""OpacityStart"":0,""OpacityEnd"":0.3,""Spawn"":""Defined"",""RandomizedDefinedSpawn"":true,""RenderOrigin"":{""X"":0.5,""Y"":0.5},""Times"":[{""Name"":""ScaleTime"",""Time"":7,""Random"":false,""RandomData"":{""Min"":6,""Max"":2}},{""Name"":""FadeTime"",""Time"":2,""Random"":false,""RandomData"":{}}],""Points"":[{""X"":1366,""Y"":768},{""X"":0,""Y"":768},{""X"":682,""Y"":0}],""Transforms"":[{""Name"":""ScaleTransform"",""Enabled"":true,""To"":{""Value"":1,""Random"":false,""RandomData"":{""Min"":0.5,""Max"":1}},""From"":{""Value"":0.25,""Random"":false,""RandomData"":{""Min"":0.25,""Max"":0.35}}},{""Name"":""ReflectVertically"",""Enabled"":true,""To"":{},""From"":{}}],""SpawnPoints"":[0,1,2,3,4,5,9,10,14,15,19,20,21,22,23,24]},{""Name"":""Triangles3"",""Description"":""Spawns random triangles at random positions"",""ShapeCount"":3,""OpacityStart"":0,""OpacityEnd"":0.3,""Spawn"":""Defined"",""RandomizedDefinedSpawn"":false,""RenderOrigin"":{""X"":0.5,""Y"":0.5},""Times"":[{""Name"":""ScaleTime"",""Time"":7,""Random"":false,""RandomData"":{""Min"":6,""Max"":2}},{""Name"":""FadeTime"",""Time"":2,""Random"":false,""RandomData"":{}}],""Points"":[{""X"":1366,""Y"":768},{""X"":0,""Y"":768},{""X"":682,""Y"":0}],""Transforms"":[{""Name"":""ScaleTransform"",""Enabled"":true,""To"":{""Value"":1,""Random"":false,""RandomData"":{""Min"":0.5,""Max"":1}},""From"":{""Value"":0.25,""Random"":false,""RandomData"":{""Min"":0.25,""Max"":0.35}}},{""Name"":""ReflectVertically"",""Enabled"":true,""To"":{},""From"":{}}],""SpawnPoints"":[5,6,7,8,9]},{""Name"":""Triangles3"",""Description"":""Spawns random triangles at random positions"",""ShapeCount"":3,""OpacityStart"":0,""OpacityEnd"":0.3,""Spawn"":""Defined"",""RandomizedDefinedSpawn"":true,""RenderOrigin"":{""X"":0.5,""Y"":0.5},""Times"":[{""Name"":""ScaleTime"",""Time"":7,""Random"":false,""RandomData"":{""Min"":6,""Max"":2}},{""Name"":""FadeTime"",""Time"":2,""Random"":false,""RandomData"":{}}],""Points"":[{""X"":1366,""Y"":768},{""X"":0,""Y"":768},{""X"":682,""Y"":0}],""Transforms"":[{""Name"":""ScaleTransform"",""Enabled"":true,""To"":{""Value"":1,""Random"":false,""RandomData"":{""Min"":0.5,""Max"":1}},""From"":{""Value"":0.25,""Random"":false,""RandomData"":{""Min"":0.25,""Max"":0.35}}},{""Name"":""ReflectVertically"",""Enabled"":true,""To"":{},""From"":{}}],""SpawnPoints"":[1,6,11,16,21,3,8,13,18,23]}]"}
            };

            ShapeDefinitions = new Dictionary<int, List<ShapeDefinition>>()
            {
                {(int)SHAPE.TRIANGLE, Byteopia.Helpers.JSON.DeserializeObject<List<ShapeDefinition>>( ShapeDefinitionsJSON[(int)SHAPE.TRIANGLE ]) }
            };

            currentShape = SHAPE.TRIANGLE;
            currentAnin = 0;
        }

        public void Load()
        {
            BuildContainerShapeFromJSON(ShapeDefinitions[(int)currentShape][currentAnin]);
        }

        int x = 0;
    
         void timer_Tick(object sender, object e)
         {
             if (x == Shapes.Count - 1)
             {
                 x = 0;
                 currentAnin++;
                 BuildContainerShapeFromJSON(ShapeDefinitions[(int)currentShape][currentAnin]);
             }

             Container.Children.Add(Shapes[x].Path);
             Shapes[x].Animation.Begin();
             x++;
         }
        

        /// <summary>
        /// Takes JSON definition of a shape container and builds something we use better
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public void BuildContainerShapeFromJSON(ShapeDefinition shapeDef)
        {
            if (Shapes == null)
                Shapes = new List<ContainerShape>();

            Shapes.Clear();

            if (shapeDef != null)
            {
                //Each shape def has one or more shapes
                for (int shapeNum = 0; shapeNum < shapeDef.ShapeCount; shapeNum++)
                {
                    #region Build Path
                    Path path = new Path();
                    PathSegmentCollection segs = new PathSegmentCollection();

                    int pointIterator = 0;
                    foreach (Point point in shapeDef.Points)
                    {
                        if (pointIterator++ == 0)
                            continue;

                        segs.Add(new LineSegment()
                        {
                            Point = point
                        });
                    }

                    PathFigure figure = new PathFigure()
                    {
                        StartPoint = shapeDef.Points[0],
                        Segments = segs,
                        IsFilled = true,
                        IsClosed = true
                    };

                    PathFigureCollection col = new PathFigureCollection();
                    col.Add(figure);

                    PathGeometry geo = new PathGeometry()
                    {
                        Figures = col
                    };

                    path.Data = geo;

                    BitmapCache cache = new BitmapCache();

                    path.CacheMode = cache;

                    CompositeTransform trans = new CompositeTransform();
                    trans.ScaleY = 1;
                    trans.ScaleX = 1;
                    path.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0xcf, 0xb9, 0xfa));
                    path.HorizontalAlignment = HorizontalAlignment.Left;
                    path.VerticalAlignment = VerticalAlignment.Top;
                    path.Opacity = shapeDef.OpacityStart;
                    path.RenderTransformOrigin = shapeDef.RenderOrigin;
                    path.RenderTransform = trans;

                    #endregion

                    #region Build Animation
                    Storyboard storyboard = new Storyboard();


                    double scaleFromX = 1.0;
                    double scaleFromY = 1.0;
                    double scaleToX = 1.0;
                    double scaleToY = 1.0;
                    bool reflectVertically = false;
                    bool reflectHorizontally = false;
                    foreach (ShapeTransform transform in shapeDef.Transforms)
                    {
                        switch (transform.Name)
                        {
                            case "ScaleTransform":

                                if (transform.Enabled == false)
                                    continue;

                                double scaleFrom = transform.From.Value;
                                if (transform.From.Random)
                                {
                                    if (transform.From.RandomData != null)
                                    {
                                        scaleFrom = GetRandom(transform.From.RandomData);
                                    }
                                }

                                double scaleTo = transform.To.Value;
                                if (transform.To.Random)
                                {
                                    if (transform.To.RandomData != null)
                                    {
                                        scaleTo = GetRandom(transform.To.RandomData);
                                    }
                                }

                                scaleFromX = scaleFromY = scaleFrom;
                                scaleToX = scaleToY = scaleTo;

                                (path.RenderTransform as CompositeTransform).ScaleX = scaleFrom;
                                (path.RenderTransform as CompositeTransform).ScaleY = scaleFrom;
                                break;
                            case "ReflectVertically":
                                if (transform.Enabled == false)
                                    continue;

                                reflectVertically = true;
                                break;
                            case "ReflectHorizontally":
                                if (transform.Enabled == false)
                                    continue;

                                reflectHorizontally = true;
                                break;
                            case "Rotate":
                                (path.RenderTransform as CompositeTransform).Rotation = transform.To.Value;    
                            break;
                        }
                    }

                    if (reflectVertically)
                    {
                        (path.RenderTransform as CompositeTransform).ScaleY *= -1;
                        scaleToY *= -1;
                    }

                    if (reflectHorizontally)
                    {
                        (path.RenderTransform as CompositeTransform).ScaleX *= -1;
                        scaleToX *= -1;
                    }

                    TimeSpan scaleTime = TimeSpan.FromSeconds(shapeDef.Times[SCALE_TIME_INDEX].Time);
                    TimeSpan fadeTime = TimeSpan.FromSeconds(shapeDef.Times[FADE_TIME_INDEX].Time);

                    if (shapeDef.Times[SCALE_TIME_INDEX].Random)
                    {
                        if (shapeDef.Times[SCALE_TIME_INDEX].RandomData != null)
                        {
                            scaleTime = TimeSpan.FromSeconds(GetRandom(shapeDef.Times[SCALE_TIME_INDEX].RandomData));
                        }
                    }

                    if (shapeDef.Times[FADE_TIME_INDEX].Random)
                    {
                        if (shapeDef.Times[FADE_TIME_INDEX].RandomData != null)
                        {
                            fadeTime = TimeSpan.FromSeconds(GetRandom(shapeDef.Times[FADE_TIME_INDEX].RandomData));
                        }
                    }

                    DoubleAnimationUsingKeyFrames growAnimationX = new DoubleAnimationUsingKeyFrames();
                    SplineDoubleKeyFrame growXFrame = new SplineDoubleKeyFrame()
                    {

                        KeyTime = scaleTime,
                        Value = scaleToX
                    };

                    growAnimationX.KeyFrames.Add(growXFrame);
                    storyboard.Children.Add(growAnimationX);

                    DoubleAnimationUsingKeyFrames growAnimationY = new DoubleAnimationUsingKeyFrames();
                    SplineDoubleKeyFrame growYFrame = new SplineDoubleKeyFrame()
                    {
                        KeyTime = scaleTime,
                        Value = scaleToY
                    };

                    growAnimationY.KeyFrames.Add(growYFrame);

                    storyboard.Children.Add(growAnimationY);

                    DoubleAnimationUsingKeyFrames opacity = new DoubleAnimationUsingKeyFrames();
                    SplineDoubleKeyFrame opacityFadeIn = new SplineDoubleKeyFrame()
                    {
                        Value = shapeDef.OpacityEnd,
                        KeyTime = fadeTime
                    };
                    opacity.KeyFrames.Add(opacityFadeIn);
                    SplineDoubleKeyFrame opacityFadeOut = new SplineDoubleKeyFrame()
                    {
                        Value = shapeDef.OpacityStart,
                        KeyTime = scaleTime
                    };
                    opacity.KeyFrames.Add(opacityFadeOut);

                    storyboard.Children.Add(opacity);

                    Storyboard.SetTargetProperty(growAnimationX, new PropertyPath("ScaleX").Path);
                    Storyboard.SetTargetProperty(growAnimationY, new PropertyPath("ScaleY").Path);
                    Storyboard.SetTargetProperty(opacity, new PropertyPath("Opacity").Path);

                    Storyboard.SetTarget(opacity, path);
                    Storyboard.SetTarget(growAnimationX, path.RenderTransform);
                    Storyboard.SetTarget(growAnimationY, path.RenderTransform);


                    storyboard.Duration = scaleTime;
                    storyboard.BeginTime = TimeSpan.Zero;


                    #endregion

                    #region Path Location
                    Point pathLoc = new Point(0, 0);
                    switch (shapeDef.SpawnPosition)
                    {
                        case "Random":
                            pathLoc = GetRandomSpawn2();
                        break;
                        case "Defined":
                            {
                                Point spawnPoint = new Point(0, 0);
                                int spawnIndex = shapeDef.SpawnPoints[shapeNum];
                                if (shapeDef.RandomizeDefinedSpawn)
                                {
                                    spawnPoint = GetRandomSpawn2();
                                }
                                
                                pathLoc = SpawnIndexToScreenPoint(spawnIndex);
                            }
                        break;
                    }

                    pathLoc.X -= shapeDef.Points[0].X / 2;
                    pathLoc.Y -= shapeDef.Points[1].Y / 2;
                    path.Margin = new Thickness(pathLoc.X, pathLoc.Y, 0, 0);
                    #endregion

                    #region Add
                    Shapes.Add(new ContainerShape()
                    {
                        Path = path,
                        Animation = storyboard
                    });
                    #endregion
                }
            }

            timer.Start();
        }

        public Point SpawnIndexToScreenPoint(int index)
        {
            List<Point> points = GetSpawnPoints();
            return points[index];
        }

        public Point GetRandomSpawn2()
        {
            List<Point> points = GetSpawnPoints();
            return points[random.Next(0, points.Count)];
        }

        public List<Point> GetSpawnPoints()
        {
            List<Point> points = new List<Point>();
            int rows = 4, cols = 4;

            double rowSize = ContainerSize.X / (double)rows;
            double colSize = ContainerSize.Y / (double)cols;

            double x = 0, y = 0;
            for (int i = 0; i <= rows; i++)
            {
                x = 0;
                for (int j = 0; j <= cols; j++)
                {
                    points.Add(new Point(x, y));
                    x += colSize;
                }

                y += rowSize;

            }

            return points;
        }

        public double GetRandom(RandomData rand)
        {
            return random.NextDouble() * (rand.Max - rand.Min) + rand.Min;
        }
    }
}
