using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MathsRevision
{
    /// <summary>
    /// Interaction logic for Circles.xaml
    /// </summary>
    public partial class Circles : Page
    {
        public Circles()
        {
            InitializeComponent();
        }
        private void Nums_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (R == null || D == null || A == null || P == null || OR == null || OD == null || OA == null || OP == null) { return; } // Some stupid issue

            Debug.WriteLine("[Circles] Number changed, running");

            Evaluate();
        }

        float _Radius = 0;
        float _Diameter = 0;
        float _Area = 0;
        float _Perimeter = 0;

        char? op = null;

        private void Evaluate()
        {
            Debug.IndentLevel = 0;
            bool cancel = false;

            Debug.WriteLine("[Rectangles] Evaluating");
            Debug.Indent();
            Debug.WriteLine("Attempting to parse inputs...");
            try
            {
                _Radius = float.Parse(R.Text);
                _Diameter = float.Parse(D.Text);
                _Area = float.Parse(A.Text);
                _Perimeter = float.Parse(P.Text);

                if(_Radius == 0)
                {
                    op = 'r';
                } else if(_Diameter == 0)
                {
                    op = 'd';
                } else if(_Area == 0)
                {
                    op = 'a';
                } else if(_Perimeter == 0)
                {
                    op = 'p';
                }
            }
            catch { Debug.WriteLine("Failed to parse inputs"); cancel = true; }

            if (cancel)
            {
                Debug.Unindent();
                return;
            }

            Debug.WriteLine("Doing calculations...");

            switch(op)
            {
                case 'r':
                    _Radius = _Diameter / 2;
                    break;
                case 'd':
                    _Diameter = _Radius * 2;
                    break;
                case 'a':
                    _Area = (float)(Math.PI * (_Radius * _Radius));
                    break;
                case 'p':
                    _Perimeter = (float)((Math.PI * (_Radius * _Radius)));
                    break;
            }

            Debug.WriteLine("Updating outputs");

            OR.Text = _Radius.ToString();
            OD.Text = _Diameter.ToString();
            OA.Text = _Area.ToString();
            OP.Text = _Perimeter.ToString();

            Debug.WriteLine("Completed successfully");
            Debug.Unindent();

            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Debug.IndentLevel = 0;

            Debug.WriteLine("[Circles] Updating canvas");
            Debug.Indent();

            Debug.WriteLine("Deleting all children");
            Draw.Children.Clear();

            Ellipse circle = new Ellipse
            {
                Width = _Radius * 2,
                Height = _Radius * 2,
                Stroke = Brushes.Black,
                Fill = new SolidColorBrush(Color.FromArgb(70, 127, 127, 127)), //Brushes.Black
                StrokeThickness = 0.1
            };
            double scale = 10.0; //2.0;
            circle.RenderTransform = new ScaleTransform(scale, scale, 4, 5);

            Draw.Children.Add(circle);

            Debug.WriteLine("Completed successfully");
            Debug.Unindent();
        }

        private void Randomise_Click(object sender, RoutedEventArgs e)
        {
            Debug.IndentLevel = 0;
            Debug.WriteLine("[Circles] Randomise button pressed");
            Debug.Indent();
            R.Text = "0";
            D.Text = "0";
            A.Text = "0";
            P.Text = "0";

            Random r = new Random();
            char op = r.Next(0, 3) == 0 ? 'a' : r.Next(0, 3) == 1 ? 'b' : 'c';

            switch(op)
            {
                case 'a':
                    A.Text = r.Next(0, 15).ToString();
                    P.Text = (Math.Sqrt(_Area * Math.PI) * 2).ToString();
                    break;
                case 'b':
                    R.Text = r.Next(0, 15).ToString();
                    D.Text = (_Radius * 2).ToString();
                    break;
                case 'c':
                    R.Text = r.Next(0, 15).ToString();
                    D.Text = (_Radius * 2).ToString();
                    break;
            }

            Debug.WriteLine("Completed successfully");
            Debug.Unindent();
        }
    }
}
