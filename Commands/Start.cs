using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SubscribeBot.Commands
{
    class Start : Command
    {
        public override string Name => "/start";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.From.Id, "Привет💫\n Чтобы получить гайд \"Как перестать контролировать и начать жить\" необходимо подписаться на мой ТГ канал: @plyasunovapsy", replyMarkup: ReplyKeyboards.OK());
        }
    }
}
