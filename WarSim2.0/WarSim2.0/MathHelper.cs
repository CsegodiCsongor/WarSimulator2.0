using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSim2._0
{
    public static class MathHelper
    {
        public static int CalcDist(Unit unit1, Unit unit2)
        {
            return (int)(Math.Sqrt(Math.Pow(unit1.Location.X - unit2.Location.X, 2) + Math.Pow(unit1.Location.Y - unit2.Location.Y, 2)) - unit1.Size/2d - unit2.Size/2d);
        }
    }
}
