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
using System.Windows.Controls.Primitives;
using System.Collections;

//using System.Drawing;
namespace Tagsformatter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public static class TrendsInfo
    {
       public static List<string> VideoList = new List<string>();

        public static string _Relatedtopics { get; set; }
        public static string _Relatedqueries { get; set; }
    }
    public partial class MainWindow : Window
    {
        string input, trim, inputText1, inputText2, inputText3, inputText4, inputText5;

        public MainWindow()
        {

            InitializeComponent();
            ContentType.IsEnabled = false; //handles radio button functionality before sumbiting hashtags.

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

        private void TrendsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DependencyObject originalSource = (DependencyObject)e.OriginalSource;
            while ((originalSource != null) && !(originalSource is ListViewItem))
            {
                originalSource = VisualTreeHelper.GetParent(originalSource);
            }
            //if it didn’t find a ListViewItem anywhere in the hierarch, it’s because the user
            //didn’t click on one. Therefore, if the variable isn’t null, run the code
            if (originalSource is ListViewItem listViewItem)
            {                
                {
                    // Get the data item associated with this ListViewItem
                    var clickedItem = listViewItem.Content;

                    // Retrieve the index of the clicked item
                    int index = TrendsList.ItemContainerGenerator.IndexFromContainer(listViewItem);

                    //Console.WriteLine(TrendsInfo.VideoList[3]);
                    // Display the item and its index
                    //MessageBox.Show($"You double-clicked on item '{clickedItem}' at index {index}");

                    Clipboard.SetData(DataFormats.Text, (Object)clickedItem);
                }
            }
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
            YoutubeSearchForVideos();

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

                }
            }
        }

        public async Task YoutubeSearchForVideos()
        {

            var YTService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ""
            });

            var Videorequest = YTService.Search.List("snippet");
            Videorequest.Q = MainTextBox.Text;
            //Videorequest.Q = "DeadRising";
            Videorequest.MaxResults = 10;

            var Searchresults = await Videorequest.ExecuteAsync();
                       
            

            if (Searchresults.Items.Count <= 0)
            {
                Console.WriteLine("No videos found");
                this.Close();
            }

            foreach (var PopluarVideo in Searchresults.Items)
            {

               TrendsInfo.VideoList.Add(TrendsInfo._Relatedtopics = String.Format(PopluarVideo.Snippet.Title));

            }

            TrendsList.ItemsSource = TrendsInfo.VideoList;
     
        }
    }
}