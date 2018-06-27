using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamantaChatBotTelegram
{
    class Smilic
    {
        private static Dictionary<string, string> Dic = new Dictionary<string, string>
        {
            { "улыбка", "U+1F605" },
            { "подмигивание", "U+1F609" },
            { "румянец", "U+1F60A" },
            { "ангел", "U+1F607" },
            { "приятное смущение", "U+1F60C"},
             { "ошибка", "U+26A0" },
            { "стрелка вправо", "U+23E9" },
            { "стрелка влево", "U+23EA" },
            { "обида", "U+1F612" }
        };

        public static string GetStringByName(string name)
        {
            var unicodeNumber = Dic[name];
            string visibleSymbol = 
                char.ConvertFromUtf32(Convert.ToInt32(unicodeNumber.Substring(2), 16));
            return visibleSymbol;
        }
    }
}
