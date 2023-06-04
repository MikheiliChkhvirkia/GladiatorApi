using Gladiators.Application.GameManager.Commands;
using Gladiators.Application.GameManager.Contracts;
using Gladiators.Common.API;
using Gladiators.Common.Characters;
using Gladiators.Common.Characters.Enum;
using Gladiators.Common.Classes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gladiators.API.Controllers
{
    public class GladiatorController : ApiControllerBase
    {
        public GladiatorController(IGameManagerRepository repository)
            : base(repository) { }

        [HttpGet("Character-Classes")]
        [SwaggerOperation("get all character classes")]
        public Dictionary<string, int> GetCharacterClasses()
        {
            Dictionary<string, int> characterClasses = new();

            foreach (CharacterClassesEnum characterClass in Enum.GetValues(typeof(CharacterClassesEnum)))
            {
                characterClasses.Add(characterClass.ToString(), (int)characterClass);
            }

            return characterClasses;
        }

        [HttpPost("Fight-Characters")]
        [SwaggerOperation("fight newly created characters to each other (use max 25 points)")]
        public ResponseModel FightCharacters([FromBody] CharacterFightRequest fightRequest)
        {
            Character firstCharacter = CreateCharacter(fightRequest.CharacterOne);
            Character secondCharacter = CreateCharacter(fightRequest.CharacterTwo);
            Console.WriteLine($"Fight started between {firstCharacter.Name} and {secondCharacter.Name}");
            return repository.Fight(firstCharacter, secondCharacter);
        }

        #region Private Methods

        private static Character CreateCharacter(CharacterModel character)
        {
            return character.Class switch
            {
                CharacterClassesEnum.Warrior => new Warrior
                                        (character.Name,
                                        character.Armor,
                                        character.Crit,
                                        character.Dexterity,
                                        character.Intelligence,
                                        character.Strength,
                                        character.Vigor),
                CharacterClassesEnum.Mage => new Mage
                                        (character.Name,
                                        character.Armor,
                                        character.Crit,
                                        character.Dexterity,
                                        character.Dexterity,
                                        character.Strength,
                                        character.Vigor),
                CharacterClassesEnum.Archer => new Archer
                                        (character.Name,
                                        character.Armor,
                                        character.Crit,
                                        character.Dexterity,
                                        character.Dexterity,
                                        character.Strength,
                                        character.Vigor),
                _ => null,
            };
        }

        #endregion
    }
}