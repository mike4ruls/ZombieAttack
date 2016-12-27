// Michael Ray
// Professor Cascioli
// Main Program - This shouls execute the game
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieAttack
{
    class Program
    {
        static void Main(string[] args)
        {
            //Listing varibles
            string phrases = "TypingPhrases/phrases.txt";
            string zombie = "asciiArt";
            string name;

            //Title Screen
            Console.WriteLine("Welcome to ~ Zombies Attack: type or die Edition\nPress Enter");
            Console.ReadLine();
            Console.Clear();

            //The intro
            Console.Write("Type your name in: ");
            name = Console.ReadLine();
            Console.WriteLine("\nIn a world where guns are keyboards, you must type to survive. Are you ready to be a keyboard warrior? press enter to begin! ");
            Console.ReadLine();
            Console.Clear();

            // Playing the game
            Game GamePlay = new Game(phrases, zombie, name);
            GamePlay.PlayGame();
            
            Console.ReadLine();



        }
    }
}
