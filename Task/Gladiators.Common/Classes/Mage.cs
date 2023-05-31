using Gladiators.Common.Characters;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.Skills.Mage;

namespace Gladiators.Common.Classes
{
    public class Mage : Character
    {
        public Mage(string name, int armor, int crit, int dexterity, int intelligence, int strength, int vigor) : base()
        {
            Name = name;
            Armor = armor;
            Crit = crit;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Strength = strength;
            Vigor = vigor;

            Skills = new List<BaseSkill>() {
                new Earthshock()
            };

            CalculateStats();
        }

        #region Calculations

        protected override void CalculateDamage()
            => PhysicalDamage = Strength;

        protected override void CalculateHealth()
            => Health = Strength * 100;

        protected override void CalculateHealthRegen()
            => HealthRegen = Vigor;

        protected override void CalculateMana()
            => Mana = Intelligence * 2;

        protected override void CalculateManaRegen()
            => ManaRegen = Dexterity + 10;

        protected override void CalculateCritRate()
            => CritRate = 0;

        protected override void CalculateCritDamage()
            => CritDamage = PhysicalDamage * 10;

        #endregion

        #region Actions
        public override int Attack(Character target)
        {
            int dmgToTarget;
            if (target is Warrior warriorTarget)
            {
                dmgToTarget = PhysicalDamage - warriorTarget.Armor * 2;
                warriorTarget.Health -= dmgToTarget;
                Console.WriteLine($"Attacker {Name} used normal attack on {target.Name} with {dmgToTarget} targets health is {warriorTarget.Health}");
                return dmgToTarget;
            }
            return 0;
        }

        public override void UseSkill(BaseSkill skill, Character target)
        {
            switch (this)
            {
                case Mage attacker when attacker.Mana >= skill?.ManaCost:
                    skill.Use(attacker, target);
                    attacker.Mana -= skill.ManaCost;
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
