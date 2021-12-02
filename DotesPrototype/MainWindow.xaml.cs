using DotesPrototype.Models;
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

namespace DotesPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Coil coil=null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            int rowsDeep = Convert.ToInt32(RowsDeepTextBLock.Text);
            int rowsCount = Convert.ToInt32(RowsCountTextBlock.Text);
            bool offset = OffsetCheckbox.IsChecked.Value;
            int circuitCount = Convert.ToInt32(CircuitCountTextBlock.Text);
            coil = new Coil(offset, rowsDeep, rowsCount,circuitCount);
            if (coil.Error.HasError)
            {
                ErrorLabel.Visibility = Visibility.Visible;
                ErrorLabel.Content = coil.Error.Message;
            }
            else
            {
                DrawCanvas();
                PatternLabel.Content = "Circuits pattern:" + coil.Combination.ToString();
                ErrorLabel.Visibility = Visibility.Hidden;
            }
        }
        private void DrawCanvas()
        {
            int xStep = 20;
            int yStep = 20;
            int offset = 10;
            if (coil != null)
            {
                Canvas.Children.Clear();
                for (int i = 0; i < coil.TubeRows.Count; i++)
                {
                    if (coil.Offset&&i%2!=0)
                    {
                        foreach (Tube tube in coil.TubeRows[i])
                        {
                            Canvas.Children.Add(newEllipse(tube.Column * xStep + offset, tube.Row * yStep));
                        }
                    }
                    else
                    {
                        foreach (Tube tube in coil.TubeRows[i])
                        {
                            Canvas.Children.Add(newEllipse(tube.Column * xStep, tube.Row * yStep));
                        }
                    }
                }
            }
        }
        private Ellipse newEllipse(double x, double y)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            Ellipse el = new Ellipse();
            el.StrokeThickness = 2;
            el.Stroke = Brushes.Black;
            el.Fill = mySolidColorBrush;
            el.Width = 15;
            el.Height = 15;
            el.Margin = new Thickness(x, y, 0, 0);
            return el;
        }
    }
}
