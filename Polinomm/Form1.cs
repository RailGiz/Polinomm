using System.Drawing;
using System.Reflection;

namespace Polinomm
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private int xAxisMaxValue = 1000;
        private int yAxisMaxValue = 1000;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(panel1.Width, panel1.Height);
            panel1.BackColor = Color.White;
        }

        private List<PointF> points = new List<PointF>();

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

                // Добавляем подпись "y" по вертикальной оси
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;
                g.DrawString("y", font, brush, panel1.Width / 2, panel1.Height - 500, format);

                // Добавляем подпись "x" по горизонтальной оси
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Near;
                g.DrawString("x", font, brush, panel1.Width - 30, panel1.Height / 2, format);

                points.Add(new PointF(-100, 100));
                 points.Add(new PointF(0, 0));
                
                 points.Add(new PointF(100, 100));
                 DrawLagrangePolynomial();

            }
        }

        private void panel1_MouseClick_1(object sender, MouseEventArgs e)
        {
            PointF point = new PointF(
                (float)e.X / panel1.Width * xAxisMaxValue - xAxisMaxValue / 2,
                -(float)e.Y / panel1.Height * yAxisMaxValue + yAxisMaxValue / 2
            );
            points.Add(point);
            DrawPoints();
            /* points.Add(new PointF(-100, -100));
             points.Add(new PointF(0, 0));
             points.Add(new PointF(100, 100));*/
            DrawLagrangePolynomial();
        }

        private void DrawPoints()
        {
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            foreach (PointF point in points)
            {
                PointF panelPoint = new PointF(
                    (point.X + xAxisMaxValue / 2) / xAxisMaxValue * panel1.Width,
                    (yAxisMaxValue / 2 - point.Y) / yAxisMaxValue * panel1.Height
                );
                g.FillEllipse(Brushes.Red, panelPoint.X - 3, panelPoint.Y - 3, 6, 6);
                string coordinates = $"({point.X}, {point.Y})";
                SizeF size = g.MeasureString(coordinates, this.Font);
                float x = panelPoint.X + 10;
                float y = panelPoint.Y - 20;
                g.FillRectangle(Brushes.White, x, y, size.Width, size.Height);
                g.DrawString(coordinates, this.Font, Brushes.Black, x, y);
            }
            panel1.Invalidate();
        }


        private void DrawLagrangePolynomial()
        {
            if (points.Count < 2 || panel1.Width <= 0 || panel1.Height <= 0)
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
            double prevSum = 0;
            for (int x = 0; x < panel1.Width; x++)
            {
                double sum = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    double product = y[i];
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (j != i)
                        {

                            double panelX = (double)x / panel1.Width * xAxisMaxValue - xAxisMaxValue / 2;
                            product *= (panelX - points[j].X) / (double)(points[i].X - points[j].X);
                        }
                    }
                    sum += product;
                }


                // Переводим координаты полинома в координаты панели
                float panelY = (float)((yAxisMaxValue / 2 - sum) / yAxisMaxValue * panel1.Height);
                panelY = Math.Max(float.MinValue, Math.Min(float.MaxValue, panelY));

                // Рисуем линии полинома
                if (x > 0)
                {
                    float prevPanelY = (float)((yAxisMaxValue / 2 - prevSum) / yAxisMaxValue * panel1.Height);
                    prevPanelY = Math.Max(float.MinValue, Math.Min(float.MaxValue, prevPanelY));
                    Pen bluePen = new Pen(Color.FromArgb(100, 0, 0, 255), 3);
                    g.DrawLine(bluePen, x - 1, prevPanelY, x, panelY);
                }
                prevSum = sum;
                // Обновляем графический контекст панели
                panel1.Invalidate();
            }

            // Разбиваем оси на равные отрезки
            int numberOfTicks = 10;
            float tickSize = 5;
            Pen tickPen = new Pen(Color.Black, 1);
            Font tickFont = new Font("Arial", 10);
            Brush tickBrush = Brushes.Black;
            StringFormat tickFormat = new StringFormat();
            tickFormat.Alignment = StringAlignment.Center;
            tickFormat.LineAlignment = StringAlignment.Near;

            for (int i = 1; i < numberOfTicks; i++)
            {
                int xTick = i * panel1.Width / numberOfTicks;
                int yTick = i * panel1.Height / numberOfTicks;
                float xValue = i * xAxisMaxValue / numberOfTicks - xAxisMaxValue / 2;
                float yValue = yAxisMaxValue / 2 - i * yAxisMaxValue / numberOfTicks;

                g.DrawLine(tickPen, xTick, panel1.Height / 2 - tickSize, xTick, panel1.Height / 2 + tickSize);
                g.DrawLine(tickPen, panel1.Width / 2 - tickSize, yTick, panel1.Width / 2 + tickSize, yTick);

                g.DrawString(xValue.ToString(), tickFont, tickBrush, xTick, panel1.Height / 2 + tickSize, tickFormat);
                g.DrawString(yValue.ToString(), tickFont, tickBrush, panel1.Width / 2 - tickSize * 2, yTick, tickFormat);
            }

            double integrationStep = (xAxisMaxValue - 0) / panel1.Width;
            double integralValue = 0;

            for (int x = 1; x < panel1.Width; x++)
            {
                double panelX1 = (float)(x - 1) / panel1.Width * xAxisMaxValue - xAxisMaxValue / 2;
                double panelX2 = (float)x / panel1.Width * xAxisMaxValue - xAxisMaxValue / 2;

                double sum1 = 0;
                double sum2 = 0;

                for (int i = 0; i < points.Count; i++)
                {
                    double product1 = y[i];
                    double product2 = y[i];

                    for (int j = 0; j < points.Count; j++)
                    {
                        if (j != i)
                        {
                            product1 *= (panelX1 - points[j].X) / (double)(points[i].X - points[j].X);
                            product2 *= (panelX2 - points[j].X) / (double)(points[i].X - points[j].X);
                        }
                    }
                    sum1 += product1;
                    sum2 += product2;
                }

                // Вычисляем приближенное значение интеграла с использованием метода трапеций
                integralValue += (sum1 + sum2) / 2 * integrationStep;
            }

            // Выводим значение интеграла в textBox1
            textBox1.Text = integralValue.ToString();
            panel1.Invalidate();
        }

        private void axisXMaxTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(axisXMaxTextBox.Text, out int value))
            {
                xAxisMaxValue = value;
            }
        }

        private void axisYMaxTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(axisYMaxTextBox.Text, out int value))
            {
                yAxisMaxValue = value;
            }
        }
    }
}


