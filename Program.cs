using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Question1(@"https://www.ferragamo.com/shop/gb/en/women/handbags/tote-bags-");
            Question2(@"https://www.ferragamo.com/shop/gb/en/women/handbags/tote-bags-/boxyz-731854--24");
            
            Console.WriteLine("계속 하시려면 아무 키나 누르세요.");
            Console.ReadKey();
        }

        static void PopupClose(IWebDriver driver)
        {
            // ? 쓸데없는 팝업창닫기
            if (driver.WindowHandles.Count >= 2)
            {
                for (int i = driver.WindowHandles.Count; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        driver.SwitchTo().Window(driver.WindowHandles[i]);
                    }
                    else
                    {
                        driver.SwitchTo().Window(driver.WindowHandles[i]);
                        driver.Close();
                    }
                }
            }
        }

        /// <summary>
        /// ex) https://www.ferragamo.com/shop/gb/en/women/handbags/tote-bags- 
        /// </summary>
        /// <param name="uri"></param>
        static void Question1(string uri)
        {
            // ? 크롬 숨기기
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            options.AddArgument("--headless");

            using (IWebDriver driver = new ChromeDriver(driverService, options))
            {
                driver.Navigate().GoToUrl(uri);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                // ? 쓸데없는 팝업창닫기
                PopupClose(driver);

                // ? 상품 리스트 찾기
                var element = driver.FindElement(By.XPath("/html/body/main/section[2]"));
                foreach (var item in element.FindElements(By.TagName("li")))
                {
                    Console.WriteLine(item.FindElement(By.TagName("a"))?.GetAttribute("href"));
                }

                Console.WriteLine("");
            }
        }

        /// <summary>
        /// ex) https://www.ferragamo.com/shop/gb/en/women/handbags/tote-bags-/boxyz-731854--24 
        /// </summary>
        /// <param name="uri"></param>
        static void Question2(string uri)
        {
            // ? 크롬 숨기기
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            options.AddArgument("--headless");

            using (IWebDriver driver = new ChromeDriver(driverService, options))
            {
                driver.Navigate().GoToUrl(uri);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                // ? 쓸데없는 팝업창닫기
                PopupClose(driver);

                // ? 상품 정보 출력
                Console.WriteLine(driver.FindElement(By.XPath("/html/body/main/section[1]/section[2]/div/div/div[1]/div[2]/div/span/div/div/span"))?.Text);
                Console.WriteLine($"Name : {driver.FindElement(By.XPath("/html/body/main/section[1]/section[1]/div[3]/div/div[1]"))?.Text}");
                Console.WriteLine($"Price : {driver.FindElement(By.XPath("/html/body/main/section[1]/section[1]/div[3]/div/div[2]/div/ul/li"))?.Text}");
            }
        }
    }
}
