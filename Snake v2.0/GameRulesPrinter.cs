using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_v2._0
{
    public class GameRulesPrinter
    {
        internal static void PrintGameRules() //bajzel tu jest straszny z tymi "\n" ale w sumie nie wiedziałem jak to inaczej ładnie zrobić żeby mi się słowa nie dzieliły brzydko
        {
            Console.BufferWidth = Console.WindowWidth;
            Console.Clear();

            Console.WriteLine("Hello! I'm happy and honoured that you've chosen to play the very first game I've" +
                " ever made - a recreation of the famous Snake game (to which [obviously] I have no rights whatsoever)." +
                " I have added a few implementations and features you can" + "\n" + "toggle on and off, or choose from, in order to make" +
                " the game a bit more fun. " + "\n" + "And thus, you can:" + "\n");

            Console.WriteLine("Choose from map sizes:");
            Console.WriteLine("Small:  20 x 30");
            Console.WriteLine("Medium: 30 x 45");
            Console.WriteLine("Large:  40 x 60");
            Console.WriteLine("Huge:   40 x 120" + "\n");

            Console.WriteLine("Choose from difficulties:");
            Console.WriteLine("Easy: Slower starting speed, snake goes faster every 7 eaten normal preys");
            Console.WriteLine("Medium: Faster starting speed, snake goes faster every 5 eaten normal preys");
            Console.WriteLine("Hard: Same as medium, but snake goes faster every 3 eaten normal preys" + "\n");

            Console.WriteLine("Oh, and once every while, a special kind of prey shows up, and it can earn you" + "\n" + "bonus points!" +
                " It's marked as an \"S\" sign (as opposed to an \"O\" sign for normal" + "\n" + "prey). First special prey shows up" +
                " after 20-40 seconds from starting new game," + "\n" + "and every next one, after 15-50 seconds. If you manage to eat it, you can" +
                " get" + "\n" + "from 1 (meh) up to even 20 (WOW!) points! Isn't it cool? :D" + "\n");

            Console.WriteLine("But wait, there's more! I've mentioned that you can toggle some features on and" + "\n" + "off, and these are:" +
                " speeding up snake moves after eating every 7/5/3 preys," + "\n" + "generating walls that spawn randomly on the board, growth of snake after eating a prey," +
                " generating special prey at all and teleporting through board edges! Wow!" + "\n" + "\"Normal\" Snake never had this many options" +
                " (at least the one I've used to play" + "\n" + "long time ago, hehe)" + "\n");

            Console.WriteLine("So, please, go for it and have fun with the game! (and press any key to go back" + "\n" + "to main menu)" + "\n");

            Console.WriteLine("Przegrałem");

            Console.ReadKey();
        }
    }
}
