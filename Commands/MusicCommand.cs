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
    class MusicCommand : Command
    {
        public override string[] Names => new string[] { "/music_help", "/NY", "/M" };

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            if (message.Text.Contains(Names[0]))
            {
                string angelSmilic = Smilic.GetStringByName("ангел");
                string text = angelSmilic + "Введи /NYчисло или /Mчисло \r\n"
                    +"для того чтобы слушать песню Новогоднюю или Обычную";
                await client.SendTextMessageAsync(chatId, text);
            }
            else
            {
                string dirPath = Directory.GetCurrentDirectory();
                string numStr = "";
                if (message.Text.StartsWith(Names[1]))
                {
                    dirPath = dirPath.Replace("bin\\Debug", "music\\НГ");
                    numStr = message.Text.Replace("/NY", "");
                }
                else if (message.Text.StartsWith(Names[2]))
                {
                    dirPath = dirPath.Replace("bin\\Debug", "music");
                    numStr = message.Text.Replace("/M", "");
                }

                string[] fileNames = Directory.GetFiles(dirPath);

                int num;
                if (!int.TryParse(numStr, out num))
                {
                    await client.SendTextMessageAsync(chatId, Smilic.GetStringByName("ошибка")
                        + "число неккоректное((", replyToMessageId: messageId);
                }
                else
                {
                    num--;
                    if(num >= 0 && num < fileNames.Length)
                        await SendMusic(chatId.ToString(), fileNames[num], AppSettings.Key);
                    else
                        await client.SendTextMessageAsync(chatId, Smilic.GetStringByName("ошибка")
                            + "столько песен нет", replyToMessageId: messageId);
                }
            }
        }

        private async static Task SendMusic(string chatId, string filePath, string token)
        {
            var url = string.Format("https://api.telegram.org/bot{0}/sendAudio", token);
            var fileName = filePath.Split('\\').Last();

            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(chatId.ToString(), Encoding.UTF8), "chat_id");

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    form.Add(new StreamContent(fileStream), "audio", fileName);

                    using (var client = new HttpClient())
                    {
                        await client.PostAsync(url, form);
                    }
                }
            }
        }//end SendMusic
    }
}
