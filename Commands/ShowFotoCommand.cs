using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SamantaChatBotTelegram
{
    class ShowFotoCommand : Command
    {
        public override string[] Names => new string[] { "/foto_next", "/foto_prev"};
        private int curFotoNum = -1;

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string dirPath = Directory.GetCurrentDirectory();
            dirPath = dirPath.Replace("bin\\Debug", "foto");
            string[] fileNames = Directory.GetFiles(dirPath);
            if (message.Text.Contains(Names[0]))
            {
                if (curFotoNum == fileNames.Length-1)
                    await client.SendTextMessageAsync(chatId, "фото закончились, вперед не могу"
                        + Smilic.GetStringByName("ошибка"),
                        replyToMessageId: messageId);
                else
                {
                    curFotoNum++;
                    await SendPhoto(chatId.ToString(), fileNames[curFotoNum], AppSettings.Key);
                }
            }
            else
            if (message.Text.Contains(Names[1]))
            {
               if(curFotoNum == 0)
                    await client.SendTextMessageAsync(chatId, "назад не могу, только вперед"
                        + Smilic.GetStringByName("ошибка"), 
                        replyToMessageId: messageId);
                else
                {
                    curFotoNum--;
                    await SendPhoto(chatId.ToString(), fileNames[curFotoNum], AppSettings.Key);
                }

            }
        }

        private async static Task SendPhoto(string chatId, string filePath, string token)
        {
            var url = string.Format("https://api.telegram.org/bot{0}/sendPhoto", token);
            var fileName = filePath.Split('\\').Last();

            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(chatId.ToString(), Encoding.UTF8), "chat_id");

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    form.Add(new StreamContent(fileStream), "photo", fileName);

                    using (var client = new HttpClient())
                    {
                        await client.PostAsync(url, form);
                    }
                }
            }
        }//end SendFoto

    }
}
