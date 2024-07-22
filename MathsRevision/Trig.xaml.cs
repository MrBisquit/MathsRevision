using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for Trig.xaml
    /// </summary>
    public partial class Trig : Page
    {
        public Trig()
        {
            InitializeComponent();
        }

        float _H = 0;
        float _O = 0;
        float _A = 0;

        char? op = null;

        private void Nums_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(H == null || O == null || A == null || OH == null || OO == null || OA == null) { return; } // Some stupid issue

            Debug.WriteLine("[Trig] Number changed, running");

            Evaluate();
        }

        private void Evaluate()
        {
            Debug.IndentLevel = 0;
            bool cancel = false;

            Debug.WriteLine("[Trig] Evaluating");
            Debug.Indent();
            Debug.WriteLine("Attempting to parse inputs...");
            try
            {
                op = null;
                if (string.IsNullOrEmpty(H.Text) || H.Text == "0")
                {
                    _H = 0;
                    op = 't';
                }
                else _H = float.Parse(H.Text);
                if (string.IsNullOrEmpty(O.Text) || O.Text == "0")
                {
                    _O = 0;
                    op = 'c';
                }
                else _O = float.Parse(O.Text);
                if (string.IsNullOrEmpty(A.Text) || A.Text == "0")
                {
                    _A = 0;
                    op = 's';
                }
                else _A = float.Parse(A.Text);

                Debug.WriteLine("Parsed inputs");
            }
            catch { Debug.WriteLine("Failed to parse inputs"); cancel = true; }

            if(cancel)
            {
                Debug.Unindent();
                return;
            }

            Debug.WriteLine("Preforming calculations...");

            switch (op)
            {
                case 's':
                    MathsUsed.Text = $"Sin (x) = {_O}/{_H}";
                    _A = (float)Math.Sin(_O / _H);
                    break;
                case 'c':
                    MathsUsed.Text = $"Cos (x) = {_A}/{_H}";
                    _O = (float)Math.Tan(_A / _H);
                    break;
                case 't':
                    MathsUsed.Text = $"Tan (x) = {_O}/{_A}";
                    _H = (float)Math.Tan(_O / _A);
                    break;
            }

            Debug.WriteLine("Updating outputs");

            /*H.Text = _H.ToString();
            O.Text = _O.ToString();
            A.Text = _A.ToString();*/

            OH.Text = _H.ToString();
            OO.Text = _O.ToString();
            OA.Text = _A.ToString();

            Debug.WriteLine("Completed successfully");
            Debug.Unindent();

            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Debug.IndentLevel = 0;

            Debug.WriteLine("[Trig] Updating canvas");
            Debug.Indent();

            Debug.WriteLine("Deleting all children");
            Draw.Children.Clear();

            var triangle = new Polygon
            {
                Points = new PointCollection { new Point(0, 0), new Point(_A, 0), new Point(_O, _H) },
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
    }
}
