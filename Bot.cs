using SubscribeBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace SubscribeBot
{
    class Bot
    {
        private static TelegramBotClient client;

        private static List<Command> commandsList;
        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }

        public static async Task<TelegramBotClient> Get()
        {
           
            if (client != null)
            {
                return client;
            }

            commandsList = new List<Command>
            {
                    new Start()
            };


            client = new TelegramBotClient(BotSettings.Key);
            var cts = new CancellationTokenSource();
            await client.ReceiveAsync(new DefaultUpdateHandler(updateHandler: HandleUpdateAsync, errorHandler: UpdateManager.HandleErrorAsync), cancellationToken: cts.Token);

            return client;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient arg1, Update update, CancellationToken cancellationToken)
        {
            await UpdateManager.Handle(arg1, update, cancellationToken, Commands, client);
        }
    }
}
