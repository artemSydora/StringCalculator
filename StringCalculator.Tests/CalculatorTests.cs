using System;
using System.Text;
using Xunit;

namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator = null;

        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void Add_InputStringIsEmpty_ShouldReturn0()
        {
            //arrange
            var numbers = string.Empty;

            //act
            int actual = _calculator.Add(numbers);

            //assert
            Assert.Equal(0, actual);
        }

        [Fact]
        public void Add_InputStringWith1Number_ShouldReturnThisNumber()
        {
            //arrange
            var numbers = "1";

            //act
            int actual = _calculator.Add(numbers);

            //assert
            Assert.Equal(1, actual);
        }

        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("1,2,3,4,5", 15)]
        public void Add_InputStringWithAmountNumbers_ShouldReturnSumOfAllNumbers(string numbers, int expected)
        {
            //act
            int actual = _calculator.Add(numbers);

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_ShouldSupportNewLineDelimeter()
        {
            //arrange
            var numbers = @"1\n2,3";

            //act
            int actual = _calculator.Add(numbers);

            //assert
            Assert.Equal(6, actual);
        }

        [Fact]
        public void Add_InputStringWithNegativeNumbers_ShouldThrowArgumentException()
        {
            //arrange
            var numbers = "-1,2,-3,4,-5";
            var expected = "negatives not allowed - -1 -3 -5";
  
            //act
            Action act = () => _calculator.Add(numbers);

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public void Add_ShouldIgnoreNumbersBiggerThan1000()
        {
            //arrange
            var numbers = "1,2,1000,2000";

            //act
            int actual = _calculator.Add(numbers);

            //assert
            Assert.Equal(1003, actual);
        }

        [Fact]
        public void Add_ShouldSupportCustomCharDelimeter()
        {
            //arrange
            var numbers = @"//[&]\n1&2";

            //act
            int actual = _calculator.Add(numbers);

            //assert
            Assert.Equal(3, actual);
        }

        [Fact]
        public void Add_ShouldSupportCustomDelimeterOfAnyLength()
        {
            //arrange
            var numbers = @"//[***]]\n1***]2";

            //act
            int actual = _calculator.Add(numbers);

            //assert
            Assert.Equal(3, actual);
        }

        [Fact]
        public void Add_ShouldSupportCustomMultipleCharDelimeters()
        {
            //arrange
            var numbers = @"//[*][&]\n1*2&3";

            //act
            int actual = _calculator.Add(numbers);

            //assert
            Assert.Equal(6, actual);
        }

        [Fact]
        public void Add_ShouldSupportCustomMultipleDelimetersOfAnyLength()
        {
            //arrange
            var numbers = @"//[***][&&&]\n1***2&&&3";

            //act
            int actual = _calculator.Add(numbers);

            //assert
            Assert.Equal(6, actual);
        }
    }
}
