using System.Text;

namespace PrimulMeuCursDeGrafica
{
    public partial class Form1 : Form
    {
        MyGraphics myGraphics;
        static Random rnd = new Random();
        float a = 0, b = 0;
        public Form1()
        {
            InitializeComponent();
            myGraphics = new MyGraphics(pictureBox1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // DrawPolygon(myGraphics.grp, RegularPolygon(4, new PointF(250, 250), 150, 0));

            /*
            for (float a=0; a<=(float)(Math.PI * 2); a += 0.01f)
            {
                DrawPolygon(myGraphics.grp, RegularPolygon(4, new PointF(250, 250), 150, a));
            }

            float b = 0;
            for (float a = 0; a <= 200; a += 10, b += 0.1f)
            {
                DrawPoints(myGraphics.grp, RegularPolygon(6, new PointF(250, 250), a, b));

            }
            */
            button1.Text = "Start";

            DrawPolygon(myGraphics.grp, IrregularPolygon(20, new PointF(250, 250), 100, 200, 0));
            myGraphics.Refresh();
        }

        private static List<PointF> RegularPolygon(int n, PointF C, float R, float fi)
        {
            List<PointF> toReturn = new List<PointF>();

            float alpha = (float)(2 * Math.PI) / n;

            for(int i=0; i<n; i++)
            {
                float x = C.X + R * (float)Math.Cos(i * alpha + fi);
                float y = C.Y + R * (float)Math.Sin(i * alpha + fi);
                toReturn.Add(new PointF(x, y));
            }
            return toReturn;
        }

        //TODO: fix iregularPolygon points
        private List<PointF> IrregularPolygon(int n, PointF C, float minR, float maxR, float fi)
        {
            List<PointF> toReturn = new List<PointF>();
            float pi2 = (float)(2 * Math.PI);
            float[] a = new float[n + 1];
            a[0] = 0;
            a[n] = pi2;

            for (int i = 1; i < n; i++)
            {
                a[i] = (float)(rnd.NextDouble() * pi2);
            }

            for (int i = 0; i <= n; i++)
                for (int j = i + 1; j < n; j++)
                    if (a[i] > a[j])
                        (a[i], a[j]) = (a[j], a[i]);
            /*
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<=n;i++)
            {
                sb.Append(a[i].ToString("0.000"));
                sb.Append(" ");
            }
            textBox1.Text = sb.ToString();
            */
            float[] alpha = new float[n];
            float[] d = new float[n];

            alpha[0] = a[0];
            for(int i=1; i<n; i++)
            {
                alpha[i] = (a[i + 1] - a[i]) + alpha[i-1];
                d[i] = (float)rnd.NextDouble() * (maxR - minR) + minR;
            }

            for (int i = 0; i < n; i++)
            {
                float x = C.X + d[i] * (float)Math.Cos(i * alpha[i] + fi);
                float y = C.Y + d[i] * (float)Math.Sin(i * alpha[i] + fi);
                toReturn.Add(new PointF(x, y));
                myGraphics.grp.DrawLine(Pens.Red, x, y, C.X, C.Y);
            }
            return toReturn;
        }

        public void DrawPolygon(Graphics grp, List<PointF> points)
        {
            grp.DrawPolygon(new Pen(Color.Black), points.ToArray());
        }

        public void DrawPoints(Graphics grp, List<PointF> points)
        {
            foreach(var point in points)
            {
                grp.DrawEllipse(new Pen(Color.Black), point.X - 1, point.Y - 1, 3, 3);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            a += 10;
            b += 0.1f;

            if (a >= 200)
                a = 10;

            myGraphics.Clear();
            DrawPoints(myGraphics.grp, RegularPolygon(6, new PointF(250, 250), a, b));
            myGraphics.Refresh();
        }
    }
}