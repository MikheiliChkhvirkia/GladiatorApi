using Gladiators.Common.Characters;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.SkillContracts.BasedOnClass;

namespace Gladiators.Common.Skills.Warrior
{
    public class Defence : BaseSkill, IWarriorSkill
    {
        const string name = "Double Defence";
        const int manaCost = 1;
        const int value = 2;
        public Defence() : base(name, manaCost, value) { }

        public override void Use(Character attacker)
        {
            IsActive = false;
            attacker.Mana -= ManaCost;
            Console.WriteLine($"Attacker {attacker.Name} used {Name} old armor is {attacker.Armor} new armor is {attacker.Armor *= Value}");
        }
    }
}
