/* **********************************************************
 * Lg Lg n
 * Cora Boyoung Jung
 * Outputting the floor of lglg(n) for user given input
 ***********************************************************/

using System;

namespace ProgramLogLogN
{
    class ProgramLogLogN
    {
        // function asking the user to enter a number 
        static void Main(string[] args)
        {
            // welcoming message
            Console.WriteLine("Welcome to CS 212 Program01, Lg(Lg(n))! ");

            while (true)
            {
                Console.Write("Enter a number (n): ");

                // reading user input value
                int n = int.Parse(Console.ReadLine());

                // computing the lglgn with the input value
                int n2 = (Lg(Lg(n)));


                // outputting the result to the user
                Console.WriteLine("The floor of lg(lg({0})) is {1}.", n, n2);
                Console.WriteLine('\n');

                // giving chance to quit
                Console.WriteLine("Enter 'q' to quit, else enter any other keys.");
                Console.WriteLine('\n');

                //quitting
                string quit = Console.ReadLine();
                if (quit == "q")
                {
                    break;
                }

            }
        }


        // function computing the lg of n
        static int Lg(int n)
        {
            // initializing the result
            int result = 0;

            // n1 has to be greater than 1
            while (n > 1)
            {
                n /= 2;
                result++;
                Console.WriteLine(n);
            }

            // returning the result
            return result;
        }
    }
}