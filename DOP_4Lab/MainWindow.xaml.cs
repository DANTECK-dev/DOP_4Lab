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

namespace DOP_4Lab
{
    public enum Block_Type
    {
        straight,   // прямая
        turn,       // поворот
        fork        // развилка
    }
    public enum Rotate
    {
        deg0,
        deg90,
        deg180,
        deg270,
        deg360
    }
    class Cell
    {
        Block? block = null;
        int x;
        int y;

        public Cell(Block? block, int x, int y)
        {
            this.block = block;
            this.x = x;
            this.y = y;
        }
    }
    class Block
    {
        Block_Type type;
        Polygon polygon;
        Rotate rotate;

        public Block(Block_Type type, Rotate rotate)
        {
            this.type = type;
            this.rotate = rotate;
        }
    }
    public partial class MainWindow : Window
    {
        bool GameIsStarted = false;
        Polygon rect;
        Point Center = new Point(25, 25);
        public MainWindow()
        {
            InitializeComponent();
            rect = new Polygon();
            rect.Points = new PointCollection()
            {
                new Point(0, 20),
                new Point(30, 20),
                new Point(30, 50),
                new Point(20, 50),
                new Point(20, 30),
                new Point(0, 30)
            };
            rect.Fill = new SolidColorBrush(Colors.Black);
            Player_Field.Children.Add(rect);
            Grid.SetRow(rect, 1);
            Grid.SetColumn(rect, 0);

            Polygon polygon = Rotate(rect, 45);
            polygon.Fill = new SolidColorBrush(Colors.Black);
            Player_Field.Children.Add(polygon);
            Grid.SetRow(polygon, 2);
            Grid.SetColumn(polygon, 2);
        }
        private Polygon Rotate(Polygon polygon, double angle)
        {
            angle = angle * Math.PI / 180;
            Point[] points = new Point[polygon.Points.Count];
            for (int j = 0; j < polygon.Points.Count; j++)
            {
                double x = (double)((polygon.Points[j].X - Center.X) * Math.Cos(angle) - (polygon.Points[j].Y - Center.Y) * Math.Sin(angle) + Center.X);
                double y = (double)((polygon.Points[j].X - Center.X) * Math.Sin(angle) + (polygon.Points[j].Y - Center.Y) * Math.Cos(angle) + Center.Y);
                points[j] = new Point(x, y);
            }
            Polygon polygon1 = new Polygon();
            polygon1.Points = new PointCollection(points);
            return polygon1;
        }

        private void Game_Started_Button_Click(object sender, RoutedEventArgs e)
        {
            rect = Rotate(rect, 90);
            rect.Fill = new SolidColorBrush(Colors.Black);
            Player_Field.Children.Add(rect);
            Grid.SetRow(rect, 2);
            Grid.SetColumn(rect, 3);
            //if (GameIsStarted)
            //{
            //    GameIsStarted = false;
            //    Game_Started_Button.IsEnabled = true;
            //    Game_Started_Button.Content = "Начать игру";
            //}
            //else
            //{
            //    GameIsStarted = true;
            //    Game_Started_Button.IsEnabled = false;
            //    Game_Started_Button.Content = "Остановить игру";
            //}
        }
    }
}
