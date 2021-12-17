using System.Threading.Tasks;

namespace DicordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bot = new DiscordBot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
