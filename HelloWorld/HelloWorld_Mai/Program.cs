using System.ComponentModel.DataAnnotations;

namespace HelloWorld_Mai
{
    internal class Program
    {
        private static object? age;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //initialize object
            Card aCard = new Card("Jack", "Spade");

            //call object methods
            aCard.PrintDetails();


            static double Average(int[] numbers)
            {
                int sum = 0;
                for (int i =0; i < numbers.Length; i++)
                {
                    sum += numbers[i];
                }
                double average = (double)sum / (numbers.Length);
                return average;
            }



        }
    }
}
