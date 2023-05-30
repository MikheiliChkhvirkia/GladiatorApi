using Gladiators.Common.Characters;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.SkillContracts.BasedOnClass;

namespace Gladiators.Common.Skills.Warrior
{
    public class FireSword : BaseSkill, IWarriorSkill
    {
        const string name = "Fire Sword";
        const int manaCost = 3;
        const int value = 20;
        public FireSword() : base(name, manaCost, value) { }

        public override void Use(Character attacker)
        {
            IsActive = false;
            attacker.Mana -= ManaCost;
            Console.WriteLine($"Attacker {attacker.Name} used {Name} old Damage is {attacker.PhysicalDamage} new Damage is {attacker.PhysicalDamage += Value}");
        }
    }
}
