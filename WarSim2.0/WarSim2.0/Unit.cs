using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSim2._0
{
    public class DirectionVector
    {
        public float xScale { get; set; }
        public float yScale { get; set; }

        public void CalcScale(Unit SourceUnit, Unit TargetUnit)
        {
            float xScale = TargetUnit.Location.X - SourceUnit.Location.X;
            float yScale = TargetUnit.Location.Y - SourceUnit.Location.Y;

            if (Math.Abs(xScale - yScale) > 0)
            {
                yScale /= Math.Abs(xScale);
                xScale /= Math.Abs(xScale);
            }
            else
            {
                xScale /= Math.Abs(yScale);
                yScale /= Math.Abs(yScale);
            }

            this.xScale = xScale;
            this.yScale = yScale;
        }
    }


    public class Unit
    {
        //Location of the unit. We will use the Point struct for now but we could change it to a Point3D.
        //It can represent either coordinates or tile indexes... we will decide later...
        public Point Location;
        //Params that will decide if the unit will hit the target or dodge the attack
        public int Size { get; set; }
        public int Accuracy { get; set; }  //Percentage
        public int Dexterity { get; set; } //Percentage
        //The spped at which the unit will move
        public int MovementSpeed { get; set; }
        //The damage that the unit will inflict/recieve and if the target is in range
        public int Defense { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        //The criteria if the unit is alive
        public int Health { get; set; }
        //We will see later...
        public bool Veteran { get; set; }
        public int Morale { get; set; }
        //The radius of the attack for catapults maybe and whatnot
        public int Radius { get; set; }
        //Direction Vector for movement
        private DirectionVector Direction;

        public Unit Target;

        public Unit(int Health = 50, int Damage = 2, int Defense = 20, int Accuracy = 50, int Dexterity = 10, int MovementSpeed = 3, int Size = 5, int Range = 2000)
        {
            this.Health = Health;
            this.Damage = Damage;
            this.Defense = Defense;
            this.Accuracy = Accuracy;
            this.Dexterity = Dexterity;
            this.MovementSpeed = MovementSpeed;
            this.Size = Size;
            this.Range = Range;
        }


        #region BattleLogic
        private void Attack(Army enemyArmy)
        {
            //Need complex attack logic

            //For the moment we will settle with the following:
            if (Radius == 0)
            {
                AttemptAttack(Target);
            }
            else
            {
                foreach(Unit enemy in enemyArmy.Units)
                {
                    if(MathHelper.CalcDist(this, enemy) <= Radius)
                    {
                        AttemptAttack(enemy);
                    }
                }
            }
        }

        private void AttemptAttack(Unit enemyUnit)
        {
            enemyUnit.Health -= Damage;
            if (enemyUnit.Health <= 0)
            {
                if(enemyUnit == Target)
                {
                    Target = null;
                }
            }
        }

        private void MoveToTarget()
        {
            Direction = new DirectionVector();
            Direction.CalcScale(this, Target);
            Location.X += (int)(Direction.xScale * MovementSpeed);
            Location.Y += (int)(Direction.yScale * MovementSpeed);
        }

        private void SelectTarget(Army EnemyArmy)
        {
            if (EnemyArmy.Units.Count > 0)
            {
                Target = EnemyArmy.Units[Engine.random.Next(EnemyArmy.Units.Count)];
            }
        }

        public void Charge(Army EnemyArmy)
        {
            if (Target != null)
            {
                if (MathHelper.CalcDist(this, Target) <= Range)
                {
                    Attack(EnemyArmy);
                }
                else
                {
                    MoveToTarget();
                }
            }
            else
            {
                SelectTarget(EnemyArmy);
            }
        }
        #endregion
    }
}
