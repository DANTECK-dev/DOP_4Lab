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
        public Block? block = null;
        public int x;
        public int y;

        public Cell(Block? block, int x, int y)
        {
            this.block = block;
            this.x = x;
            this.y = y;
        }
    }
    class Sides_Block
    {
        public Block? top_block = null;
        public Block? right_block = null;
        public Block? bottom_block = null;
        public Block? left_block = null;
    }
    class Block
    {
        public Rotate rotate;
        public Block_Type type;
        public Polygon polygon;
        public Sides_Block sides = new Sides_Block();

        public Block(Block_Type type, Polygon polygon, Rotate rotate)
        {
            this.type = type;
            this.rotate = rotate;
            this.polygon = polygon;
        }
        public Block(Block_Type type, Rotate rotate)
        {
            this.type = type;
            this.rotate = rotate;
            this.polygon = New_Block.Rotated_Polygon(type, rotate);
        }
    }
    static class New_Block
    {
        public static Random random = new Random();
        public static Point Center = new Point(25, 25);
        public static Polygon Rotated_Polygon(Block_Type block_Type, Rotate rotate)
        {
            Polygon polygon = new Polygon();
            Polygon R_polygon = new Polygon();
            if (rotate == Rotate.deg0)
            {
                polygon = NonRotate_Polygon(block_Type);
                R_polygon = polygon;
            }
            else if (rotate == Rotate.deg90)
            {
                polygon = NonRotate_Polygon(block_Type);
                R_polygon = Rotate_Polygon(polygon, 90);
            }
            else if (rotate == Rotate.deg180)
            {
                polygon = NonRotate_Polygon(block_Type);
                R_polygon = Rotate_Polygon(polygon, 180);
            }
            else if (rotate == Rotate.deg270)
            {
                polygon = NonRotate_Polygon(block_Type);
                R_polygon = Rotate_Polygon(polygon, 270);
            }
            else if (rotate == Rotate.deg360)
            {
                polygon = NonRotate_Polygon(block_Type);
                R_polygon = Rotate_Polygon(polygon, 360);
            }
            return R_polygon;
        }
        public static Polygon Rotate_Polygon(Polygon polygon, double angle)
        {
            angle = angle * Math.PI / 180;
            Point[] points = new Point[polygon.Points.Count];
            for (int j = 0; j < polygon.Points.Count; j++)
            {
                int x = (int)((polygon.Points[j].X - Center.X) * Math.Cos(angle) - (polygon.Points[j].Y - Center.Y) * Math.Sin(angle) + Center.X);
                int y = (int)((polygon.Points[j].X - Center.X) * Math.Sin(angle) + (polygon.Points[j].Y - Center.Y) * Math.Cos(angle) + Center.Y);
                points[j] = new Point(x, y);
            }
            Polygon R_polygon = new Polygon();
            R_polygon.Points = new PointCollection(points);
            return R_polygon;
        }
        public static Polygon NonRotate_Polygon(Block_Type block_Type)
        {
            Polygon polygon = new Polygon();
            if (block_Type == Block_Type.straight)
            {
                polygon.Points = new PointCollection()
                {
                    new Point(0, 20),
                    new Point(50, 20),
                    new Point(50, 30),
                    new Point(0, 30)
                };
            }
            else if (block_Type == Block_Type.turn)
            {
                polygon.Points = new PointCollection()
                {
                    new Point(0, 20),
                    new Point(30, 20),
                    new Point(30, 50),
                    new Point(20, 50),
                    new Point(20, 30),
                    new Point(0, 30)
                };
            }
            else if (block_Type == Block_Type.fork)
            {
                polygon.Points = new PointCollection()
                {
                    new Point(0, 20),
                    new Point(50, 20),
                    new Point(50, 30),
                    new Point(30, 30),
                    new Point(30, 50),
                    new Point(20, 50),
                    new Point(20, 30),
                    new Point(0, 30)
                };
            }
            polygon.Fill = new SolidColorBrush(Colors.Black);
            return polygon;
        }
        public static Block_Type Rand_Block_Type()
        {
            return (Block_Type)random.Next(0, 3);
        }
        public static Rotate Rand_Rotate()
        {
            return (Rotate)random.Next(0, 5);
        }
    }
    public partial class MainWindow : Window
    {
        bool GameIsStarted = false;
        Block[,] matrix_blocks = new Block[5, 5];
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Game_Started_Button_Click(object sender, RoutedEventArgs e)
        {
            if (GameIsStarted)
            {
                GameIsStarted = false;
                Game_Started_Button.IsEnabled = true;
                Game_Started_Button.Content = "Начать игру";
            }
            else
            {
                GameIsStarted = true;
                Game_Started_Button.IsEnabled = true;
                Game_Started_Button.Content = "Остановить игру";
                Block[] blocks = new Block[3];
                for (int i = 0; i < 3; i++)
                {
                    blocks[i] = new Block(New_Block.Rand_Block_Type(), New_Block.Rand_Rotate());
                    Generated_Blocks.Children.Add(blocks[i].polygon);
                    Grid.SetRow(blocks[i].polygon, (1 + i));
                    Grid.SetColumn(blocks[i].polygon, 1);
                }
            }
        }
    }
}
