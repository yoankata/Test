using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculations;

namespace TaxCalculations
{
    class Program
    {
        static void Main(string[] args)
        {
            // print greeting to user
            Console.WriteLine("|-------------------------------------------------------------------------------------------------------------|");
            Console.WriteLine("This is a Tax Calculator for calculating the total income and social security tax due and net take home income:");
            Console.WriteLine("***Non-taxable income is 1000 IND.");
            Console.WriteLine("***Income tax of 10% is applied on income over 1000 IND.");
            Console.WriteLine("***Social security contributions are applied to income between 1000 and 3000 IND!");
            Console.WriteLine("|-------------------------------------------------------------------------------------------------------------|");
            Console.WriteLine("Type 'x' to exit.");

            // get input for calculation
            while (true)
            {
                decimal income;

                Console.WriteLine("Please, enter before tax annual income in IND:");
                string line = Console.ReadLine(); // Get string from user
                if (line == "x") // Check string
                {
                    return; //exit program
                }
                else if (decimal.TryParse(line, out income) && income >= 0)
                {
                    new TaxBracket(-10, 10, 0);

                    // input is correct, calculate tax
                    TaxCalculator taxCalculator = new TaxCalculator(income);
                    decimal totalTaxDue = taxCalculator.GetTotalTaxDue();
                    decimal netIncome = income - totalTaxDue;

                    Console.WriteLine("Total tax due for annual income of " + income + " is: " + totalTaxDue + ".");
                    Console.WriteLine("Net annual income is: " + netIncome);

                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Please enter a valid income or 'x' to exit:");
                }



            }
        }
    }
}