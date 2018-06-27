
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SamantaChatBotTelegram
{
    class StartCommand : Command
    {
        public override string[] Names => new string[] { "/start" };
        public bool isFirstTime = true;

        public StartCommand() { }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            AppSettings.isTranslaterMode = true;
            var textToSend = "Now I will translate everything that you will write to me"+ 
                Smilic.GetStringByName("подмигивание");
            await client.SendTextMessageAsync(chatId, textToSend);
        }
    }
}

