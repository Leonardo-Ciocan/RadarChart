using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RadarChart
{
    /// <summary>
    /// Interaction logic for RadarChartControl.xaml
    /// </summary>
    public partial class RadarChartControl : UserControl
    {
        public RadarChartControl()
        {
            InitializeComponent();
            this.Loaded += (a, b) => DrawBackground(NumberOfCorners);
        }

        public int NumberOfCorners { get; set; }
        public double SectorAngle { get { return 360.0 / NumberOfCorners; } }
        public double Radius { get { return root.ActualHeight / 2; } }

        Random rnd = new Random();
        public int[] Values;
        public void DrawBackground(int n)
        {

            for(double ratio = 1;ratio>=0.2;ratio-=0.2){
                DrawRingAtRatio(ratio);
            }
            Values = new int[n];
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = rnd.Next(30,100);
            }
            DrawValues();
        }

        public void DrawRingAtRatio(double ratio)
        {
            for (int x = 0; x < NumberOfCorners; x++)
            {
                double angleA = SectorAngle * x;
                Point A = new Point(Radius + Radius * ratio * Math.Cos((angleA + 90) / 180.0 * 3.14),
                                    Radius - Radius * ratio * Math.Sin((angleA + 90) / 180.0 * 3.14));

                double angleB = SectorAngle * (x+1);
                Point B = new Point(Radius + Radius * ratio * Math.Cos((angleB + 90) / 180.0 * 3.14),
                                    Radius - Radius * ratio * Math.Sin((angleB + 90) / 180.0 * 3.14));

                Line l = new Line
                {
                    Stroke = new SolidColorBrush(Colors.Gray),
                    StrokeThickness = 2,
                    X1 = A.X,
                    X2 = B.X,
                    Y1 = A.Y,
                    Y2 = B.Y
                };
                root.Children.Add(l);

                if (ratio == 1)
                {
                    Line l2 = new Line
                    {
                        Stroke = new SolidColorBrush(Colors.Gray),
                        StrokeThickness = 2,
                        X1 = Radius,
                        X2 = A.X,
                        Y1 = Radius,
                        Y2 = A.Y
                    };
                    root.Children.Add(l2);
                }
            }
        }

        public void DrawValues()
        {
            for (int i = 0; i < Values.Length; i++)
            {
                int j = (i == Values.Length - 1) ? 0 : i + 1;
                double ratioA = Values[i] / 100.0;
                double ratioB = Values[j] / 100.0;

                double angleA = SectorAngle * i;
                Point A = new Point(Radius + Radius * ratioA * Math.Cos((angleA + 90) / 180.0 * 3.14),
                                    Radius - Radius * ratioA * Math.Sin((angleA + 90) / 180.0 * 3.14));

                double angleB = SectorAngle * (i + 1);
                Point B = new Point(Radius + Radius * ratioB * Math.Cos((angleB + 90) / 180.0 * 3.14),
                                    Radius - Radius * ratioB * Math.Sin((angleB + 90) / 180.0 * 3.14));

                ValuesShape.Points.Add(A);
                ValuesShape.Points.Add(B);
            }
        }
    }
}
