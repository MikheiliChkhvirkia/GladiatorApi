using Gladiators.Common.Characters;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.SkillContracts.BasedOnClass;

namespace Gladiators.Common.Skills.Mage
{
    public class Earthshock : BaseSkill, IMageSkill
    {
        const string name = "Earthshock";
        const int manaCost = 30;
        const int value = 20;
        public Earthshock() : base(name, manaCost, value) { }

        public override void Use(Character attacker, Character target)
        {
            base.UpdateCharacterStats(attacker);
            Console.WriteLine($"Attacker {attacker.Name} used {Name} old defender health {target.Health} new defender health is {target.Health - Value}");
        }
    }
}
