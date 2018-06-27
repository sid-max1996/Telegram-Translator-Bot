using Telegram.Bot;
using Telegram.Bot.Types;

namespace SamantaChatBotTelegram
{
    class LangTranslSwitch: Command
    {
        public override string[] Names => new string[] { "/en_ru", "/ru_en" };
        public bool isFirstTime = true;

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            string send = "";
            if (message.Text.Contains(Names[0]))
            {
                send = "translation from English to Russian" 
                     + Smilic.GetStringByName("стрелка вправо") + "🇷🇺";
                AppSettings.IsRuEn = false;
            }
            else if (message.Text.Contains(Names[1]))
            {
                send = "translation from Russian to English" 
                    + Smilic.GetStringByName("стрелка влево") + "🇷🇺"; 
                AppSettings.IsRuEn = true;
            }
            await client.SendTextMessageAsync(chatId, send);
        }
    }
}
