namespace PrimulMeuCursDeGrafica
{
    public partial class Form1 : Form
    {
        MyGraphics myGraphics;
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