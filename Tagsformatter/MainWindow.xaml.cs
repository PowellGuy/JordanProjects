using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
//using System.Net.Http;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;
using System.Windows.Shapes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
//using System;
using OpenQA.Selenium.BiDi.Communication.Transport;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;
//using Youtube Api
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;



using Newtonsoft.Json.Linq;
using System.Diagnostics.Contracts;

//using System.Drawing;
namespace Tagsformatter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class TrendsInfo
    {
        public string _Relatedtopics { get; set; }
        public string _Relatedqueries { get; set; }
    }
    public partial class MainWindow : Window
    {
        string input, trim, inputText1, inputText2, inputText3, inputText4, inputText5;

        public MainWindow()
        {

            InitializeComponent();
            Run();
            ContentType.IsEnabled = false; //handles radio button functionality before sumbiting hashtags.

            //YoutubeSearchSuggestions();
            //SelenuiumScrape2Async();
        }

        private void ThemeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //Set background using hex color in C#
            string hexColor = "#191A1F";
            string hexColor2 = "#3C3F47";
            var brushConverter = new BrushConverter();

            ContentStackLeft.Background = (Brush)brushConverter.ConvertFromString(hexColor);
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

        private void ThemeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            ContentStackLeft.Background = Brushes.White;
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
            if (MainTextBox.Text == "")
            {
                MessageBox.Show("Please enter search term to convert");
                this.Close();
            }

            else
            {
                SelenuiumScrape();
                ContentType.IsEnabled = true;
                input = MainTextBox.Text;


            }
        }

        private void YoutubeTags_Checked(object sender, RoutedEventArgs e)
        {
            HeadingLabel.Content = "Youtube";
            trim = input.Replace("#", "");
            trim = trim.Replace(" ", ",");
            MainTextBox.Text = trim;
            ContentType.Background = Brushes.Red;
            ConvertButton.Background = Brushes.Red;
        }

        private void InstagramTags_Checked(object sender, RoutedEventArgs e)
        {
            HeadingLabel.Content = "Instagram";
            ContentType.Background = Brushes.MediumPurple;
            ConvertButton.Background = Brushes.MediumPurple;
            MainTextBox.Text = input;
        }

        private void TIKTOKTags_Checked(object sender, RoutedEventArgs e)
        {
            inputText1 = input.Split(' ')[1].Trim();
            inputText2 = input.Split(' ')[2].Trim();
            inputText3 = input.Split(' ')[3].Trim();

            MainTextBox.Text = inputText1 + " " + inputText2 + " " + inputText3 + " " + "#fyp";
            ContentType.Background = Brushes.HotPink;
            ConvertButton.Background = Brushes.HotPink;
        }

        private void FacebookTags_Checked(object sender, RoutedEventArgs e)
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

        private void TwitterTags_Checked(object sender, RoutedEventArgs e)
        {
            HeadingLabel.Content = "Twitter";

            inputText1 = input.Split(' ')[1].Trim();
            inputText2 = input.Split(' ')[2].Trim();

            MainTextBox.Text = inputText1 + " " + inputText2;

            ContentType.Background = Brushes.DeepSkyBlue;
            ConvertButton.Background = Brushes.DeepSkyBlue;
        }

        public void SelenuiumScrape()
        {
            //uses chromedriver class from Selenuim Package to create webdriver.
            using (IWebDriver SelenuimDriver = new ChromeDriver())
            {


                //Navigates to the URL
                SelenuimDriver.Navigate().GoToUrl("https://app.sistrix.com/en/instagram-hashtags");

                //Creating webelement to handing Searchbar and button functionality, via find ID.
                IWebElement WebsiteSearchBar = SelenuimDriver.FindElement(By.Id("html-input-span-1"));

                WebsiteSearchBar.SendKeys(MainTextBox.Text);

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

                    string[] TrendingResults =
                        {
                            WebsiteResult.Text.Split('#')[0].Trim(), WebsiteResult.Text.Split('#')[1].Trim(),
                            WebsiteResult.Text.Split('#')[2].Trim(), WebsiteResult.Text.Split('#')[3].Trim(),
                            WebsiteResult.Text.Split('#')[4].Trim()
                        };

                    List<TrendsInfo> TrendResults = new List<TrendsInfo>();
                    //TrendResults.Add(new TrendsInfo() { _Relatedqueries = WebsiteResult.Text, _Relatedtopics = "Test2" });                    
                    TrendResults.Add(new TrendsInfo() { _Relatedtopics = TrendingResults[0], _Relatedqueries = "Test2" });
                    TrendResults.Add(new TrendsInfo() { _Relatedtopics = TrendingResults[1], _Relatedqueries = "Test2" });
                    TrendResults.Add(new TrendsInfo() { _Relatedtopics = TrendingResults[2], _Relatedqueries = "Test2" });
                    TrendResults.Add(new TrendsInfo() { _Relatedtopics = TrendingResults[3], _Relatedqueries = "Test2" });
                    TrendResults.Add(new TrendsInfo() { _Relatedtopics = TrendingResults[4], _Relatedqueries = "Test2" });

                    TrendsList.ItemsSource = TrendResults;

                }
            }
        }

        private async Task Run()
        {

        }
    }
}

    //    public async Task YoutubeSearchSuggestions()
    //    {
    //        // URL for the Google Trends API request
    //        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
    //        {
    //            ApiKey = "", 
    //            ApplicationName = this.GetType().ToString()
    //        });

    //        var searchListRequest = youtubeService.Search.List("snippet");
    //        searchListRequest.Q = "Deadrising"; // Replace with your search term.
    //        searchListRequest.MaxResults = 5;

    //        var searchListResponse = await searchListRequest.ExecuteAsync();

    //        List<string> videos = new List<string>();

    //        // Add each result to the appropriate list, and then display the lists of
    //        // matching videos, channels, and playlists.
    //        foreach (var searchResult in searchListResponse.Items)
    //        {
    //            videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
    //            break;
    //        }

    //        Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
    //    }
    //}





//    {
//        try
//        {
//            // Send the request and get the response as a string
//            string GT_UrlResponse = await client.GetStringAsync(url);

//            // Clean up the response by removing the prefix ")]}',"
//            string GT_CleanedResponse = GT_UrlResponse.Replace(")]}',", "");

//            // Parse the cleaned response as JSON
//            JObject GT_jsonData = JObject.Parse(GT_CleanedResponse);

//            // A foreach loop used to extract the topics from the JSON object, by placing the data into the array and iterate over each topic.
//            var GT_Relatedtopics = GT_jsonData["default"]["topics"];
//            int index = 1;
//            foreach (var topic in GT_Relatedtopics)
//            {
//                Console.WriteLine($"Topic {index++}: {topic}");
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error: {ex.Message}");
//        }
//    }