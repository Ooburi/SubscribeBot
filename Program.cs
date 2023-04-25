using System;

namespace SubscribeBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bot.Get();

            Console.WriteLine("Бот запущен!");
            Console.ReadLine();
        }
    }
}
