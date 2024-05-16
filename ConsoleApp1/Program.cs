using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using static System.Net.WebRequestMethods;

namespace parsing_in_browesr2
{
    internal class Program
    {



        public class GamePriceSearcher
        {
            public static async Task SearchAndDisplayPrice(string nameGame, Telegram.Bot.TelegramBotClient botClient, Telegram.Bot.Types.Message messeg)
            {
                string Stoimost = "";
                IWebDriver driver = new ChromeDriver();
                driver.Url = @"https://steambuy.com/catalog/";

                for (int i = 0; i < 10; i++)
                {
                    Task.Delay(100);
                    driver.FindElement(By.XPath("//input[@placeholder='ПОИСК ПО НАЗВАНИЮ ИГРЫ ИЛИ ИЗДАТЕЛЮ']"));
                    Task.Delay(1500);

                    break;
                }

                driver.FindElement(By.XPath("//input[@placeholder='ПОИСК ПО НАЗВАНИЮ ИГРЫ ИЛИ ИЗДАТЕЛЮ']")).SendKeys(nameGame);

                Thread.Sleep(2000);
                Stoimost = driver.FindElement(By.XPath("//*[@id=\"view-detail\"]/div/div[1]/div[3]/div[1]/div[2]")).GetAttribute("textContent"); /*textContent*/ /*product - item__cost*/ /*#text*/

                if (Stoimost.Length > 0)
                {
                    await botClient.SendTextMessageAsync(messeg.Chat.Id, $"стоимость игры на steambuy {Stoimost} ");
                }
                else
                {
                    await botClient.SendTextMessageAsync(messeg.Chat.Id, $"Игра не найденаm или скоро выйдет");
                }

                driver.Dispose();
            }
        }


     
        static void Main(string[] args)
        {


            var client = new TelegramBotClient("7096308016:AAG04fsIrmhYk0mShbYn2fJI3WBmSfceokI");

            client.StartReceiving(Update, Error);


            Console.ReadKey();
        }

        private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            await botClient.SendTextMessageAsync(messeg.Chat.Id, "произошла ошибка, попробутей сделать запрас чесрез время");

            throw new NotImplementedException();
        }

        private static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var messeg = update.Message;
            var client = new TelegramBotClient("7096308016:AAG04fsIrmhYk0mShbYn2fJI3WBmSfceokI");
            var yourMessage = messeg;
            string nameGame = messeg.Text;

            var yourBotClient = client;

            if (messeg != null)
            { 

                Console.WriteLine("ЕСТЬ СООБЩЕНИЕ");

                if (messeg.Text.ToLower().Contains("хочу найти игру"))
                {

                    await botClient.SendTextMessageAsync(messeg.Chat.Id, "введи название игры");

                    return;
                }

                if (messeg.Text.ToLower().Contains(""))
                {
                 

                   GamePriceSearcher.SearchAndDisplayPrice(nameGame, yourBotClient, yourMessage);



                    return;

                }







            }




        }
    }
}
