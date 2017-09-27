using System;

using TaxCalculations;

namespace TaxCalculations
{
    class Program
    {
        static void Main(string[] args)
        {
            // print greeting to user
            Console.WriteLine("|--------------------------------------------------------------------------|");
            Console.WriteLine("This is a Tax Calculator for income and social security tax due and net income:");
            Console.WriteLine("  ***Tax-free income is 1000 IND.");
            Console.WriteLine("  ***Income tax of 10% is applied on income over 1000 IND.");
            Console.WriteLine("  ***Social security is applied to income between 1000 and 3000 IND.");
            Console.WriteLine("|---------------------------------------------------------------------------|");
            Console.WriteLine("Type 'x' to exit.");

            string prompt = "Please enter a valid income or 'x' to exit:";

            // get input and calculate
            while (true)
            {
                Console.WriteLine(prompt);
                string line = Console.ReadLine(); // Get string from user

                if (line == "x") // Check string
                {
                    return; //exit program
                }
                else if (decimal.TryParse(line, out decimal income) && income >= 0)
                {
                    // input is correct, calculate tax
                    TaxCalculator taxCalculator = new TaxCalculator(income);
                    decimal totalTaxDue = taxCalculator.GetTotalTaxDue();
                    decimal netIncome = income - totalTaxDue;

                    Console.WriteLine("|--------------------------------------------------------------------------|");
                    Console.WriteLine("Total tax due is:  " + string.Format("{0:N}", totalTaxDue) + " IND");
                    Console.WriteLine("Net income is:     " + string.Format("{0:N}", netIncome) + " IND");
                    Console.WriteLine("|--------------------------------------------------------------------------|");
                }
                else
                {
                    Console.WriteLine(prompt);
                }



            }
        }
    }
}
