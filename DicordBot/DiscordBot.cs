using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicordBot
{
    class DiscordBot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public async Task RunAsync()
        {
            //json
            var json = await ReadJsonConfig();
            var configJson = JsonConvert.DeserializeObject<JsonConfig>(json);
            //configuration
            var config = Config(configJson);
            //client
            Client = new DiscordClient(config);
            Client.Ready += OnClientReady;

            //command
            var commandsConfig = ConfigCommand(configJson);
            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<BotCommands>();

           
            
            //connect
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private DiscordConfiguration Config(JsonConfig json)
        {
            var config = new DiscordConfiguration
            {
                Token = json.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug
            };
            return config;
        }
        private CommandsNextConfiguration ConfigCommand(JsonConfig json)
        {
            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { json.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = true,
            };
            return commandsConfig;
        }

        private Task OnClientReady(DiscordClient sender , ReadyEventArgs e)
        {
            Console.Error.WriteLine("Running");
            return Task.CompletedTask;
        }

        private async Task<string> ReadJsonConfig()
        {
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                return await sr.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
