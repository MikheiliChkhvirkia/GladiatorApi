namespace Gladiators.Common.Characters.Base
{
    /// <summary>
    /// Base character class representing common attributes and properties of a character.
    /// </summary>
    public abstract class BaseCharacter
    {
        /// <summary>
        /// Character Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Strength attribute representing health and physical damage.
        /// </summary>
        public int Strength { get; set; }


        /// <summary>
        /// Vigor attribute representing health regeneration.
        /// </summary>
        public int Vigor { get; set; }

        /// <summary>
        /// Intelligence attribute representing mana and magic/skill damage.
        /// </summary>
        public int Intelligence { get; set; }

        /// <summary>
        /// Dexterity attribute representing mana regeneration.
        /// </summary>
        public int Dexterity { get; set; }


        /// <summary>
        /// Crit attribute representing critical hit rate.
        /// </summary>
        public int Crit { get; set; }

        /// <summary>
        /// Armor attribute representing defense against attacks.
        /// </summary>
        public int Armor { get; set; }

    }
}

