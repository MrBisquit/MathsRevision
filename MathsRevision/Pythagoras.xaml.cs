using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
    /// Interaction logic for Pythagoras.xaml
    /// </summary>
    public partial class Pythagoras : Page
    {
        public Pythagoras()
        {
            InitializeComponent();
        }

        float _A = 0;
        float _B = 0;
        float _C = 0;
        float Ans = 0;

        char? op = null;

        private void Nums_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (A == null || B == null || C == null || OA == null || OB == null || OC == null) { return; } // Some stupid issue

            Debug.WriteLine("[Pythagoras] Number changed, running");

            Evaluate();
        }

        private void Evaluate()
        {
            Debug.IndentLevel = 0;
            bool cancel = false;

            Debug.WriteLine("[Pythagoras] Evaluating");
            Debug.Indent();
            Debug.WriteLine("Attempting to parse inputs...");
            try
            {
                op = null;
                if (string.IsNullOrEmpty(A.Text) || A.Text == "0")
                {
                    _A = 0;
                    op = 'a';
                }
                else _A = float.Parse(A.Text);
                if (string.IsNullOrEmpty(B.Text) || B.Text == "0")
                {
                    _B = 0;
                    op = 'b';
                }
                else _B = float.Parse(B.Text);
                if (string.IsNullOrEmpty(C.Text) || C.Text == "0")
                {
                    _C = 0;
                    op = 'c';
                }
                else _C = float.Parse(C.Text);

                Debug.WriteLine("Parsed inputs");
            }
            catch { Debug.WriteLine("Failed to parse inputs"); cancel = true; }

            if (cancel)
            {
                Debug.Unindent();
                return;
            }

            Debug.WriteLine("Preforming calculations...");

            // Had to manually square them because it was complaining about floats or doubles
            switch (op)
            {
                case 'a':
                    _A = (float)Math.Sqrt((_B * _B) + (_C * _C));
                    Ans = _A;
                    break;
                case 'b':
                    _B = (float)Math.Sqrt((_A * _A) + (_C * _C));
                    Ans = _B;
                    break;
                case 'c':
                    _C = (float)Math.Sqrt((_A * _A) + (_B * _B));
                    Ans = _C;
                    break;
            }

            Debug.WriteLine("Updating outputs");

            OA.Text = _A.ToString();
            OB.Text = _B.ToString();
            OC.Text = _C.ToString();

            Maths.Text = "a² + b² = c²\n";
            switch(op)
            {
                case 'a':
                    Maths.Text += $"a² = {_B}² + {_C}²\n";
                    break;
                case 'b':
                    Maths.Text += $"b² = {_A}² + {_C}²\n";
                    break;
                case 'c':
                    Maths.Text += $"c² = {_A}² + {_B}²\n";
                    break;
            }
            Maths.Text += $"= {Ans}";

            Debug.WriteLine("Completed successfully");
            Debug.Unindent();

            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Debug.IndentLevel = 0;

            Debug.WriteLine("[Pythagoras] Updating canvas");
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

        private void Randomise_Click(object sender, RoutedEventArgs e)
        {
            Debug.IndentLevel = 0;
            Debug.WriteLine("[Pythagoras] Randomise button pressed");
            Debug.Indent();
            A.Text = "0";
            B.Text = "0";
            C.Text = "0";

            Random r = new Random();
            char op = r.Next(0, 3) == 0 ? 'A' : r.Next(0, 3) == 1 ? 'B' : 'C'; // Picks which one to leave out
            Debug.WriteLine($"Leaving {op} out");
            switch (op)
            {
                case 'A':
                    B.Text = r.Next(1, 15).ToString();
                    C.Text = r.Next(1, 15).ToString();
                    break;
                case 'B':
                    A.Text = r.Next(1, 15).ToString();
                    C.Text = r.Next(1, 15).ToString();
                    break;
                case 'C':
                    A.Text = r.Next(1, 15).ToString();
                    B.Text = r.Next(1, 15).ToString();
                    break;
            }
            Debug.WriteLine("Completed successfully");
            Debug.Unindent();
        }
    }
}
