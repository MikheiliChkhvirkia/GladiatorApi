using Gladiators.Common.Characters;
using Gladiators.Common.Characters.Enum;

namespace Gladiators.Common.SkillContracts
{
    public abstract class BaseSkill
    {
        protected BaseSkill(string name, int manaCost, int value)
        {
            Name = name;
            ManaCost = manaCost;
            Value = value;
        }
        public string Name { get; }
        public bool IsActive { get; protected set; } = true;
        public int ManaCost { get; }
        public int Value { get; protected set; }
        public int LastActivated { get; set; }

        public virtual void Use(Character attacker, Character target)
            => throw new NotImplementedException();

        public virtual void ActivateSkill()
        {
            IsActive = true;
        }

        protected virtual void UpdateCharacterManaAndSkillStat(Character character)
        {
            IsActive = false;
            character.Mana -= ManaCost;
        }

        protected virtual int GetTotalDamage(Character attacker, Character target)
        {
            return target.Class switch
            {
                CharacterClassesEnum.Warrior => Value,
                CharacterClassesEnum.Mage => Value,
                CharacterClassesEnum.Archer => Value,
                _ => -1,
            };
        }
    }
}
