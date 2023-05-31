using Gladiators.Common.Characters.Base;
using Gladiators.Common.Characters.Enum;

namespace Gladiators.Application.GameManager.Commands
{
    public struct CharacterFightRequest
    {
        public CharacterModel CharacterOne { get; set; }
        public CharacterModel CharacterTwo { get; set; }
    }

    public class CharacterModel : BaseCharacter
    {
        public CharacterClassesEnum Class { get; set; }
    }
}
