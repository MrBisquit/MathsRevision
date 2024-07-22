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
    /// Interaction logic for Rectangles.xaml
    /// </summary>
    public partial class Rectangles : Page
    {
        public Rectangles()
        {
            InitializeComponent();
        }

        private void Nums_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (W == null || H == null || P == null || A == null || OW == null || OH == null || OP == null || OA == null) { return; } // Some stupid issue

            Debug.WriteLine("[Rectangles] Number changed, running");

            Evaluate();
        }

        float _Width = 0;
        float _Height = 0;
        float _Perimeter = 0;
        float _Area = 0;

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
                _Width = float.Parse(W.Text);
                _Height = float.Parse(H.Text);
                _Perimeter = float.Parse(P.Text);
                _Area = float.Parse(A.Text);

                if(_Area != 0 || _Perimeter != 0)
                {
                    if (_Width == 0)
                    {
                        op = 'w';
                    }
                    else if (_Height == 0)
                    {
                        op = 'h';
                    }
                } else
                {
                    op = 'a';
                }

                Debug.WriteLine("Parsed inputs");
            }
            catch { Debug.WriteLine("Failed to parse inputs"); cancel = true; }

            if (cancel)
            {
                Debug.Unindent();
                return;
            }

            Debug.WriteLine("Preforming calculations...");

            switch(op)
            {
                case 'a':
                    _Area = _Width * _Height;
                    _Perimeter = (_Width * _Width) + (_Height + _Height);
                    break;
                case 'w':
                    if(_Area != 0)
                    {
                        _Width = _Area / _Height;
                    } else
                    {
                        _Width = (_Perimeter / 2) - _Height;
                    }
                    break;
                case 'h':
                    if(_Area != 0)
                    {
                        _Height = _Area / _Width;
                    } else
                    {
                        _Height = (_Perimeter / 2) - _Width;
                    }
                    break;
            }

            Debug.WriteLine("Updating outputs");

            OW.Text = _Width.ToString();
            OH.Text = _Height.ToString();
            OP.Text = _Perimeter.ToString();
            OA.Text = _Area.ToString();

            Debug.WriteLine("Completed successfully");
            Debug.Unindent();

            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Debug.IndentLevel = 0;

            Debug.WriteLine("[Triangles] Updating canvas");
            Debug.Indent();

            Debug.WriteLine("Deleting all children");
            Draw.Children.Clear();

            var triangle = new Polygon
            {
                //Points = new PointCollection { new Point(0, 0), new Point(_A, 0), new Point(_B, _C) },
                Points = new PointCollection { new Point(0, 0), new Point(_Width, 0), new Point(_Width, _Height), new Point(0, _Height) },
                Stroke = Brushes.Black,
                Fill = new SolidColorBrush(Color.FromArgb(70, 127, 127, 127)), //Brushes.Black
                StrokeThickness = 0.1
            };
            double scale = 10.0; //2.0;
            triangle.RenderTransform = new ScaleTransform(scale, scale, 4, 5);

            Draw.Children.Add(triangle);

            Debug.WriteLine("Completed successfully");
            Debug.Unindent();
        }

        private void Randomise_Click(object sender, RoutedEventArgs e)
        {
            Debug.IndentLevel = 0;
            Debug.WriteLine("[Rectangles] Randomise button pressed");
            Debug.Indent();
            W.Text = "0";
            H.Text = "0";
            P.Text = "0";
            A.Text = "0";

            Random r = new Random();
            char op = r.Next(0, 3) == 0 ? 'a' : 'b';

            switch (op)
            {
                case 'a':
                    //A.Text = r.Next(1, 5).ToString();
                    P.Text = r.Next(1, 100).ToString();
                    break;
                case 'b':
                    W.Text = r.Next(1, 15).ToString();
                    H.Text = r.Next(1, 15).ToString();
                    break;
            }

            Debug.WriteLine("Completed successfully");
            Debug.Unindent();
        }
    }
}
