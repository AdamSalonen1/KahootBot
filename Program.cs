using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kahoot_Bot
{
    internal class Program
    {
        private static string gamePin;
        private static string name;
        private static int j;
        public static void CallToChildThread()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("Headless");

            WebDriver driver = new ChromeDriver(options);

            driver.Url = "https://kahoot.it/";

            SendToElement("name", "gameId", gamePin, driver);

            ClickElement("xpath", "//button[@data-functional-selector='join-game-pin']", driver);

            SendToElement("name", "nickname", (name + j++), driver);

            ClickElement("xpath", "//button[@data-functional-selector='join-button-username']", driver);

            Sleep(1);
            driver.Quit();

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Game Pin: ");
            gamePin = Console.ReadLine();

            Console.WriteLine("Name: ");
            name = Console.ReadLine();

            Console.WriteLine("NumberOfBots");
            string numberOfBots = Console.ReadLine();

            for (int i = 0; i < int.Parse(numberOfBots); i++)
            {
                if (!(i != 0 && i % 5 == 0))
                {
                    var childRef = new ThreadStart(CallToChildThread);
                    Thread childThread = new Thread(childRef);
                    childThread.Start();
                }
                else { Sleep(6); }
            }
        }

        public static void Sleep(int i)
        {
            string sleepString = i + "000";
            System.Threading.Thread.Sleep(Int32.Parse(sleepString));
        }

        public static void ClickElement(string by, string elementName, WebDriver driver)
        {
            if (by.ToLower().Equals("xpath"))
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(elementName)));
                element.Click();
            }
            else if (by.ToLower().Equals("id"))
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(elementName)));
                element.Click();
            }
            else if (by.ToLower().Equals("name"))
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name(elementName)));
                element.Click();
            }
        }

        public static void SendToElement(string by, string elementName, string keys, WebDriver driver)
        {
            if (by.ToLower().Equals("xpath"))
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(elementName)));
                element.SendKeys(keys);
            }
            else if (by.ToLower().Equals("id"))
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(elementName)));
                element.SendKeys(keys);
            }
            else if (by.ToLower().Equals("name"))
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name(elementName)));
                element.SendKeys(keys);
            }
        }
    }
}
