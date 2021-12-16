using System;

namespace DicordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new DiscordBot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
