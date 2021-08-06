using System;

namespace StringCalculator
{
    public class CustomConsole
    {
        public virtual void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
