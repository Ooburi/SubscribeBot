using SubscribeBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SubscribeBot
{
    public class UpdateManager
    {
        static IReadOnlyList<Command> Commands = null;
        static long PublicID = -1001641505431;

        public static async Task Handle(ITelegramBotClient arg1, Update update, CancellationToken cancellationToken, IReadOnlyList<Command> commands, TelegramBotClient client)
        {
            Commands = commands;

            switch (update.Type)
            {

                case UpdateType.Message:
                    try
                    {
                        await HandleMessage(update, client);
                    }
                    catch (Exception e)
                    {
                        await HandleErrorAsync(client, e, cancellationToken);
                    }
                    break;
                case UpdateType.CallbackQuery:
                    try
                    {
                        await HandleCallback(update, client);
                    }
                    catch (Exception e)
                    {
                        await HandleErrorAsync(client, e, cancellationToken);
                    }
                    break;
            }
        }

        private static async Task HandleCallback(Update update, TelegramBotClient client)
        {
            string updateData = update.CallbackQuery.Data;
           

            if (updateData == "Subscribed")
            {
                long userId = update.CallbackQuery.From.Id;
                bool subscribed = true;
                try
                {
                    ChatMember chatMember = await client.GetChatMemberAsync(PublicID, userId);
                    if (chatMember == null || chatMember.Status == ChatMemberStatus.Left || chatMember.Status == ChatMemberStatus.Restricted || chatMember.Status == ChatMemberStatus.Kicked)
                    {
                        subscribed = false;
                    }
                }
                catch
                {
                    subscribed = false;
                }

                if (subscribed)
                {
                    await client.SendTextMessageAsync(userId, "Отлично, скачать гайд  можно по этой ссылке:\n МЕСТО ДЛЯ ССЫЛКИ");
                }
                else
                {
                    await client.SendTextMessageAsync(userId, "К сожалению вы все ещё не подписались. Попробуйте ещё раз😉", replyMarkup: ReplyKeyboards.OK());
                }
            }
        }

        private static async Task HandleMessage(Update update, TelegramBotClient client)
        {
            Message message = update.Message;
           
            switch (message.Type)
            {
                // если текст (команды)
                case MessageType.Text:
                    await ManageText(update, client);
                    break;
            }
        }
        private async static Task ManageText(Update update, TelegramBotClient client)
        {
            Message message = update.Message;

            var commands = Commands;

            foreach (var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    try
                    {
                        
                        await command.Execute(message, client);
                        return;
                    }
                    catch
                    {

                    }
                }
            }
        }
            public static Task HandleErrorAsync(ITelegramBotClient arg1, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
            return Task.CompletedTask;
        }
    }
}
