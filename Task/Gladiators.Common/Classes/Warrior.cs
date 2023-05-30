using Gladiators.Common.Characters;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.Skills.Warrior;

namespace Gladiators.Common.Classes
{
    public class Warrior : Character
    {
        public Warrior(string name, int armor, int crit, int dexterity, int intelligence, int strength, int vigor) : base()
        {
            Name = name;
            Armor = armor;
            Crit = crit;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Strength = strength;
            Vigor = vigor;

            Skills = new List<BaseSkill>() {
                new Defence(),
                new IceSpear(),
                new FireSword(),
            };

            CalculateStats();
        }

        #region Calculations

        protected override void CalculateDamage()
            => PhysicalDamage = Strength + Strength / 2 + Strength * 2;

        protected override void CalculateHealth()
            => Health = Strength * 2 * 100;

        protected override void CalculateHealthRegen()
            => HealthRegen = Vigor;

        protected override void CalculateMana()
            => Mana = Intelligence;

        protected override void CalculateManaRegen()
            => ManaRegen = Dexterity;

        protected override void CalculateCritRate()
            => CritRate = 5;

        protected override void CalculateCritDamage()
            => CritDamage = PhysicalDamage * 10;

        #endregion

        #region Actions
        public override int Attack(Character target)
        {
            int dmgToTarget = PhysicalDamage;
            if (target is Warrior warriorTarget)
            {
                if (PhysicalDamage <= warriorTarget.Armor * 2)
                {
                    warriorTarget.Armor -= dmgToTarget;
                }
                else
                {
                    if (warriorTarget.Armor > 0) 
                    {
                        dmgToTarget = Math.Abs(warriorTarget.Armor * 2 - PhysicalDamage);
                        warriorTarget.Armor -= dmgToTarget/2;
                        warriorTarget.Health -= dmgToTarget;
                    }
                    else
                    {
                        warriorTarget.Armor = 0;
                        warriorTarget.Health -= dmgToTarget;
                    }
                    
                }

                Console.WriteLine($"Attacker {Name} used normal attack on {target.Name} with {dmgToTarget} targets health is {warriorTarget.Health}");
                return dmgToTarget;
            }
            return 0;
        }

        public override void UseSkill(BaseSkill skill, Character target)
        {
            switch (this)
            {
                case Warrior attacker when attacker.Mana >= skill?.ManaCost:
                    skill.Use(attacker);
                    break;
                default:
                    // Attacker is not a Warrior or does not have enough mana, use normal attack
                    Attack(target);
                    break;
            }
        }
        #endregion

    }

}

