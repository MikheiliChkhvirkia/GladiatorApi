using Gladiators.Application.GameManager.Commands;
using Gladiators.Common.Characters;
using System.Text;

namespace Gladiators.Application.GameManager.Contracts
{
    public interface IGameManagerRepository
    {
        static int Rounds { get; set; }
        ResponseModel Fight(Character charOne, Character charTwo);
    }
}
