using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculations;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class TaxCalculatorUnitTest
    {
        const decimal eps = 0.000001m;

        // exception cases
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Negative income shouldn't be allowed.")]
        public void TaxCalculator_NegativeIncome()
        {
            TaxCalculator calculator = new TaxCalculator(-1000);
        }

        // default Tax Brackets
        [TestMethod]
        public void TaxCalculator_IncomeAtTaxableDefault()
        {
            TaxCalculator calculator = new TaxCalculator(1000);
            decimal actual = calculator.GetTotalTaxDue();

            Assert.IsTrue(0m - actual < eps);
        }

        [TestMethod]
        public void TaxCalculator_IncomeAboveTaxableDefault()
        {
            TaxCalculator calculator = new TaxCalculator(1000.01m);
            decimal actual = calculator.GetTotalTaxDue();

            Assert.IsTrue(0.001m - actual < eps);
        }

        [TestMethod]
        public void TaxCalculator_IncomeBelowTaxableDefault()
        {
            TaxCalculator calculator = new TaxCalculator(500m);
            decimal actual = calculator.GetTotalTaxDue();

            Assert.IsTrue(0m - actual < eps);
        }

        [TestMethod]
        public void TaxCalculator_ZeroIncome()
        {
            TaxCalculator calculator = new TaxCalculator(0);
            decimal actual = calculator.GetTotalTaxDue();

            Assert.IsTrue(0m - actual < eps);
        }

        // overlapping Tax Brackets
        [TestMethod]
        public void TaxCalculator_OverlappingBrackets()
        {
            List<TaxBracket> brackets = new List<TaxBracket> {
                new TaxBracket(0, 10, 0.10m),
                new TaxBracket(5, 15, 0.20m),
                new TaxBracket(10, 20, 0.10m)};
            TaxCalculator calculator = new TaxCalculator(1000, brackets);
            decimal actual = calculator.GetTotalTaxDue();

            Assert.IsTrue(4 - actual < eps);
        }

        [TestMethod]
        public void TaxCalculator_GappyBrackets()
        {
            List<TaxBracket> brackets = new List<TaxBracket> {
                new TaxBracket(0, 5, 0.10m),
                new TaxBracket(10, 20, 0.10m)};
            TaxCalculator calculator = new TaxCalculator(1000, brackets);
            decimal actual = calculator.GetTotalTaxDue();

            Assert.IsTrue(1.5m - actual < eps);
        }

        [TestMethod]
        public void TaxCalculator_IncomeBelowLowestBrackets()
        {
            List<TaxBracket> brackets = new List<TaxBracket> {
                new TaxBracket(1000, 5000, 0.10m),
                new TaxBracket(5000, 6000, 0.10m)};
            TaxCalculator calculator = new TaxCalculator(999, brackets);
            decimal actual = calculator.GetTotalTaxDue();

            Assert.IsTrue(0 - actual < eps);
        }

        [TestMethod]
        public void TaxCalculator_IncomeAtLowestBrackets()
        {
            List<TaxBracket> brackets = new List<TaxBracket> {
                new TaxBracket(1000, 5000, 0.10m),
                new TaxBracket(5000, 6000, 0.10m)};
            TaxCalculator calculator = new TaxCalculator(1000, brackets);
            decimal actual = calculator.GetTotalTaxDue();

            Assert.IsTrue(0 - actual < eps);
        }

        [TestMethod]
        public void TaxCalculator_IncomeAboveHighestBrackets()
        {
            List<TaxBracket> brackets = new List<TaxBracket> {
                new TaxBracket(0, 10, 0.10m),
                new TaxBracket(10, 20, 0.20m)};
            TaxCalculator calculator = new TaxCalculator(25, brackets);
            decimal actual = calculator.GetTotalTaxDue();

            Assert.IsTrue(3 - actual < eps);
        }

    }
}
