using System.Buffers.Text;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace Task
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }


        //Task 1: Classes and Constructors
        //Create a class Character with properties like Name, Health, Strength, and AvailablePoints.
        //Implement two constructors - a default constructor that assigns some default values and another constructor which accepts values for the properties.
        //The AvailablePoints property is the total points a player can distribute between Health and Strength.

        //Task 2: Inheritance and Method Overriding
        //Create a derived class Warrior that inherits from the Character class.
        //Add a new property Armor.Override the Attack() method to include a bonus damage when Armor is above a certain value.

        //Task 3: Abstract Classes
        //Refactor your Character class to be an abstract class, and make the Attack() method abstract.
        //This method should now take another Character object as a parameter and reduce their Health based on the attacker's Strength.

        //Task 4: Introduction to Interfaces
        //Create an interface ISkill with a method UseSkill().
        //Implement this interface in the Warrior class with a skill that,
        //for instance,
        //increases Armor or Strength temporarily but at the cost of AvailablePoints.

        //Task 5: Interface Implementation with Inheritance
        //Create another class Mage that also inherits from Character and implements the ISkill interface.
        //Mage should have its own properties like Mana and a different implementation of UseSkill() - perhaps a spell that heals the character or increases their attack but also at the cost of AvailablePoints.

        //Task 6: Implementing IDisposable and using the 'using' keyword
        //Each Character has a Weapon object that needs to be disposed of when the character is finished with it.
        //Implement IDisposable in the Weapon class and ensure that it releases its resources properly.
        //Also, implement IDisposable in the Character class and dispose of the Weapon object when the character is disposed of.

        //In the game simulation, whenever a Weapon is equipped to a Character, use the using keyword.
        //This will ensure that the Weapon resources are properly released when the Character is done with it, even if an exception occurs.

        //Task 7: Polymorphism with Interfaces and Abstract Classes
        //Create a list of Character objects(containing Warrior and Mage instances).
        //Make characters attack each other in turn.
        //Create a list of ISkill objects(also Warrior and Mage instances) and make them use their skills.
        //Monitor the characters' Health and AvailablePoints to see how they change through the actions.

        //Again, remember to add checks to ensure Health, Strength, and other stats cannot go negative.
        //A character can't use skills or attack if their Health is at 0.
}