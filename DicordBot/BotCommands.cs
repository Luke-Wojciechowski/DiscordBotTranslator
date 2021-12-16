﻿using DSharpPlus.CommandsNext;
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
        public async Task Translate(CommandContext ctx,params string[] s)
        {
            var result = await translator.Trans(string.Join(" ", s));
            await ctx.Channel.SendMessageAsync(result);
        }
    }
}
