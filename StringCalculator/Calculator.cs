using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public virtual int Add(string numbers)
        {
            if (numbers.Equals(string.Empty))
            {
                return 0;
            }

            var nums = ParseString(numbers);

            VerifyNumbers(nums);

            var result = nums.Where(x => x <= 1000).Sum();

            return result;
        }

        private string[] GetDelimeters(string numbers)
        {
            var delimeters = new List<string> { ",", @"\n" };

            if (!numbers.Contains(@"\n"))
            {
                return delimeters.ToArray();
            }

            var indexOfCommand = 0;
            var command = numbers.Split(@"\n")[indexOfCommand];

            if (command.StartsWith("//["))
            {
                var indexOfCustomDelimeter = command.IndexOf('[') + 1;
                var lengthOfCustomDelimeter = command.LastIndexOf(']') - indexOfCustomDelimeter;

                foreach (var customDelimeter in command.Substring(indexOfCustomDelimeter, lengthOfCustomDelimeter).Split("]["))
                {
                    delimeters.Add(customDelimeter);
                }
            }
            else if (command.StartsWith("//"))
            {
                var indexOfDelimeter = 2;

                delimeters.Add(command[indexOfDelimeter].ToString());
            }

            return delimeters.ToArray();
        }

        private IEnumerable<int> ParseString(string input)
        {
            var delimeters = GetDelimeters(input);
            var numbers = new List<int>();

            foreach (var item in input.Split(delimeters, StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(item, out int number))
                {
                    numbers.Add(number);
                }
            }

            return numbers;
        }

        private void VerifyNumbers(IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(x => x < 0);

            if (negativeNumbers.Any())
            {
                var errorMessage = "negatives not allowed - " + string.Join(" ", negativeNumbers);

                throw new ArgumentException(errorMessage);
            }
        }
    }
}
