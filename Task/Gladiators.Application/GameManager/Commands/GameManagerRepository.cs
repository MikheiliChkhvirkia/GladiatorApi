using Gladiators.Application.GameManager.Contracts;
using Gladiators.Application.GameManager.Enums;
using Gladiators.Common.Characters;
using Gladiators.Common.SkillContracts;
using System.Text;

namespace Gladiators.Application.GameManager.Commands
{
    public class GameManagerRepository : IGameManagerRepository
    {
        public int Rounds { get; set; } = 1;

        public ResponseModel Fight(Character charOne, Character charTwo)
        {
            PrintCharacterGraph(charOne, charTwo, "Press Enter to start the fight...");

            while (true)
            {
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

                Console.WriteLine($"Round {Rounds++} \n");

                PerformTurn(charOne, charTwo);
                PerformTurn(charTwo, charOne);

                PrintCharacterGraph(charOne, charTwo, "Press Enter to continue to the next turn...");

                charOne.RegenManaAndHealth();
                charTwo.RegenManaAndHealth();
            }
        }
        #region Private Methods
        private void PrintCharacterGraph(Character charOne, Character charTwo, string text)
        {
            Console.WriteLine(BuildCharacterGraph(charOne, charTwo).ToString());
            Console.WriteLine(text);
            Console.ReadLine();
            if (Rounds % 5 == 0)
            {
                Console.Clear();
            }
        }

        private void PerformTurn(Character attacker, Character target)
        {
            if (!attacker.IsStunned())
            {
                attacker.UseSkill(GetSkill(attacker), target);
            }
            else
            {
                if (attacker.StunDuration > 0)
                {
                    Console.WriteLine($"{attacker.Name} is stunned! {attacker.StunDuration} Rounds are left!\n");
                }
                else
                {
                    Console.WriteLine($"{attacker.Name} will attack next round.\n");
                }
            }
        }

        private static StringBuilder BuildCharacterGraph(Character characterOne, Character characterTwo)
        {
            int labelWidth = Math.Max(characterOne.Name.Length, characterTwo.Name.Length) + 2;

            StringBuilder sb = new();

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

        private ResponseModel GameOver(Character character, GameOverEnum gameOverEnum)
        {
            Rounds = 1;
            ResponseModel result = new();
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
                    result = new ResponseModel()
                    {
                        Text = $"{character.Name} Won!",
                        Character = character
                    };
                    return result;
                case GameOverEnum.PlayerTwoWon:
                    Console.WriteLine($"Game Over, {character.Name} Won!");
                    result = new ResponseModel()
                    {
                        Text = $"{character.Name} Won!",
                        Character = character
                    };
                    return result;
            }
            return result;
        }

        private BaseSkill GetSkill(Character character)
        {
            List<BaseSkill> activeSkills = character.Skills
                .Where(x => x.IsActive && x.ManaCost <= character.Mana)
                .ToList();

            //Activate disabled skills if last round activated was 5 rounds ago warrior 2 rounds age if mage
            List<BaseSkill> disabledSkills = character.Skills
                .Where(x => !x.IsActive && x.LastActivated + character.SkillCooldown <= Rounds)
                .ToList();

            disabledSkills?.ForEach(skill => skill.ActivateSkill());

            int activeSkillsCount = activeSkills.Count;

            if (activeSkillsCount == 0)
                return null;

            Random random = new();
            int nullChance = activeSkillsCount - 1;
            int skillChance = 1;

            int totalChances = nullChance + skillChance;
            int randomIndex = random.Next(1, totalChances + 1);

            if (randomIndex <= nullChance)
                return null;

            randomIndex = random.Next(0, activeSkillsCount);
            activeSkills[randomIndex].LastActivated = Rounds;
            return activeSkills[randomIndex];
        }
        #endregion

    }
}
