using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Net.Http;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;
using System.Windows.Shapes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using OpenQA.Selenium.BiDi.Communication.Transport;
using System.Linq.Expressions;


//using System.Drawing;



namespace Tagsformatter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //bool IsTagsSelected = false;
        string input, trim, inputText1, inputText2, inputText3, inputText4, inputText5,
        URL = "https://best-hashtags.com/hashtag/deadrising", URL2 = "https://www.tagsfinder.com/en-us/related/xbox/";
        static readonly HttpClient HttpScrapeClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            BeginScrape();
            SelenuiumScrape();
            ContentType.IsEnabled = false; //handles radio button functionality before sumbiting hashtags.
        }

        private void ThemeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            // Set background using hex color in C#
            string hexColor = "#191A1F";
            string hexColor2 = "#3C3F47";
            var brushConverter = new BrushConverter();
            
            ContentStack.Background = (Brush)brushConverter.ConvertFromString(hexColor);
            MainLayout.Background = (Brush)brushConverter.ConvertFromString(hexColor);
            HeadingLabel.Background = (Brush)brushConverter.ConvertFromString(hexColor);
            MainTextBox.Background = (Brush)brushConverter.ConvertFromString(hexColor);

            HeadingLabel.Foreground = Brushes.White;
            TitleLabel.Foreground = Brushes.White;
            MainTextBox.Foreground = Brushes.White;
            MainTextBox.CaretBrush = Brushes.White;
            ThemeCheckbox.Foreground = Brushes.White;
            YoutubeTags.Foreground = Brushes.White;
            InstagramTags.Foreground = Brushes.White;
            TIKTOKTags.Foreground = Brushes.White;
            FacebookTags.Foreground = Brushes.White;
            TwitterTags.Foreground = Brushes.White;

            ContentType.Background = (Brush)brushConverter.ConvertFromString(hexColor2);
            ConvertButton.Background = (Brush)brushConverter.ConvertFromString(hexColor2);
            ConvertButton.Foreground = Brushes.White;
        }

        private void ConvertButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //ConvertButton.Background = Brushes.White;
            //ConvertButton.Foreground = Brushes.Black;
        }

        private void ConvertButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //string hexColor2 = "#3C3F47";
            //var brushConverter = new BrushConverter();
            //ConvertButton.Background = (Brush)brushConverter.ConvertFromString(hexColor2);
        }

        private void ThemeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            ContentStack.Background = Brushes.White;
            MainLayout.Background = Brushes.White;
            HeadingLabel.Background = Brushes.White;
            MainTextBox.Background = Brushes.White;

            HeadingLabel.Foreground = Brushes.Black;
            TitleLabel.Foreground = Brushes.Black;
            MainTextBox.Foreground = Brushes.Black;

            MainTextBox.CaretBrush = Brushes.Black;
            ThemeCheckbox.Foreground = Brushes.Black;

            ContentType.Background = Brushes.Yellow;
            ConvertButton.Background = Brushes.Yellow;
            ContentType.Background = Brushes.Yellow;
            ConvertButton.Foreground = Brushes.Black;

            YoutubeTags.Foreground = Brushes.Black;
            InstagramTags.Foreground = Brushes.Black;
            TIKTOKTags.Foreground = Brushes.Black;
            FacebookTags.Foreground = Brushes.Black;
            TwitterTags.Foreground = Brushes.Black;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            int Hashtagcounter;
            string ConvertToString;

            if (MainTextBox.Text == "" || MainTextBox.Text.Contains("#") != true)
            {
                MessageBox.Show("No Hashtag to convert, try again.");
            }

            if (MainTextBox.Text.Contains("#"))
            {
                Hashtagcounter = MainTextBox.Text.Count(x => x == '#');
                ConvertToString = Hashtagcounter.ToString();

                if (Hashtagcounter > 5)
                {
                    input = MainTextBox.Text;
                    ContentType.IsEnabled = true;
                }

                else
                {
                    MessageBox.Show("Please convert at least 5 hashtags.");
                }
            }
            else
            {
                ContentType.IsEnabled = true;
                input = MainTextBox.Text;
            }
        }

        public void YoutubeTags_Checked(object sender, RoutedEventArgs e)
        {
            if (ContentType.IsEnabled == true)
            {
                HeadingLabel.Content = "Youtube";

                trim = input.Replace("#", "");
                trim = trim.Replace(" ", ",");
                MainTextBox.Text = trim;
                ContentType.Background = Brushes.Red;
                ConvertButton.Background = Brushes.Red;
            }

            else
            {
                ContentType.IsEnabled = false;
            }
        }

        public void InstagramTags_Checked(object sender, RoutedEventArgs e)
        {
            if (ContentType.IsEnabled == true)
            {
                //IsTagsSelected = true;
                HeadingLabel.Content = "Instagram";
                ContentType.Background = Brushes.MediumPurple;
                ConvertButton.Background = Brushes.MediumPurple;
                MainTextBox.Text = input;
            }

            else
            {
                ContentType.IsEnabled = false;
            }
        }

        public void TIKTOKTags_Checked(object sender, RoutedEventArgs e)
        {
            if (ContentType.IsEnabled == true)
            {
                HeadingLabel.Content = "Tiktok";

                inputText1 = input.Split(' ')[1].Trim();
                inputText2 = input.Split(' ')[2].Trim();
                inputText3 = input.Split(' ')[3].Trim();

                MainTextBox.Text = inputText1 + " " + inputText2 + " " + inputText3 + " " + "#fyp";
                ContentType.Background = Brushes.HotPink;
                ConvertButton.Background = Brushes.HotPink;
            }

            else
            {
                ContentType.IsEnabled = false;
            }
        }

        private void FacebookTags_Checked(object sender, RoutedEventArgs e)
        {
            if (ContentType.IsEnabled == true)
            {
                HeadingLabel.Content = "Facebook";

                inputText1 = input.Split(' ')[1].Trim();
                inputText2 = input.Split(' ')[2].Trim();
                inputText3 = input.Split(' ')[3].Trim();
                inputText4 = input.Split(' ')[4].Trim();
                inputText5 = input.Split(' ')[5].Trim();

                MainTextBox.Text = inputText1 + " " + inputText2 + " " + inputText3 + " " + inputText4 + " " + inputText5;
                ContentType.Background = Brushes.CornflowerBlue;
                ConvertButton.Background = Brushes.CornflowerBlue;
            }

            else
            {
                ContentType.IsEnabled = false;
            }
        }

        private void TwitterTags_Checked(object sender, RoutedEventArgs e)
        {
            if (ContentType.IsEnabled == true)
            {
                HeadingLabel.Content = "Twitter";

                inputText1 = input.Split(' ')[1].Trim();
                inputText2 = input.Split(' ')[2].Trim();

                MainTextBox.Text = inputText1 + " " + inputText2;

                ContentType.Background = Brushes.DeepSkyBlue;
                ConvertButton.Background = Brushes.DeepSkyBlue;
            }

            else
            {
                ContentType.IsEnabled = false;
            }
        }

        private void SelenuiumScrape()
        {
            //uses chromedriver class from Selenuim Package to create webdriver.
            using (IWebDriver SelenuimDriver = new ChromeDriver())
            {
                //Navigates to the URL
                SelenuimDriver.Navigate().GoToUrl("https://app.sistrix.com/en/instagram-hashtags");

                //Creating webelement to handing Searchbar and button functionality, via find ID.
                IWebElement WebsiteSearchBar = SelenuimDriver.FindElement(By.Id("html-input-span-1"));
                WebsiteSearchBar.SendKeys("DeadRising");

                //action for the enter key.
                new Actions(SelenuimDriver)
                  .KeyDown(Keys.Shift)
                  .SendKeys(Keys.Enter)
                  .Perform();

                //Using Selenuim driver to create delay/timeout.
                var Delay = SelenuimDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                if (Delay != null)
                {
                    IWebElement WebsiteResult = SelenuimDriver.FindElement(By.ClassName("instagram-hashtag-results"));
                    MainTextBox.Text += WebsiteResult.Text;
                }
            } 
        }

        //class for attempting to read url and add to a variable.
        private async Task BeginScrape()
        {
            Console.WriteLine("beginning connection to site: " + URL);

            string HtmlString = await HttpScrapeClient.GetStringAsync(URL);

            //uses the HtmlDocument object to handle the data taken from the chosen URL.
            var HttpDocHandler = new HtmlDocument();
            HttpDocHandler.LoadHtml(HtmlString);

            ////uses the HTMLDocument class to collect the specific tags/keywords and parses them into a list.
            var DivHandler = HttpDocHandler.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("tag-box")).ToList();

            //Console.WriteLine("You entered in the search bar: "); 

            //for each loop created to cycle through all the divs in the HTML page.
            foreach (var div in DivHandler)
            {
                Console.WriteLine(div.InnerText.Trim());
                Console.WriteLine("Hashtags scraped");
                break;
            }
        }
    }
}

