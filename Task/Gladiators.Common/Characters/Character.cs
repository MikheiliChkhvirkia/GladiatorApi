using Gladiators.Common.Characters.Base;
using Gladiators.Common.SkillContracts;

namespace Gladiators.Common.Characters
{
    public abstract class Character : BaseCharacter
    {
        #region Private Properties

        private int maxHealth;

        private int maxMana;

        private bool isStatsInitialized = false;

        private bool timeToRegen = true;
        #endregion

        public int Health { get; set; }
        protected int HealthRegen { get; set; }

        public int Mana { get; set; }
        protected int ManaRegen { get; set; }

        public int PhysicalDamage { get; set; }
        public int MagicalDamage { get; set; }

        public int CritRate { get; set; }
        public int CritDamage { get; set; }

        public List<BaseSkill> Skills { get; set; }

        public abstract int Attack(Character target);

        #region Protected abstracts

        protected abstract void CalculateHealth();
        protected abstract void CalculateDamage();
        protected abstract void CalculateMana();
        protected abstract void CalculateHealthRegen();
        protected abstract void CalculateManaRegen();
        protected abstract void CalculateCritRate();
        protected abstract void CalculateCritDamage();

        /// <summary>
        /// Calculates all character stats 
        /// </summary>
        protected void CalculateStats()
        {
            CalculateDamage();
            CalculateCritDamage();
            CalculateCritRate();

            CalculateHealth();
            CalculateHealthRegen();

            CalculateMana();
            CalculateManaRegen();

            if (!isStatsInitialized)
            {
                maxHealth = Health;
                maxMana = Mana;
                isStatsInitialized = true;
            }
            else
            {
                isStatsInitialized = false;
            }
        }
        public abstract void UseSkill(BaseSkill skill, Character target);

        #endregion

        #region public methods
        public bool IsAlive()
        {
            return Health > 0;
        }
        public bool IsDraw(Character opponent)
        {
            return !IsAlive() && !opponent.IsAlive();
        }
        public void DecreaseManaPoints(int manaCost)
        {
            Mana -= manaCost;
        }

        public void RegenManaAndHealth()
        {
            if (timeToRegen)
            {
                if (Health < maxHealth)
                    Health += HealthRegen;
                if (Mana < maxMana)
                    Mana += ManaRegen;
                timeToRegen = false;
            }
        }
        #endregion
    }
}
