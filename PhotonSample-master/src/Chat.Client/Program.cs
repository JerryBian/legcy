using System;
using Chat.Client.Operations;

namespace Chat.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            while (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.WriteLine("==========================");
                var obj = new ChatClient();
                obj.Execute();
            }

            Console.ReadKey();
        }
    }
}
