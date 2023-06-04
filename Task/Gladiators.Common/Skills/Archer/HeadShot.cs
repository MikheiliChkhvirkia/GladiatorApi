using Gladiators.Common.Characters;
using Gladiators.Common.Characters.Enum;
using Gladiators.Common.SkillContracts;
using Gladiators.Common.SkillContracts.BasedOnClass;

namespace Gladiators.Common.Skills.Archer
{
    public class HeadShot : BaseSkill, IArcherSkill
    {
        const string name = "HeadShot";
        const int manaCost = 3;
        const int value = 0;
        const int stunDuration = 2;
        public HeadShot() : base(name, manaCost, value) { }

        public override void Use(Character attacker, Character target)
        {
            base.UpdateCharacterManaAndSkillStat(attacker);

            Console.WriteLine($"Attacker {attacker.Name} used {Name}");

            int totalDamage = GetTotalDamage(attacker, target);

            target.StunDuration = stunDuration;

            Console.WriteLine($"Defender {target.Name}. Old defender health: {target.Health}. New defender health: {target.Health -= attacker.PhysicalDamage * 2 + totalDamage}\n");
            Console.WriteLine($"Defender {target.Name}. is now stunned");
        }

        protected override int GetTotalDamage(Character attacker, Character target)
        {
            return target.Class switch
            {
                CharacterClassesEnum.Warrior => Value,
                CharacterClassesEnum.Mage => Value + attacker.MagicalDamage,
                CharacterClassesEnum.Archer => Value,
                _ => -1,
            };
        }

    }
}
