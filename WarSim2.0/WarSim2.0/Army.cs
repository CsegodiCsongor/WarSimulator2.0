using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSim2._0
{
    public class Army
    {
        public List<Unit> Units;
        public int InitialArmySize;

        public Army TargetArmy;

        public Army()
        {
            Units = new List<Unit>();
            InitialArmySize = 0;
        }


        #region FormationLogic
        public void PopulateArmy(int numOfUnits)
        {
            for (int i = 0; i < numOfUnits; i++)
            {
                Units.Add(new Unit());
            }
            InitialArmySize = numOfUnits;
        }

        public void CreateTestFormation(int startingX, int endingX, int startingY)
        {
            int unitSize = Units[0].Size;

            if (startingX > endingX)
            {
                int aux = endingX;
                endingX = startingX;
                startingX = aux;
            }

            int y = startingY;
            int unitIndex = 0;
            for (int i = startingX; unitIndex < Units.Count; i += unitSize * 2)
            {
                if (i > endingX)
                {
                    i = startingX;
                    y += unitSize * 2;
                }
                Units[unitIndex].Location = new Point(i, y);
                unitIndex++;
            }
        }
        #endregion


        #region BattleLogic
        public void Charge(General EnemyGeneral)
        {
            UpdateUnits();
            if (TargetArmy == null || TargetArmy.Units.Count <= 0)
            {
                ChooseTarget(EnemyGeneral);
            }
            else
            {
                foreach (Unit unit in Units)
                {
                    unit.Charge(TargetArmy);
                }
            }
        }

        private void ChooseTarget(General enemyGeneral)
        {
            if (enemyGeneral.Armies.Count > 0)
            {
                TargetArmy = enemyGeneral.Armies[Engine.random.Next(enemyGeneral.Armies.Count)];
            }
        }

        public void UpdateUnits()
        {
            for (int i = 0; i < Units.Count; i++)
            {
                if (Units[i].Health <= 0)
                {
                    Units.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion
    }
}
