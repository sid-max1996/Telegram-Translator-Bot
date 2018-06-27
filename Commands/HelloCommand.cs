using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SamantaChatBotTelegram
{
    public class HelloCommand : Command
    {
        public override string[] Names => new string[] { "Hello",
            "hello", "hi", "привет", "здравствуй"};
        public bool isFirstTime = true;

        public HelloCommand() { }

        private string getName(TelegramBotClient client)
        {
            var me = client.GetMeAsync().Result;
            return me.FirstName;
        }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var textToSend = isFirstTime ? "Hello " + message.From.FirstName + " My Name is "
                +  getName(client) + " I'm an English text translator" + Smilic.GetStringByName("румянец")
                : "Hello again buddy" + Smilic.GetStringByName("улыбка");
            await client.SendTextMessageAsync(chatId, textToSend, replyToMessageId: messageId);
            isFirstTime = false;
        }
    }
}