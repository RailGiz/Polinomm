namespace Polinomm
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(panel1.Width, panel1.Height);
            // явно задаем цвет фона панели
            panel1.BackColor = Color.White;
        }

        private List<Point> points = new List<Point>();
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (bitmap != null)
            {
                Graphics g = e.Graphics;
                g.DrawImage(bitmap, Point.Empty);

                // Рисуем оси координат
                Pen pen = new Pen(Color.Black, 2);
                g.DrawLine(pen, 0, panel1.Height / 2, panel1.Width, panel1.Height / 2);
                g.DrawLine(pen, panel1.Width / 2, 0, panel1.Width / 2, panel1.Height);

                // Добавляем подписи к осям
                Font font = new Font("Arial", 20);
                Brush brush = Brushes.Black;
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Far;
                g.DrawString("y", font, brush, panel1.Width / 2, 0, format);
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;
                g.DrawString("x", font, brush, panel1.Width - 20, panel1.Height / 2, format);

            }
        }

        private void panel1_MouseClick_1(object sender, MouseEventArgs e)
        {
            Point point = e.Location;
            points.Add(point);
            DrawPoints();
            DrawLagrangePolynomial();
        }

        private void DrawPoints()
        {
            
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            foreach (Point point in points)
            {
                g.FillEllipse(Brushes.Red, point.X - 3, point.Y - 3, 6, 6);
                string coordinates = $"({point.X}, {point.Y})";
                SizeF size = g.MeasureString(coordinates, this.Font);
                float x = point.X + 10;
                float y = point.Y - 20;
                g.FillRectangle(Brushes.White, x, y, size.Width, size.Height);
                g.DrawString(coordinates, this.Font, Brushes.Black, x, y);
            }
            panel1.Invalidate();
        }
        private void DrawLagrangePolynomial()
        {
            if (points.Count < 2)
            {
                return;
            }

            Graphics g = Graphics.FromImage(bitmap);

            // сохраняем нарисованные точки
            DrawPoints();

            // Найти значения полинома в узлах интерполяции
            double[] y = new double[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                y[i] = points[i].Y;
            }

            // Интерполяционный полином Лагранжа
            double integral = 0;
            for (int x = 0; x < panel1.Width - 1; x++)
            {
                double sum = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    double product = y[i];
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (j != i)
                        {
                            product *= (x - points[j].X) / (double)(points[i].X - points[j].X);
                        }
                    }
                    sum += product;
                }
                integral += sum * ((panel1.Width - 1) / (double)points.Count);
                g.FillRectangle(Brushes.Blue, x, (float)sum, 1, 1);
            }

            panel1.Invalidate();

            textBox1.Text = "Значение интеграла: " + integral.ToString();
        }

    }
}