using Gladiators.Common.Characters;
using Gladiators.Common.Characters.Enum;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.Skills.Warrior;

namespace Gladiators.Common.Classes
{
    public class Warrior : Character
    {
        public Warrior(string name, int armor, int crit, int dexterity, int intelligence, int strength, int vigor) : base()
        {
            Class = CharacterClassesEnum.Warrior;
            SkillCooldown = 5;
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
        protected override void CalculateMagicalDamage()
            => MagicalDamage = Intelligence / 2;

        protected override void CalculateHealth()
            => Health = Strength * 2 * 50;

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
        public override void Attack(Character target)
        {
            int damageToTarget = target is Archer ? PhysicalDamage + MagicalDamage : PhysicalDamage;

            if (PhysicalDamage <= target.Armor * 2)
            {
                target.Armor -= damageToTarget;
            }
            else
            {
                if (target.Armor > 0)
                {
                    damageToTarget = Math.Abs(target.Armor * 2 - PhysicalDamage);
                    target.Armor -= damageToTarget / 2;
                    target.Health -= damageToTarget;
                }
                else
                {
                    target.Armor = 0;
                    target.Health -= damageToTarget;
                }
            }

            // Check for critical damage
            bool isCritical = RollForCritical();
            if (isCritical)
            {
                target.Health -= CritDamage;
                Console.WriteLine($"Attacker {Name} used normal attack on {target.Name} with {damageToTarget} damage. Critical hit! Target's health is {target.Health} (Critical Damage: {CritDamage})  \n");
            }
            else
            {
                Console.WriteLine($"Attacker {Name} used normal attack on {target.Name} with {damageToTarget} damage. Target's health is {target.Health}  \n");
            }
        }

        public override void UseSkill(BaseSkill skill, Character target)
        {
            if (Mana >= skill?.ManaCost)
            {
                skill.Use(this, target);
                return;
            }
            Attack(target);
            return;
        }
        #endregion

    }

}

