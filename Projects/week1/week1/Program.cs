using System;
class Program
{
    static double Average(double[] numbers)
    {
        double sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }
        return sum / numbers.Length;
    }
    static void Main()
    {
        //step 5a: inline instantiation of the dataset
        double[] data = { 2.5, -1.4, -7.2, -11.7, -13.5, -13.5, -14.9, -15.2, -14.0, -9.7, -2.6, 2.1 };

        //step 5b: invoke the function
        double avg = Math.Round(Average(data), 0, MidpointRounding.AwayFromZero);

        //step 5c: print result
        Console.WriteLine("The average is: " + avg);

        //step 5d: print name and student ID
        Console.WriteLine("Student Name: Nguyen Xuan Nang Mai");
        Console.WriteLine("Student ID: 105549980");

        //step 8: basic interpretation
        if (avg > 0)
        {
            Console.WriteLine("Average value positive");
        }
        else if (avg == 0)
        {
            Console.WriteLine("Average value neutral");
        }
        //step 10: average is negative
        else
        {
            Console.WriteLine("Average value negative");
        }
        
        //step 9: check if single/multiple digits
        if (Math.Abs(avg) >= 10)
        {
            Console.WriteLine("Multiple digits");
        }
        else
        {
            Console.WriteLine("Single digit");
        }

        //step 11: compare the last digit of average and the last digit of student ID
        int lastDigitAvg = (int)Math.Abs(avg) % 10;
        int lastDigitID = 105549980 % 10;

        if (lastDigitAvg > lastDigitID)
        {
            Console.WriteLine("Larger than my last digit");
        }
        else if (lastDigitAvg == lastDigitID)
        {
            Console.WriteLine("Equal to my last digit");
        }
        else
        {
            Console.WriteLine("Smaller than my last digit");
        }
    }
}
