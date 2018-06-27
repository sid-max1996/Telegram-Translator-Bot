using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;


namespace SamantaChatBotTelegram
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Command> commandsList = new List<Command>();
            commandsList.Add(new HelloCommand());
            commandsList.Add(new HelpCommand());
            commandsList.Add(new StartCommand());
            commandsList.Add(new LangTranslSwitch());
            commandsList.Add(new StopCommand());
            commandsList.Add(new ShowFotoCommand());
            commandsList.Add(new MusicCommand());
            Bot samantha = new Bot(AppSettings.Key, commandsList);
            samantha.Start();
            Console.ReadLine();
        }
    }

}