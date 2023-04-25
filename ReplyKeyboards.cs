using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace SubscribeBot
{
    public static class ReplyKeyboards
    {
        public static InlineKeyboardMarkup OK()
        {
            InlineKeyboardButton[] buttons;
            InlineKeyboardMarkup inlineKeyboardMarkup;

            buttons = new InlineKeyboardButton[1];


            buttons[0] = InlineKeyboardButton.WithCallbackData("Готово!", "Subscribed");


            inlineKeyboardMarkup = new InlineKeyboardMarkup(buttons);

            return inlineKeyboardMarkup;
        }
        public static InlineKeyboardMarkup Default()
        {
            InlineKeyboardButton[] buttons;
            InlineKeyboardMarkup inlineKeyboardMarkup;

            buttons = new InlineKeyboardButton[1];


            buttons[0] = InlineKeyboardButton.WithCallbackData("ДАЙ ГАЙД", "getGuide");


            inlineKeyboardMarkup = new InlineKeyboardMarkup(buttons);

            return inlineKeyboardMarkup;
        }
    }
}
