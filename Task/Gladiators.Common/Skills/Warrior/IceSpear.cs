using Gladiators.Common.Characters;
using Gladiators.Common.Characters.Enum;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.SkillContracts.BasedOnClass;

namespace Gladiators.Common.Skills.Warrior
{
    public class IceSpear : BaseSkill, IWarriorSkill
    {
        const string name = "Ice Spear";
        const int manaCost = 2;
        const int value = 10;
        const int stunDuration = 1;
        public IceSpear() : base(name, manaCost, value) { }

        public override void Use(Character attacker, Character target)
        {
            base.UpdateCharacterManaAndSkillStat(attacker);

            Console.WriteLine($"Attacker {attacker.Name} used {Name}");

            int totalDamage = GetTotalDamage(attacker, target);
            target.StunDuration = stunDuration;

            Console.WriteLine($"Attacker {attacker.Name} used {Name} old Damage is {attacker.PhysicalDamage} new Damage is {attacker.PhysicalDamage + totalDamage}");
            Console.WriteLine($"Defender {target.Name}. Old defender health: {target.Health}. New defender health: {target.Health -= attacker.PhysicalDamage + totalDamage} \n");
        }

        protected override int GetTotalDamage(Character attacker, Character target)
        {
            return target.Class switch
            {
                CharacterClassesEnum.Warrior => Value,
                CharacterClassesEnum.Mage => Value,
                CharacterClassesEnum.Archer => Value + attacker.MagicalDamage,
                _ => -1,
            };
        }
    }
}
