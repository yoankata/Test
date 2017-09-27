using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculations
{
    public class TaxCalculator
    {
        // default TaxBrackets as requested
        List<TaxBracket> TaxBrackets = new List<TaxBracket> { // non taxable income <=1000,
                                        new TaxBracket(1000, 3000, 0.15m), // social contributions of 15% for income b/w 1000-3000
                                        new TaxBracket(1000, decimal.MaxValue, 0.10m) }; // income tax applied to income over 1000
        decimal _income = 0;

        public TaxCalculator(decimal income, List<TaxBracket> taxBrackets = null)
        {
            // if tax brackets are set override the default settings
            if (taxBrackets != null)
            {
                TaxBrackets = new List<TaxBracket>(taxBrackets);
            }

            // sanity check
            if (income < 0)
            {
                throw new ArgumentOutOfRangeException("Input error: Tax Calculator passed negative income!");
            }

            _income = income;
        }

        public decimal GetTotalTaxDue()
        {
            decimal totalTax = 0;

            foreach (var bracket in TaxBrackets)
            {
                totalTax += bracket.GetTaxDue(_income);
            }

            return totalTax;
        }
    }
}

