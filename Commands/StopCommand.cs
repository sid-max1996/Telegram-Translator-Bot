
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SamantaChatBotTelegram
{
    class StopCommand : Command
    {
        public override string[] Names => new string[] { "/stop" };
        public bool isFirstTime = true;

        public StopCommand() { }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            string textToSend = "";
            if (AppSettings.isTranslaterMode)
            {
                textToSend = AppSettings.TranslateCount == 0 ? "You don't really need help:)"
                    : "I hope I was useful to you"
                    + Smilic.GetStringByName("приятное смущение");
                if (AppSettings.TranslateCount >= 5)
                    textToSend = "huh,  to be honest I'm already tired of translating"
                         + Smilic.GetStringByName("обида");
                AppSettings.TranslateCount = 0;
                AppSettings.isTranslaterMode = false;
            }
            else
                textToSend = "We even don't start" + Smilic.GetStringByName("смех");
            await client.SendTextMessageAsync(chatId, textToSend);
        }
    }
}
