// Michael Ray
// Professor Cascioli
// ZombieData class - This class should load in all the needed to run the game
// all the way down to randomizing phrases and zombies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZombieAttack
{
    class ZombieData
    {
        // Listing out global variables
        string[] zombies; //going to hold my zombie strings in a string array
        string[] phrases; // going to hold my phrases in a string array
        Random rgen;
        int num; // Using the variable to determine how many phrases there are in the file
        int fCount; // Using this variable to tell how many files are in my ascii art folder
        string[] textName; // going to hold the names of my ascii art files in a string array

        public ZombieData()
        {
            rgen = new Random();
            /*
            zombies = new string[];
            phrases = new string [];*/
        }

        /// <summary>
        /// This method should load in all the phrases i have in a text file to a 
        /// string array
        /// </summary>
        /// <param name="file">a file that is holding all the phrases in</param>
        /// <returns>returns true if load was successful or false if there was an error</returns>
        public bool LoadPhrases(string file)
        {
            // Created 2 stream readers to count the number of phrases
            // and to store them into the array
            StreamReader input = null;
            StreamReader input2 = null;

            string line = null;
            num = 0;

            try
            {
                //intialized my stream reader
                input = new StreamReader(file);
                while ((line = input.ReadLine()) != null)
                {     
                    // Looping through to determine how many phrase i have in the file
                    // and storing it in num
                    num += 1;
                }

                // Closing the stream reader i just looped through
                input.Close();

                // Now i am intializing my phrases array with the exact ammount of slots
                // needed to store my phrases in
                phrases = new string[num];

                //intialized my second stream reader
                input2 = new StreamReader(file);
                // I also reset my num variable to 0 so i can use it again for pluging
                //in my phrases into the array
                num = 0;


                while ((line = input2.ReadLine()) != null)
                {
                    // Plugging my phrases into the array and incrementing num by 1 for the next loop
                    phrases[num] = line;
                    num += 1;
                }

                // Console.WriteLine("Load phrases completed ^.^"); <--------just to make sure i knew it finished loading everything
                return true;

            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                return false;

            }
            finally
            {
                if (input2 != null)
                {
                    input2.Close();
                }
            }
        }

        /// <summary>
        /// This method should load in all the zombies ascii art from the ascii art
        /// folder
        /// </summary>
        /// <param name="file">a floder containing all the ascii art</param>
        /// <returns>returns true if load was successful or false if there was an error</returns>
        public bool LoadZombies(string file)
        {
            // Creating my Stream Reader
            StreamReader input = null;
            string line = null;
            try
            {
                // This line of code tells me how files do i have in a folder and stores it in fcount
                fCount = Directory.GetFiles(file, "*", SearchOption.TopDirectoryOnly).Length;

                // Now this line of code gives me the names of all the files in a folder and stores them 
                //as an array of strings in textName
                textName = Directory.GetFiles(file, "*", SearchOption.TopDirectoryOnly);

                // Here i use fcount to determine how many slots my zombies array needs and intializes it
                zombies = new string[fCount];

                // This loop is for storing zombies in the array
                for (int i = 0; i < fCount; i++)
                {
                    input = new StreamReader(textName[i]);

                    // This loop is to get all the lines in the file to become one string to put into the array
                    while ((line = input.ReadLine()) != null)
                    {
                        zombies[i] += line + "\n";
                    }
                    input.Close();
                }
                //Console.WriteLine("Load zambies complete");<------------- let me know if it was working or not

                    return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                return false;
            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }

            }
        }
        
        /// <summary>
        /// This method should randomly chose a phrase from the string array holding all the phrases
        /// </summary>
        /// <returns>returns a phrase</returns>
        public string RandomPhrase()
        {
            int ranNum = rgen.Next(0, num); // creates a random number
            return phrases[ranNum];// returns that random phrase string
        }
        
        /// <summary>
        /// This method should randomly chose a zombie ascii art from the string array holding all
        /// the zombie ascii art.
        /// </summary>
        /// <returns>returns a string of ascii art</returns>
        public string RandomZombie()
        {

            // Generates a random number
            int ranNum = rgen.Next(0, fCount);

            // Here i added alittle personal touch to the zombies to give each
            // zombie different personalities to match how they look
            switch(ranNum)
            {
                case 0:
                    {
                        return  "A angry Zombie has appeared!!\n" + zombies[ranNum];
                        
                    }
                case 1:
                    {
                        return "A disappointed mother has appeared, you forget to take out the trash. Kill her!!\n" + zombies[ranNum];
                        
                    }
                case 2:
                    {
                        return "A very disappointed zombie has stumbled in front of you\n" + zombies[ranNum];
                        
                    }
                case 3:
                    {
                        return "An excited zombie is sprinting straight for you \n" + zombies[ranNum];
                       
                    }
                case 4:
                    {
                        return "A very happy zombie has appeared before you, I thinks it's on drugs....\n" + zombies[ranNum];
                        
                    }
                case 5:
                    {
                        return "A really small zombie wanted to pick a fight with you!!!\n" + zombies[ranNum];
                        
                    }
                case 6:
                    {
                        return "A very uninterested zombie kind of wants to fight you but shows no signs of motivation or deteremination\n" + zombies[ranNum];
                    }
                default:
                    {
                        return zombies[ranNum];
                    }
            }
            
        }

    }
}
