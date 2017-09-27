using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculations
{
    public class TaxBracket
    {
        decimal _lowerBound = 0;
        decimal _upperBound = 0;
        decimal _percentDue = 0;


        // default constuctor assumes 0 tax due
        public TaxBracket()
        {
            ValidateInput();
        }

        public TaxBracket(decimal lowerTaxBound, decimal upperBound, decimal percentDue)
        {
            _lowerBound = lowerTaxBound;
            _upperBound = upperBound;
            _percentDue = percentDue;

            ValidateInput();
        }

        public decimal GetTaxDue(decimal income)
        {
            // sanity check
            Debug.Assert((_lowerBound >= 0 && _upperBound >= 0 && _percentDue >= 0 && _lowerBound <= _upperBound));

            // income is below tax bracket so don't tax
            if (income < _lowerBound || income < 0) return 0;

            // income is right at the threshold so return max due
            if (income >= _upperBound) return (_upperBound - _lowerBound) * _percentDue;

            // income less than the higher bound so return proportion due
            return (income - _lowerBound) * _percentDue;
        }

        private void ValidateInput()
        {
            if (!(_lowerBound >= 0 && _upperBound >= 0 && _percentDue >= 0 && _percentDue <= 1 && _lowerBound <= _upperBound))
            {
                throw new ArgumentOutOfRangeException("Incorrect input: Tax Bracket cannot have negative upper or lower bound, percentage outside (0-1) or lower bound higher than upper bound!");
            }
        }
    }
}