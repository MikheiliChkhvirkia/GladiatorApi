using Gladiators.Common.Characters.Base;
using Gladiators.Common.Characters.Enum;
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
        public int StunDuration;
        public CharacterClassesEnum Class { get; protected set; }
        public int SkillCooldown { get; protected set; }
        public int Health { get; set; }
        protected int HealthRegen { get; set; }

        public int Mana { get; set; }
        protected int ManaRegen { get; set; }

        public int PhysicalDamage { get; set; }
        public int MagicalDamage { get; set; }

        public int CritRate { get; set; }
        public int CritDamage { get; set; }

        public List<BaseSkill> Skills { get; set; }

        public abstract void Attack(Character target);

        #region Protected abstracts

        protected abstract void CalculateHealth();
        protected abstract void CalculateDamage();
        protected abstract void CalculateMagicalDamage();
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
            CalculateMagicalDamage();

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

        public void RegenManaAndHealth()
        {
            if (timeToRegen)
            {
                Health = Math.Min(Health + HealthRegen, maxHealth);
                Mana = Math.Min(Mana + ManaRegen, maxMana);
                timeToRegen = false;
            }
            else
            {
                timeToRegen = true;
            }

        }

        public bool IsStunned()
        {
            if (StunDuration-- > 0)
                return true;
            return false;
        }

        public bool RollForCritical()
        {
            // Calculate the crit rate based on character stats or other factors
            double critRate = (double)CritRate / 100; // Example: 5 / 100 5% crit rate

            // Roll a random number between 0 and 1
            double roll = new Random().NextDouble();

            // Check if the roll is within the crit rate
            return roll <= critRate;
        }

        #endregion
    }
}
