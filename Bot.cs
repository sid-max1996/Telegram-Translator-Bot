using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Translator;

namespace SamantaChatBotTelegram
{
    public class Bot
    {
        private TelegramBotClient client;
        private List<Command> commandsList;
        private YandexTranslator translator;

        public IReadOnlyList<Command> Commands { get { return commandsList.AsReadOnly(); } }
        public TelegramBotClient Client { get { return client; } }

        public Bot(string token, List<Command> commandsList)
        {
            client = new TelegramBotClient(token);
            this.commandsList = commandsList;
            this.translator = new YandexTranslator();
        }

        public void Start()
        {
            client.OnMessage += Bot_OnMessage;
            client.SetWebhookAsync();
            var me = client.GetMeAsync().Result;
            Console.Title = me.Username;
            client.StartReceiving();
        }

        private async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs messEvent)
        {
            Message msg = messEvent.Message;
            if (msg == null)
                return;
            if(msg.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
            {
                if (AppSettings.isTranslaterMode)
                {
                    if (msg.Text.Contains("/stop"))
                    {
                        Command comm = new StopCommand();
                        comm.Execute(msg, client);
                    }
                    else
                    {
                        LangTranslSwitch lSwitch = new LangTranslSwitch();
                        if (lSwitch.Contains(msg.Text))
                            lSwitch.Execute(msg, client);
                        else
                        {
                            string text = msg.Text;
                            var lang = "en-ru";
                            if (AppSettings.IsRuEn)
                                lang = "ru-en";
                            var chatId = msg.Chat.Id;
                            var textToSend = translator.Translate(text, lang);
                            if (textToSend == text)
                                textToSend = msg.From.FirstName + " меня не проведешь)";
                            await client.SendTextMessageAsync(chatId, textToSend);
                            AppSettings.TranslateCount++;
                        }
                    }
                }
                else
                foreach (var comm in commandsList)
                    if (comm.Contains(msg.Text))
                        comm.Execute(msg, client);
            }
        }
    }

}