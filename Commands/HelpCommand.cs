using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SamantaChatBotTelegram
{
    class HelpCommand: Command
    {
        public override string[] Names => new string[] { "/help" };
        public bool isFirstTime = true;

        public HelpCommand() { }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            var textToSend = "/start: turn on translator mode\r\n" +
                "/stop: turn off the translator mode\r\n" +
                "/ru_en, /en_ru: switching languages for translation";
            await client.SendTextMessageAsync(chatId, textToSend);
        }
    }
}
