using Gladiators.Common.API;
using Gladiators.Common.Characters;
using Gladiators.Common.Characters.Base;
using Gladiators.Common.Characters.Enum;
using Gladiators.Common.Classes;
using Gladiators.Common.Dto;
using Gladiators.Common.SkillContracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;

namespace Gladiators.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GladiatorController : ApiControllerBase
    {

        [HttpGet("Character-Classes")]
        [SwaggerOperation("get all character classes")]
        public List<NamedData> GetCharacterClasses()
        {
            //ToDo: gasatestia Dictionari da shesacvlelia tu jobs \/
            List<NamedData> characterClasses = new();

            foreach (CharacterClassesEnum characterClass in Enum.GetValues(typeof(CharacterClassesEnum)))
            {
                characterClasses.Add(new NamedData
                {
                    Id = (int)characterClass,
                    Name = characterClass.ToString()
                });
            }

            return characterClasses;
        }

        [HttpPost("Fight-Characters")]
        [SwaggerOperation("fight newly created characters to each other")]
        public ResponseModel Fight([FromBody] CharacterFightRequest fightRequest)
        {
            Character firstCharacter = CreateCharacter(fightRequest.CharacterOne);
            Character secondCharacter = CreateCharacter(fightRequest.CharacterTwo);
            Console.WriteLine($"Fight started between {firstCharacter.Name} and {secondCharacter.Name}");
            return Fight(firstCharacter, secondCharacter);
        }

        private static ResponseModel Fight(Character charOne, Character charTwo)
        {
            int roundCounter = 1;
            while (true)
            {
                if (roundCounter == 1)
                {
                    // Print the graph
                    Console.WriteLine(BuildCharacterGraph(charOne, charTwo).ToString());
                    Console.WriteLine("Press Enter to start the fight...");
                    Console.ReadLine();
                }
                if (charOne.IsDraw(charTwo))
                {
                    return GameOver(character: null, GameOverEnum.Draw);
                }
                else if (!charOne.IsAlive())
                {
                    return GameOver(character: charTwo, GameOverEnum.PlayerOneWon);
                }
                else if (!charTwo.IsAlive())
                {
                    return GameOver(character: charOne, GameOverEnum.PlayerTwoWon);
                }
                Console.WriteLine($"Round {roundCounter++}");

                charOne.UseSkill(GetSkill(charOne), charTwo);

                charTwo.UseSkill(GetSkill(charTwo), charOne);

                // Print the graph
                Console.WriteLine(BuildCharacterGraph(charOne, charTwo).ToString());

                Console.WriteLine("Press Enter to continue to the next turn...");
                Console.ReadLine();

                charOne.RegenManaAndHealth();
                charTwo.RegenManaAndHealth();
            }
        }

        //ToDo: ushualod brdzola gasatania repositorishi
        #region Private Methods
        
        private enum GameOverEnum
        {
            Draw = 1,
            PlayerOneWon,
            PlayerTwoWon
        }
        private static ResponseModel GameOver(Character character, GameOverEnum gameOverEnum)
        {
            switch (gameOverEnum)
            {
                case GameOverEnum.Draw:
                    Console.WriteLine($"Game Over, It`s a DRAW!");
                    return new ResponseModel()
                    {
                        Text = "It's a draw",
                        Character = null
                    };
                case GameOverEnum.PlayerOneWon:
                    Console.WriteLine($"Game Over, {character.Name} Won!");
                    return new ResponseModel()
                    {
                        Text = $"{character.Name} Won!",
                        Character = character
                    };
                case GameOverEnum.PlayerTwoWon:
                    Console.WriteLine($"Game Over, {character.Name} Won!");
                    return new ResponseModel()
                    {
                        Text = $"{character.Name} Won!",
                        Character = character
                    };
            }
            return new ResponseModel();
        }

        // pseudo-random
        private static BaseSkill GetSkill(Character character)
        {
            List<BaseSkill> activeSkills = character.Skills.Where(x => x.IsActive && x.ManaCost <= character.Mana).ToList();
            int activeSkillsCount = activeSkills.Count;
            if (activeSkillsCount == 0)
                return null;

            var random = new Random();
            var drawNullChance = 2;
            var drawSkillChance = 2;

            if (activeSkillsCount == 2)
                drawSkillChance = 1;
            else if (activeSkillsCount == 3)
                drawNullChance = 1;
            else if (activeSkillsCount > 3)
            {
                drawNullChance = activeSkillsCount - 2;
                drawSkillChance = 1;
            }

            var totalChances = drawNullChance + drawSkillChance;
            var randomIndex = random.Next(1, totalChances + 1);

            if (randomIndex <= drawNullChance)
                return null;

            randomIndex = random.Next(0, activeSkillsCount);
            return activeSkills[randomIndex];
        }

        private static Character CreateCharacter(CharacterModel character)
        {
            switch (character.Class)
            {
                case CharacterClassesEnum.Warrior:
                    return new Warrior
                        (character.Name,
                        character.Armor,
                        character.Crit,
                        character.Dexterity,
                        character.Intelligence,
                        character.Strength,
                        character.Vigor);
                //case CharacterClassesEnum.Archer:
                    //return new Archer
                    //    (character.Name,
                    //    character.Armor,
                    //    character.Crit,
                    //    character.Dexterity,
                    //    character.Dexterity,
                    //    character.Strength,
                    //    character.Vigor);
                    //break;
                case CharacterClassesEnum.Mage:
                    break;
            }
            return null;
        }

        static StringBuilder BuildCharacterGraph(Character characterOne, Character characterTwo)
        {
            int labelWidth = Math.Max(characterOne.Name.Length, characterTwo.Name.Length) + 2;

            StringBuilder sb = new ();

            sb.AppendLine(string.Format("{0," + (labelWidth + 1) + "} | {1,-" + labelWidth + "} | {2,-" + labelWidth + "} |",
                "", characterOne.Name, characterTwo.Name));
            sb.AppendLine();

            sb.AppendLine(string.Format("{0}{1}{2}{2}",
                new string('-', labelWidth + 3), new string('-', labelWidth), new string('-', labelWidth)));
            sb.AppendLine();

            sb.AppendLine(string.Format("{0," + (labelWidth + 2) + "} | {1,-" + labelWidth + "} | {2,-" + labelWidth + "} |",
                "Physical Damage", characterOne.PhysicalDamage, characterTwo.PhysicalDamage));
            sb.AppendLine();

            sb.AppendLine(string.Format("{0," + (labelWidth + 2) + "} | {1,-" + labelWidth + "} | {2,-" + labelWidth + "} |",
                "Health", characterOne.Health, characterTwo.Health));
            sb.AppendLine();

            sb.AppendLine(string.Format("{0," + (labelWidth + 2) + "} | {1,-" + labelWidth + "} | {2,-" + labelWidth + "} |",
                    "Mana", characterOne.Mana, characterTwo.Mana));
            sb.AppendLine();

            sb.AppendLine(string.Format("{0," + (labelWidth + 2) + "} | {1,-" + labelWidth + "} | {2,-" + labelWidth + "} |",
                "Armor", characterOne.Armor, characterTwo.Armor));
            sb.AppendLine();

            sb.AppendLine(string.Format("{0," + (labelWidth + 2) + "} | {1,-" + labelWidth + "} | {2,-" + labelWidth + "} |",
                "Crit Damage", characterOne.CritDamage, characterTwo.CritDamage));
            sb.AppendLine();

            sb.AppendLine(string.Format("{0," + (labelWidth + 2) + "} | {1,-" + labelWidth + "} | {2,-" + labelWidth + "} |",
                "Crit Rate", characterOne.CritRate, characterTwo.CritRate));
            sb.AppendLine();

            sb.AppendLine(string.Format("{0," + (labelWidth + 2) + "} | {1,-" + labelWidth + "} | {2,-" + labelWidth + "} |",
                "Magical Damage", characterOne.MagicalDamage, characterTwo.MagicalDamage));
            sb.AppendLine();

            sb.AppendLine("Skills");

            foreach (var skill in characterOne.Skills.Concat(characterTwo.Skills))
            {
                string characterName = (characterOne.Skills.Contains(skill)) ? characterOne.Name : characterTwo.Name;
                string skillName = skill.Name;
                string skillValue = skill.IsActive ? "Active" : "Inactive";
                sb.AppendLine(string.Format("{0,-" + (labelWidth + 2) + "} | {1,-" + labelWidth + "} | {2,-" + labelWidth + "} |",
                    characterName, skillName, skillValue));
            }


            return sb;
        }

        #endregion


        //ToDo: gasatania calke
        #region models to extract from here
        public struct CharacterFightRequest
        {
            public CharacterModel CharacterOne { get; set; }
            public CharacterModel CharacterTwo { get; set; }
        }

        public class CharacterModel : BaseCharacter
        {
            public CharacterClassesEnum Class { get; set; }
        }

        public struct ResponseModel
        {
            public string Text { get; set; }
            public Character Character { get; set; }
        }
        #endregion

    }
}