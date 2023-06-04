using Gladiators.Common.Characters;
using Gladiators.Common.Characters.Enum;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.SkillContracts.BasedOnClass;

namespace Gladiators.Common.Skills.Mage
{
    public class IceArrow : BaseSkill, IMageSkill
    {
        const string name = "Ice Arrow";
        const int manaCost = 10;
        const int value = 30;
        const int stunDuration = 4;
        public IceArrow() : base(name, manaCost, value) { }

        public override void Use(Character attacker, Character target)
        {
            base.UpdateCharacterManaAndSkillStat(attacker);
            Console.WriteLine($"Attacker {attacker.Name} used {Name}");

            int totalDamage = GetTotalDamage(attacker, target);

            target.StunDuration = stunDuration;

            Console.WriteLine($"Defender {target.Name}. Old defender health: {target.Health}. New defender health: {target.Health -= totalDamage} \n");
        }
        protected override int GetTotalDamage(Character attacker, Character target)
        {
            return target.Class switch
            {
                CharacterClassesEnum.Warrior => Value + attacker.MagicalDamage,
                CharacterClassesEnum.Mage => Value,
                CharacterClassesEnum.Archer => Value,
                _ => -1,
            };
        }
    }
}
