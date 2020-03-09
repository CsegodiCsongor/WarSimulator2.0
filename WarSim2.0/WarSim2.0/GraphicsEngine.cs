using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarSim2._0
{
    public static class GraphicsEngine
    {
        public static PictureBox canvas;
        private static Bitmap bmp;
        private static Graphics grp;

        public static void Init(PictureBox pictureBox)
        {
            canvas = pictureBox;
            bmp = new Bitmap(canvas.Width, canvas.Height);
            grp = Graphics.FromImage(bmp);
        }


        #region UnitTestDraw
        public static void DrawUnits()
        {
            ClearCanvas();

            Pen redPen = new Pen(Color.Red, 5);
            Pen bluePen = new Pen(Color.Blue, 5);
            if (Engine.a != null)
            {
                grp.DrawEllipse(redPen, Engine.a.Location.X - Engine.a.Size / 2, Engine.a.Location.Y - Engine.a.Size / 2, Engine.a.Size * 2, Engine.a.Size * 2);
            }
            if (Engine.b != null)
            {
                grp.DrawEllipse(bluePen, Engine.b.Location.X - Engine.b.Size / 2, Engine.b.Location.Y - Engine.b.Size / 2, Engine.b.Size * 2, Engine.b.Size * 2);
            }

            canvas.Image = bmp;
        }
        #endregion


        #region ArmyTestDraw
        public static void TestDrawArmy(Army ArmyToDraw, Pen MyPen)
        {
            foreach (Unit unit in ArmyToDraw.Units)
            {
                grp.DrawEllipse(MyPen, unit.Location.X - unit.Size / 2, unit.Location.Y - unit.Size / 2, unit.Size * 2, unit.Size * 2);
            }

            canvas.Image = bmp;
        }
        #endregion


        #region GeneralsTestDraw
        public static void TestDrawGenerals(General GeneralToDraw, Pen MyPen)
        {
            foreach (Army army in GeneralToDraw.Armies)
            {
                TestDrawArmy(army, MyPen);
            }
            canvas.Image = bmp;
        }
        #endregion


        #region SideTestDraw
        public static void TestDrawSides(Side SideToDraw, Pen MyPen)
        {
            foreach(General general in SideToDraw.Generals)
            {
                TestDrawGenerals(general, MyPen);
            }
            canvas.Image = bmp;
        }
        #endregion


        #region MapTests
        public static void DrawBlackAndWhiteMap(double[,] mapToDraw)
        {
            for (int i = 0; i < mapToDraw.GetLength(0); i++)
            {
                for (int j = 0; j < mapToDraw.GetLength(1); j++)
                {
                    bmp.SetPixel(j, i, Color.FromArgb(255,(int)(mapToDraw[i, j] * 256), (int)(mapToDraw[i, j] * 256), (int)(mapToDraw[i, j] * 256)));
                }
            }
            canvas.Image = bmp;
        }
        #endregion


        public static void ClearCanvas()
        {
            bmp = new Bitmap(canvas.Width, canvas.Height);
            grp = Graphics.FromImage(bmp);
            canvas.Image = bmp;
        }
    }
}
