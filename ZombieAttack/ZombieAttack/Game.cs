// Michael Ray
// Professor Cascioli
// Game class - This class should loop through the game
// and print out zombies plus phrases until you die
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ZombieAttack
{
    class Game
    {
        //Listing all the global variables
        ZombieData ZD;
        string hScore = "HighScore.data"; // string for my binary file
        string phrases; // Used to hols the name of the phrases file
        string zombies; // Used to hold the name of the folder holding the ascii art
        string name; // going to hold the persons name
        int totalFkUp = 0;
        int fuckUp = 0;
        int highScore = 0;
        int score = 0;

        public Game(string phr, string zmb, string nm)
        {
            phrases = phr;
            zombies = zmb;
            name = nm;
            ZD = new ZombieData();

        }

        /// <summary>
        /// This method will run the entire game in real time keeping track of weather
        /// or not keys are pressed and how many lives you have left until you die
        /// </summary>
        public void PlayGame()
        {
            // checking if loading the phrases and zombies completed
            bool loadZ = ZD.LoadZombies(zombies);
            bool loadP = ZD.LoadPhrases(phrases);

            if (loadP == false || loadZ == false)
            {
                Console.WriteLine("Couldn't find file...");
            }

            else
            {
                //Loading the high score data in
                LoadHighScore(hScore);

                // Creating local variable
                string newZombie; // This variable is used to hold the zombie ascii art
                string newPhrase; // This variable is used to hold the phrase in
                string strNewPhrase; // This variable is used to create a new phrase string while keeping the original phrase string
                int lives = 3;
                int numChar = 0; // this variable lets me know which character the player is at in the phrase
                int time = 0;
                int Attack = 6000;

                // Generating Zombie and phrase strings
                newZombie = ZD.RandomZombie();
                newPhrase = ZD.RandomPhrase();

                //This is an added touch to bring the name you type in into the game itself
                if (newPhrase == "cause of death: physics")
                {
                    newPhrase = "Jon Doe: " + name + " " + newPhrase;
                }


                // For my game i based the time off of how long the phrase your typing is so
                // these if statemnets check to see how long the phrase is and assigns a certain
                // ammount of time to each
                if (newPhrase.Length > 40)
                {
                    Attack = 20000;
                    strNewPhrase = "Difficulty: Hard\n" + newPhrase;
                }
                else if (newPhrase.Length > 25)
                {
                    Attack = 9000;
                    strNewPhrase = "Difficulty: Medium\n" + newPhrase;
                }
                else
                {
                    strNewPhrase = "Difficulty: Easy\n" + newPhrase;
                }

                // Displaying the frist zombie encounter
                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine("Lives: " + lives + "\n");
                Console.WriteLine(newZombie);
                Console.WriteLine(strNewPhrase);

                while (lives != 0)
                {
                    // Checks for key input
                    while (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        string letter = key.KeyChar.ToString();

                        // Changes what ever you write to a green text
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(letter);

                        // This stores the character from where the player is currently at in the phrase
                        // and then into a string
                        char characters = newPhrase[numChar];
                        string strChara = "" + characters;

                        // Here i check if what the player typed is true of not
                        if (letter == strChara)
                        {
                            numChar += 1;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("<------ you fucked up");
                            Console.WriteLine();
                            fuckUp += 1;
                            numChar = 0;
                        }


                        // This if statement checks to see if you completed typing the whole phrase and diplays the next
                        // zombie encounter
                        if (numChar == newPhrase.Length)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\n\nCorrect!!! +100 points\n");
                            score += 100;
                            numChar = 0;
                            time = 0;

                            newZombie = ZD.RandomZombie();
                            newPhrase = ZD.RandomPhrase();

                            if (newPhrase == "cause of death: physics")
                            {
                                newPhrase = "Jon Doe: " + name + " " + newPhrase;
                            }

                            if (newPhrase.Length > 40)
                            {
                                Attack = 20000;
                                strNewPhrase = "Difficulty: Hard\n" + newPhrase;
                            }
                            else if (newPhrase.Length > 25)
                            {
                                Attack = 9000;
                                strNewPhrase = "Difficulty: Medium\n" + newPhrase;
                            }
                            else
                            {
                                Attack = 6000;
                                strNewPhrase = "Difficulty: Easy\n" + newPhrase;
                            }

                            Console.WriteLine("------------------------------------------------------------------");
                            Console.WriteLine("Lives: " + lives + "   Score: " + score + "\n");
                            Console.WriteLine(newZombie);
                            Console.WriteLine(strNewPhrase);
                        }
                    }
                    // Timer
                    Thread.Sleep(50);
                    time += 50;

                    // checks to see if it should attack yet
                    if (time >= Attack)
                    {
                        numChar = 0; // resets the current progress of the player back to 0
                        lives -= 1; // decreases the ammount lives you have by 1
                        time = 0; // Resets the timer

                        // This is just an personal touch to the game to make it a little different
                        if (newZombie == "A disappointed mother has appeared, you forget to take out the trash. Kill her!!\n> >\n -\n")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nYour mom smacked you and yelled at you extremly loud, you lost 1 life and 1     self-confidence!!!");
                        }
                        // The zombie attacks
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe zombie smacked you, lost 1 life...start over!!!");
                        }
                        Console.WriteLine("Lives: " + lives);

                    }

                }

                // Game Over
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine("GAME OVER\nYour final score: " + score);
                Console.WriteLine("Fuck ups: " + fuckUp);
                totalFkUp = fuckUp + totalFkUp;

                // This if statement checks if the score you got is better then the current high score
                if (score > highScore)
                {
                    Console.WriteLine("\nNEW High Score: " + score);
                    Console.WriteLine("Overall fuck ups: " + totalFkUp);
                    SaveFuckUps(hScore); // Saves the overall fuck ups to a binary file
                    SaveHighScore(hScore); // Saves the new high score to a binary file              
                }
                else
                {
                    Console.WriteLine("\nHigh Score: " + highScore);
                    Console.WriteLine("Overall fuck ups: " + totalFkUp);
                    SaveFuckUps(hScore); // Saves the overall fuck ups to a binary file
                }


            }
        }

        /// <summary>
        /// This method will load the binary file holding the highscore in it
        /// </summary>
        /// <param name="file"></param>
        public void LoadHighScore(string file)
        {
            // Creating my binary reader
            BinaryReader input = null;
            string line = null;
            try
            {
                Stream inStream = File.OpenRead(file);
                input = new BinaryReader(inStream);

                highScore = input.ReadInt32(); // Reading the highscore
                totalFkUp = input.ReadInt32();

            }
            catch (Exception e)
            {
                SaveHighScore(hScore);
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                if (input == null)
                {

                }
                else if (line == null)
                {
                    //Console.WriteLine("Load HighScore complete!!!"); <------------------just for me to check if it worked
                    input.Close();
                }
            }
        }

        /// <summary>
        /// This method will save the new high score with total fuck ups into a binary file
        /// </summary>
        /// <param name="file"></param>
        public void SaveHighScore(string file)
        {
            // Creating my binary writer
            BinaryWriter output = null;
            try
            {
                Stream outStream = File.OpenWrite(file);
                output = new BinaryWriter(outStream);

                output.Write(score); // Storing my high score into the binary file
                output.Write(totalFkUp);


            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            


        }
        /// <summary>
        /// This method should save the new ammount of fuck ups and keep the same high score point
        /// </summary>
        /// <param name="file"></param>
        public void SaveFuckUps(string file)
        {
            // Creating my binary writer
            BinaryWriter output = null;
            try
            {
                Stream outStream = File.OpenWrite(file);
                output = new BinaryWriter(outStream);

                output.Write(highScore); // Storing high score into the binary file
                output.Write(totalFkUp); // Storing fuck eps into binary file


            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            finally
            {
                output.Close();
            }
        }
    }
}
