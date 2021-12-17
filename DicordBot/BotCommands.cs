using System;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DicordBot 
{
    class BotCommands : BaseCommandModule
    {
        [Command("t")]
        public async Task Translate(CommandContext ctx,params string[] s)
        {
            var translations = await Translator.Translate(string.Join(" ", s));

            string message = "";

            foreach (var translationPair in translations)
                message += $"{translationPair.Key}:\n{translationPair.Value}\n";
            
            await new DiscordMessageBuilder()
                .WithContent(message)
                .SendAsync(ctx.Channel);
        }
    }
}
