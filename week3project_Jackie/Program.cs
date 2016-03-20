using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week3Project
{
    class Program
    {
        static void Main(string[] args)
        {
            bool answer = true; //need this so while loop won't continue if user doesn't want it to
            string response = "";

            Console.WriteLine("The Fortune Teller welcomes you.");

            do      //where program restarts if user makes it to end of game and wants to continue
            {
                restart:    //where program restarts if user enters "restart"

                Console.WriteLine("\nPlease be nice and enter your first name:  ");
                string userFirstName = Console.ReadLine();  //saves user's input for first name
                string fullName = userFirstName + " ";    //begins concatinating user's uncleaned first name to variable for the user's full name
                userFirstName = inputCleaner(userFirstName);    //need to clean first name so programEnder and restart work
                programEnder(userFirstName);    //ends game if user entered "quit"
                if (userFirstName == "restart")       //restarts game at line 19 if user entered "restart"
                {
                    goto restart;
                }

                Console.WriteLine("Ok, now please enter your last name:  ");
                string userLastName = Console.ReadLine();       //saves user's input for last name
                fullName += userLastName;   //adds user's uncleaned last name to the string called fullName. need it to print out name in fortune.
                userLastName = inputCleaner(userLastName);  //need a cleaned last name so that programEnder and restart work
                programEnder(userLastName);
                if (userLastName == "restart")
                {
                    goto restart;
                }

                string yearsRetired = ageToYearsRetire();   //runs method that returns years until retirement based on age. used later when printing fortune
                if (inputCleaner(yearsRetired) == "restart")
                {
                    goto restart;
                }

                string moneyITB = monthToMoneyITB(inputCleaner(fullName));  //runs method that uses both cleaned full name and birth month and returns moneyITB
                if (moneyITB == "restart")  //don't need to check if they entered "quit" because it's in the method
                {
                    goto restart;   //restarts program if user entered "restart" when asked their birth month
                }

                string transport = colorToTransport();      //runs method that returns type of transporation based on favorite color. will be used when printing fortune
                if (inputCleaner(transport) == "restart")   //don't need to check if input was "quit" here because it checks inside colorToTransport method
                {
                    goto restart;
                }

                string location = siblingsToLocation();     //runs method that returns location based on # of siblings. will be used later when printing fortune
                if (location == "restart")  //don't need to check if they entered "quit" because its in the method
                {
                    goto restart;   //restarts program if they entered "restart" when asked number of siblings
                }

                //adds a separating space and then prints out fortune
                Console.WriteLine();
                Console.WriteLine(fullName + " will retire in " + yearsRetired + " years with $" + moneyITB + " in the bank, a vacation home in " + location + " and a " + transport + ".");
                
                //runs fortune teller again if user wants another turn
                Console.WriteLine("\nWould you like to start over? ");
                response = inputCleaner(Console.ReadLine());
                if (response == "no" )
                {
                    answer = false;
                }
            }
            //game will repeat if answer is not "no"
            while (answer);
        }

        static string inputCleaner(string userInput)        //cleans up user input if there are spaces and makes input lowercase
        {
            userInput = userInput.ToLower();
            userInput = userInput.Trim();
            return userInput;
        }

        static void programEnder(string userInput)      //exits program if user inputs "quit"
        {
            if (inputCleaner(userInput) == "quit")
            {
                Console.WriteLine("Nobody likes a quitter...");
                Environment.Exit(0);
            }
        }

        static string ageToYearsRetire()    //after asking for user's first and last name, returns years til retirement based on user's age
        {
            Console.WriteLine("What an awesome name! Now please enter your age:  ");
            string inputAge = inputCleaner(Console.ReadLine());
            programEnder(inputAge); //ends program if user entered "quit" when asked age
            if (inputAge == "restart")
            {
                return inputAge;
            }
            int userAge = int.Parse(inputAge);        //makes user input an int so we can do math

            string[] yearsRetiredArray = { "15", "30" };    //2 scenarios for number of years until retirement
            
            if (userAge % 2 == 1)
                return yearsRetiredArray[0];    //if user's age is odd
            else
                return yearsRetiredArray[1];    //if user's age is even
        }

        static string monthToMoneyITB(string name)      //after getting user's age, returns money in the bank based on user's birth month
        {
            Console.WriteLine("Oooo, what an interesting age, now what month were you born?  ");
            string userBirthMonth = inputCleaner(Console.ReadLine());   //cleans and saves user's input for birth month
            programEnder(userBirthMonth);   //ends program if they entered "quit"
            if (userBirthMonth == "restart")
            {
                return userBirthMonth;
            }
            while ("januaryfebruarymarchaprilmayjunejulyaugustseptemberoctobernovemberdecember".Contains(userBirthMonth) == false)   //if user didn't actually enter birth month
            {//loop doesn't give user change to restart or quit
                Console.WriteLine("Try again.\nWhat month were you born?");
                userBirthMonth = inputCleaner(Console.ReadLine());  //gives user another chance to enter birth month
           }

            string[] moneyITBArray = { "2.50", "100,000", "500,000", "1,000,000" }; //4 different scenarios for amount of money in the bank
            
            foreach (char letter in name)   //loops through each character in the user's firsta and last name
            {
                if (userBirthMonth[0] == letter)    //first letter of birth month is in name
                {
                    return moneyITBArray[0];
                }
                if (userBirthMonth[1] == letter)    //second letter of birth month in name
                {
                    return moneyITBArray[1];
                }
                if (userBirthMonth[2] == letter)    //third letter of birth month in name
                {
                    return moneyITBArray[2];
                }
            }
            return moneyITBArray[3];    //if the first three letter of birth month are not in name
        }

        static string colorToTransport()    //after entering birth month, returns type of transportation based on user's favorite color
        {
            Console.WriteLine("Now, tell the sweet little Fortune Teller your favorite ROYGBIV color:  ");
            Console.WriteLine("If you need help type \"Help\"");
            string userColor = inputCleaner(Console.ReadLine());
            programEnder(userColor);
            if (userColor == "restart")
            {
                return userColor;
            }

            while (userColor == "help") //if user entered "help", lists ROYGBIV colors and reassigns userColor. won't work if user enters restart or quit
            {
                Console.WriteLine("Red, Orange, Yellow, Green, Blue, Indigo, Violet");
                Console.WriteLine("Now, did that help? Please enter ROYGBIV: ");
                userColor = inputCleaner(Console.ReadLine());
            }

            string[] transportArray = { "pink bicycle", "vespa", "SUV", "segway", "private jet", "skateboard", "wheel barrel" };    //7 scenarios for type of transportation

            string[] colorArray = { "red", "orange", "yellow", "green", "blue", "indigo", "violet" };

            for (int i = 0; i < 7; i++)
            {
                if (userColor == colorArray[i]) //checks where user's favorite color's position is in colorArray
                {
                    return transportArray[i];
                }
            }

            Console.WriteLine("Try again.");
            return colorToTransport();    //if the user didn't enter a ROYGIBIV color, starts method over
        }

        static string siblingsToLocation()  //after getting user's favorite color, returns their retirement location based on number of siblings
        {
            Console.WriteLine("How many siblings do you have my dear? ");
            string sibsString = inputCleaner(Console.ReadLine());
            programEnder(sibsString);   //ends program if user entered "quit"
            if (sibsString == "restart")
            {
                return sibsString;
            }
            int numberOfSibling = int.Parse(sibsString);    //save's user's input as an int so switch case can be used
            
            string[] locationArray = { "Portland", "Borneo", "Belize", "Parma", "North Pole" };     //5 different scenarios for retirement location

            switch (numberOfSibling)
            {
                case 0:
                    return locationArray[0];    //if user doesn't have a sibling
                case 1:
                    return locationArray[1];    //1 sibling
                case 2:
                    return locationArray[2];    //2 siblings
                case 3:
                    return locationArray[3];    //3 siblings
                default:
                    return locationArray[4];    //user has more than 3 siblings
            }
        }
    }
}
