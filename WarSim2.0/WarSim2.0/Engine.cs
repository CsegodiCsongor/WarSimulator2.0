using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WarSim2._0
{
    public static class Engine
    {
        public static Random random = new Random();
        public static MapGenerator mapGenerator;
        public static double[,] map;

        #region UnitTest
        public static Unit a = new Unit(10, 10, 10, 10, 10, 3, 10, 5);
        public static Unit b = new Unit(20, 1, 20, 2000, 20, 5, 20, 10);

        public static void UnitTestInit(PictureBox pictureBox)
        {
            GraphicsEngine.Init(pictureBox);
            b.Range = 2000;
            a.Target = b;
            b.Target = a;

            a.Location = new Point(0, 0);
            b.Location = new Point(200, 300);

            GraphicsEngine.DrawUnits();
        }

        public static void UnitChargeTest()
        {
            if (a != null)
            {
                if (a.Health <= 0)
                {
                    a = null;
                }
                else
                {
                    //a.Charge();
                }
            }
            if (b != null)
            {
                if (b.Health <= 0)
                {
                    b = null;
                }
                else
                {
                    // b.Charge();
                }
            }
            GraphicsEngine.DrawUnits();
        }
        #endregion


        #region ArmyTest
        public static Army LeftArmy = new Army();
        public static Army RightArmy = new Army();

        public static Pen LeftArmyPen;
        public static Pen RightArmyPen;

        public static void TestInitArmy(PictureBox pictureBox)
        {
            GraphicsEngine.Init(pictureBox);

            LeftArmy.PopulateArmy(100);
            RightArmy.PopulateArmy(200);
            LeftArmy.CreateTestFormation(50, 100, 50);
            RightArmy.CreateTestFormation(300, 350, 50);
            LeftArmyPen = new Pen(Color.Red, 2);
            RightArmyPen = new Pen(Color.Blue, 2);

            GraphicsEngine.TestDrawArmy(LeftArmy, LeftArmyPen);
            GraphicsEngine.TestDrawArmy(RightArmy, RightArmyPen);
        }

        public static void TestChargeArmy()
        {
            GraphicsEngine.ClearCanvas();

            //LeftArmy.Charge(RightArmy);
            //RightArmy.Charge(LeftArmy);
            GraphicsEngine.TestDrawArmy(LeftArmy, LeftArmyPen);
            GraphicsEngine.TestDrawArmy(RightArmy, RightArmyPen);
        }
        #endregion


        #region GeneralTest
        public static General leftGeneral = new General();
        public static General rightGeneral = new General();

        public static Pen leftGeneralPen;
        public static Pen rightGeneralPen;

        public static void TestInitGeneral(PictureBox pictureBox)
        {
            GraphicsEngine.Init(pictureBox);

            leftGeneral.PopulateArmies(2, new int[] { 100, 50 });
            rightGeneral.PopulateArmies(3, new int[] { 100, 100, 150 });
            leftGeneralPen = new Pen(Color.Red, 2);
            rightGeneralPen = new Pen(Color.Blue, 2);
            leftGeneral.MakeTestFormation(50, 100);
            rightGeneral.MakeTestFormation(300, 500);

            GraphicsEngine.TestDrawGenerals(leftGeneral, leftGeneralPen);
            GraphicsEngine.TestDrawGenerals(rightGeneral, rightGeneralPen);
        }

        public static void TestChargeGeneral()
        {
            GraphicsEngine.ClearCanvas();

            //leftGeneral.Charge(rightGeneral);
            //rightGeneral.Charge(leftGeneral);

            GraphicsEngine.TestDrawGenerals(leftGeneral, leftGeneralPen);
            GraphicsEngine.TestDrawGenerals(rightGeneral, rightGeneralPen);
        }
        #endregion


        #region SideTest
        public static Side leftSide = new Side();
        public static Side rightSide = new Side();

        public static Pen leftSidePen;
        public static Pen rightSidePen;

        public static void TestInitSides(PictureBox pictureBox)
        {
            GraphicsEngine.Init(pictureBox);

            //leftSide.PopulateGenerals(1, new List<int>[] { new List<int>() { 1 } });
            //leftSide.Generals[0].Armies[0].Units[0].Health = 1000000000;
            //leftSide.Generals[0].Armies[0].Units[0].Radius = 10000000; catapult test.
            //leftSide.Generals[0].Armies[0].Units[0].Damage = 100;

            leftSide.PopulateGenerals(3, new List<int>[]{ new List<int>() {10, 20, 10}, new List<int>() {50, 60}, new List<int>() {70, 80} });
            rightSide.PopulateGenerals(2, new List<int>[] { new List<int>() { 5, 10 }, new List<int>() { 20, 50 } });
            leftSidePen = new Pen(Color.Red, 2);
            rightSidePen = new Pen(Color.Blue, 2);
            leftSide.MakeTestFormation(50, 100);
            rightSide.MakeTestFormation(500, 400);

            GraphicsEngine.TestDrawSides(leftSide, leftSidePen);
            GraphicsEngine.TestDrawSides(rightSide, rightSidePen);
        }

        public static void TestChargeSides()
        {
            GraphicsEngine.ClearCanvas();

            leftSide.Charge(rightSide);
            rightSide.Charge(leftSide);

            GraphicsEngine.TestDrawSides(leftSide, leftSidePen);
            GraphicsEngine.TestDrawSides(rightSide, rightSidePen);
        }
        #endregion


        #region SpacingTest

        #endregion


        #region MapTest
        public static void InitMap(PictureBox pictureBox)
        {
            mapGenerator = new MapGenerator();
            GraphicsEngine.Init(pictureBox);
        }

        public static void CreateRandomNoiseMap()
        {
            map = mapGenerator.CreateRandomNoiseMap(GraphicsEngine.canvas.Width, GraphicsEngine.canvas.Height);
            GraphicsEngine.DrawBlackAndWhiteMap(map);
        }

        public static void GuassianBlurMap(int blurWidth)
        {
            //map = mapGenerator.BlurMap(map, blurWidth, blurHeight);

            double[,] kernel = mapGenerator.CreateGuassianKernel(blurWidth);
            map = mapGenerator.KernelBlurMap(map, kernel);

            GraphicsEngine.DrawBlackAndWhiteMap(map);
        }

        public static void MeanBlurMap(int blurWidth)
        {
            double[,] kernel = mapGenerator.CreateMeanKernel(blurWidth);
            map = mapGenerator.KernelBlurMap(map, kernel);

            GraphicsEngine.DrawBlackAndWhiteMap(map);
        }
        #endregion
    }
}
