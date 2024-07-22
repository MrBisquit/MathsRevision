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
    /// Interaction logic for Triangles.xaml
    /// </summary>
    public partial class Triangles : Page
    {
        public Triangles()
        {
            InitializeComponent();
        }

        private void Nums_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (A == null || B == null || C == null || OA == null || OB == null || OC == null) { return; } // Some stupid issue

            Debug.WriteLine("[Triangles] Number changed, running");

            Evaluate();
        }

        float _A = 0;
        float _B = 0;
        float _C = 0;
        float _Width = 0;
        float _Height = 0;
        float _Perimeter = 0;
        float _Area = 0;

        char? op = null;

        private void Evaluate()
        {
            Debug.IndentLevel = 0;
            bool cancel = false;

            Debug.WriteLine("[Triangles] Evaluating");
            Debug.Indent();
            Debug.WriteLine("Attempting to parse inputs...");

            try
            {
                _A = float.Parse(A.Text);
                _B = float.Parse(B.Text);
                _C = float.Parse(C.Text);
                _Width = float.Parse(W.Text);
                _Height = float.Parse(H.Text);
                _Perimeter = float.Parse(P.Text);
                _Area = float.Parse(Ar.Text);

                if (_A == 0 && _B == 0 && _C == 0)
                {
                    op = 'a';
                }
                else if (_Width == 0 && _Height == 0)
                {
                    op = 'b';
                }
                else if (_Perimeter == 0 && _Area == 0)
                {
                    op = 'c';
                }

                Debug.WriteLine("Parsed inputs");
            } catch { Debug.WriteLine("Failed to parse inputs"); cancel = true; }

            if (cancel)
            {
                Debug.Unindent();
                return;
            }

            Debug.WriteLine("Working out values");
            switch (op)
            {
                case 'a':
                    _A = _Width;
                    _B = _Height;
                    _C = (float)Math.Sqrt((_Width *  _Width) + (_Height * _Height));
                    break;
                case 'b':
                    _Width = _A;
                    _Height = _B;
                    break;
                case 'c':
                    _Perimeter = _A + _B + _C;
                    _Area = (_Width * _Height) / 2;
                    break;
            }

            Debug.WriteLine("Updating outputs");

            OA.Text = _A.ToString();
            OB.Text = _B.ToString();
            OC.Text = _C.ToString();
            OW.Text = _Width.ToString();
            OH.Text = _Height.ToString();
            OP.Text = _Perimeter.ToString();
            OAr.Text = _Area.ToString();

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
                Points = new PointCollection { new Point(_A, 0), new Point(0, 0), new Point(0, _B * -1) },
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

        private void FFO_Click(object sender, RoutedEventArgs e)
        {
            A.Text = OA.Text;
            B.Text = OB.Text;
            C.Text = OC.Text;
            W.Text = OW.Text;
            H.Text = OH.Text;
            P.Text = OP.Text;
            Ar.Text = OAr.Text;
        }

        private void Randomise_Click(object sender, RoutedEventArgs e)
        {
            Debug.IndentLevel = 0;
            Debug.WriteLine("[Triangles] Randomise button pressed");
            Debug.Indent();
            A.Text = "0";
            B.Text = "0";
            C.Text = "0";
            W.Text = "0";
            H.Text = "0";
            P.Text = "0";
            Ar.Text = "0";

            Random r = new Random();
            char op = r.Next(0, 3) == 0 ? 'a' : 'b';

            switch (op)
            {
                case 'a':
                    A.Text = r.Next(1, 15).ToString();
                    B.Text = r.Next(1, 15).ToString();
                    C.Text = r.Next(1, 15).ToString();
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
