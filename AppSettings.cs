using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamantaChatBotTelegram
{
    public static class AppSettings
    {
        public static string Name { get; set; } = "your bot name";
        public static string Key { get; set; }  = "your bot key";
        public static bool isTranslaterMode = false;
        public static int TranslateCount = 0;
        public static string YandexTranslateKey { get; set; } = "your yandex translate key";
        public static bool IsRuEn = false;
    }
}