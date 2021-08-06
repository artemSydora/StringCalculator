namespace StringCalculator
{
    class Program
    {
        public static void Main()
        {
            var consoleWorker = new ConsoleWorker(new Calculator(), new CustomConsole());
            consoleWorker.Run();
        }
    }
}
