using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicordBot 
{
    class BotCommands : BaseCommandModule
    {
        private Translator translator = new Translator();

        [Command("trans")]
        public async Task Translate(CommandContext ctx, string s)
        {
            // var res = String.Join(" ", feedback);
            var result = await translator.Trans(s);
            await ctx.Channel.SendMessageAsync(result);
        }
    }
}
