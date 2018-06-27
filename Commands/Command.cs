using Telegram.Bot;
using Telegram.Bot.Types;

namespace SamantaChatBotTelegram
{
    public abstract class Command
    {
        public abstract string[] Names { get; }
        public abstract void Execute(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            foreach (string name in Names)
                if (command.Contains(name))
                    return true;
            return false;
        }
    }
}
