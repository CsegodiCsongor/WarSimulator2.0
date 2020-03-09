using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSim2._0
{
    public class Side
    {
        public List<General> Generals;
        public int InitialGeneralCount;

        public Side()
        {
            Generals = new List<General>();
            InitialGeneralCount = 0;
        }


        #region FormationLogic
        public void PopulateGenerals(int NumOfGenerals, List<int>[] generalStats)
        {
            for (int i = 0; i < NumOfGenerals; i++)
            {
                Generals.Add(new General());
            }
            InitialGeneralCount = NumOfGenerals;

            for(int i=0;i<NumOfGenerals; i++)
            {
                Generals[i].PopulateArmies(generalStats[i].Count, generalStats[i].ToArray());
            }
        }

        public void MakeTestFormation(int startingX, int endingX)
        {
            int dif = endingX - startingX;
            for(int i=0;i<Generals.Count;i++)
            {
                Generals[i].MakeTestFormation(startingX + dif * i, endingX + dif * i);
            }
        }
        #endregion


        #region BattleLogic
        public void Charge(Side EnemySide)
        {
            UpdateGenerals();
            foreach (General general in Generals)
            {
                general.Charge(EnemySide);
            }
        }

        public void UpdateGenerals()
        {
            for (int i = 0; i < Generals.Count; i++)
            {
                if (Generals[i].Armies.Count <= 0)
                {
                    Generals.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion
    }
}
