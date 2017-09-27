using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxCalculations;

namespace Tests
{
    [TestClass]
    public class TaxBracketUnitTests
    {
        const decimal eps = 0.000001m;

        // boundary and regular cases
        [TestMethod]
        public void TaxBracket_ZeroPercentageDue()
        {
            TaxBracket bracket = new TaxBracket(1, 10, 0);
            decimal actual = bracket.GetTaxDue(10);

            Assert.IsTrue(0m - actual < eps);
        }

        [TestMethod]
        public void TaxBracket_ZeroBracketRangeDue()
        {
            TaxBracket bracket = new TaxBracket(10, 10, 0.10m);
            decimal actual = bracket.GetTaxDue(10);

            Assert.IsTrue(0m - actual < eps);
        }


        [TestMethod]
        public void TaxBracket_ZeroIncome()
        {
            TaxBracket bracket = new TaxBracket(10, 20, 0.10m);
            decimal actual = bracket.GetTaxDue(0);

            Assert.IsTrue(0m - actual < eps);
        }

        [TestMethod]
        public void TaxBracket_0PcDue()
        {
            TaxBracket bracket = new TaxBracket(0, 10, 0m);
            decimal actual = bracket.GetTaxDue(10);

            Assert.IsTrue(0m - actual < eps);
        }


        [TestMethod]
        public void TaxBracket_10PcDue()
        {
            TaxBracket bracket = new TaxBracket(0, 10, 0.10m);
            decimal actual = bracket.GetTaxDue(10);

            Assert.IsTrue(1m - actual < eps);
        }


        [TestMethod]
        public void TaxBracket_50PcDue()
        {
            TaxBracket bracket = new TaxBracket(0, 10, 0.50m);
            decimal actual = bracket.GetTaxDue(10);

            Assert.IsTrue(5m - actual < eps);
        }


        [TestMethod]
        public void TaxBracket_100PcDue()
        {
            TaxBracket bracket = new TaxBracket(0, 10, 1.0m);
            decimal actual = bracket.GetTaxDue(10);

            Assert.IsTrue(10m - actual < eps);
        }


        [TestMethod]
        public void TaxBracket_IncomeBelowBracket()
        {
            TaxBracket bracket = new TaxBracket(10, 20, 0.10m);
            decimal actual = bracket.GetTaxDue(5);

            Assert.IsTrue(0 - actual < eps);
        }

        [TestMethod]
        public void TaxBracket_IncomeAboveBracket()
        {
            TaxBracket bracket = new TaxBracket(0, 10, 0.10m);
            decimal actual = bracket.GetTaxDue(20);

            Assert.IsTrue(1m - actual < eps);
        }

        [TestMethod]
        public void TaxBracket_IncomeInBracket()
        {
            TaxBracket bracket = new TaxBracket(0, 10, 0.10m);
            decimal actual = bracket.GetTaxDue(5);

            Assert.IsTrue(0.5m - actual < eps);
        }

        // exception cases
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Negative lower bound shouldn't be allowed.")]
        public void TaxBracket_NegativeLowerBound()
        {
            TaxBracket bracket = new TaxBracket(-10, 10, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Negative upper bound shouldn't be allowed.")]
        public void TaxBracket_NegativeUpperBound()
        {
            TaxBracket bracket = new TaxBracket(10, -10, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Negative percent due shouldn't be allowed.")]
        public void TaxBracket_NegativePercentDue()
        {
            TaxBracket bracket = new TaxBracket(10, 11, -0.10m);
        }

        // technically tax over 100% can exist and it does but we are assuming this isn't the case
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Percent due shouldn't be >1.")]
        public void TaxBracket_Over100PercentDue()
        {
            TaxBracket bracket = new TaxBracket(10, 11, 1.5m);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Lower bound in bracket shouldn't be higher than upper.")]
        public void TaxBracket_LowerBoundHigherThanUpper()
        {
            TaxBracket bracket = new TaxBracket(20, 10, 0.10m);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Negative income shouldn't be allowed.")]
        public void TaxBracket_NegativeIncome()
        {
            TaxBracket bracket = new TaxBracket(20, 10, 0.10m);
            bracket.GetTaxDue(-1000);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Negative upper, lower bound and percentage due shouldn't be allowed.")]
        public void TaxBracket_NegativeUpperLowerBoundAndPercentage()
        {
            TaxBracket bracket = new TaxBracket(-20, -10, -0.10m);
        }


    }
    
}
