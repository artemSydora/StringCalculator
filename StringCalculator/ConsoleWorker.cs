using System;

namespace StringCalculator
{
    public class ConsoleWorker
    {
        private readonly Calculator _calculator;
        private readonly CustomConsole _console;

        public ConsoleWorker(Calculator calculator, CustomConsole console)
        {
            _calculator = calculator;
            _console = console;
        }

        public void Run()
        {
            _console.WriteLine("Enter comma separated numbers (enter to exit)");

            var input = _console.ReadLine();
            var result = 0;

            while (!string.IsNullOrEmpty(input))
            {
                try
                {
                    result = _calculator.Add(input);
                }
                catch (ArgumentException ex)
                {
                    _console.WriteLine(ex.Message);
                }

                _console.WriteLine($"Result: {result}");
                _console.WriteLine("You can enter other numbers(enter to exit)?");

                input = _console.ReadLine();
            }
        }
    }
}
