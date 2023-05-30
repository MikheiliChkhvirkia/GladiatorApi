using Gladiators.Common.Characters;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.SkillContracts.BasedOnClass;

namespace Gladiators.Common.Skills.Warrior
{
    public class IceSpear : BaseSkill, IWarriorSkill
    {
        const string name = "Ice Spear";
        const int manaCost = 2;
        const int value = 10;
        public IceSpear(): base(name, manaCost, value) { }

        public override void Use(Character attacker)
        {
            Console.WriteLine($"Attacker {attacker.Name} used {Name} old Damage is {attacker.PhysicalDamage} new Damage is {attacker.PhysicalDamage += Value}");
        }
    }
}
