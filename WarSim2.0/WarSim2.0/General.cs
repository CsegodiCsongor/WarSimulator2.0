using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSim2._0
{
    public class General
    {
        public List<Army> Armies;
        public int InitialArmyCount;

        public General TargetGeneral;

        public General()
        {
            Armies = new List<Army>();
            InitialArmyCount = 0;
        }


        #region FormationLogic
        public void PopulateArmies(int numOfArmies, int[] menCount)
        {
            for (int i = 0; i < numOfArmies; i++)
            {
                Armies.Add(new Army());
                Armies[i].PopulateArmy(menCount[i]);
            }
            InitialArmyCount = numOfArmies;
        }

        public void MakeTestFormation(int startingX, int endingX)
        {
            int y = 50;
            for (int i = 0; i < Armies.Count; i++)
            {
                Armies[i].CreateTestFormation(startingX, endingX, y);
                y = Armies[i].Units[Armies[i].Units.Count - 1].Location.Y + 100;
            }
        }
        #endregion


        #region BattleLogic
        public void UpdateArmies()
        {
            for (int i = 0; i < Armies.Count; i++)
            {
                if (Armies[i].Units.Count <= 0)
                {
                    Armies.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Charge(Side EnemySide)
        {
            UpdateArmies();
            if (TargetGeneral == null || TargetGeneral.Armies.Count <= 0)
            {
                ChooseTarget(EnemySide);
            }
            else
            {
                foreach (Army army in Armies)
                {
                    army.Charge(TargetGeneral);
                }
            }
        }

        public void ChooseTarget(Side EnemySide)
        {
            if (EnemySide.Generals.Count > 0)
            {
                TargetGeneral = EnemySide.Generals[Engine.random.Next(EnemySide.Generals.Count)];
            }
        }
        #endregion
    }
}
