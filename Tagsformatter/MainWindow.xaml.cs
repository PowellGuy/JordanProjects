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
//Using JSON Serialization
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
    public partial class MainWindow : Window
    {
        //string input, trim, inputText1, inputText2, inputText3, inputText4, inputText5;

        public MainWindow()
        {

            InitializeComponent();
            TF_ContentStackPanel.IsEnabled = false; //handles radio button functionality before sumbiting hashtags.

        }

        private static class InputData
        {
            public static string input { get; set; }
            public static string trim { get; set; }
            public static string inputText1 { get; set; }
            public static string inputText2 { get; set; }
            public static string inputText3 { get; set; }
            public static string inputText4 { get; set; }
            public static string inputText5 { get; set; }

        }


        private static class YTSearchInfo
        {
            public static List<string> VideoList = new List<string>();

            public static string _Relatedtopics { get; set; }
        }

        private void ThemeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //Set background using hex color in C#
            string hexColor = "#191A1F";
            string hexColor2 = "#3C3F47";
            var brushConverter = new BrushConverter();

            TF_StackPanel.Background = (Brush)brushConverter.ConvertFromString(hexColor);
            MainWindowGrid.Background = (Brush)brushConverter.ConvertFromString(hexColor);
            TF_HeadingLabel.Background = (Brush)brushConverter.ConvertFromString(hexColor);
            TF_TextBox.Background = (Brush)brushConverter.ConvertFromString(hexColor);

            TF_HeadingLabel.Foreground = Brushes.White;
            TF_TitleLabel.Foreground = Brushes.White;
            TF_TextBox.Foreground = Brushes.White;
            TF_TextBox.CaretBrush = Brushes.White;
            DarkModeCheckbox.Foreground = Brushes.White;
            TF_YoutubeTags.Foreground = Brushes.White;
            TF_InstagramTags.Foreground = Brushes.White;
            TF_TIKTOKTags.Foreground = Brushes.White;
            TF_FacebookTags.Foreground = Brushes.White;
            TF_TwitterTags.Foreground = Brushes.White;

           TF_ContentStackPanel.Background = (Brush)brushConverter.ConvertFromString(hexColor2);
           TF_ConvertButton.Background = (Brush)brushConverter.ConvertFromString(hexColor2);
           TF_ConvertButton.Foreground = Brushes.White;
        }

        private void ThemeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            TF_StackPanel.Background = Brushes.White;
            MainWindowGrid.Background = Brushes.White;
            TF_HeadingLabel.Background = Brushes.White;
            TF_TextBox.Background = Brushes.White;

            TF_HeadingLabel.Foreground = Brushes.Black;
            TF_TitleLabel.Foreground = Brushes.Black;
            TF_TextBox.Foreground = Brushes.Black;

            TF_TextBox.CaretBrush = Brushes.Black;
            DarkModeCheckbox.Foreground = Brushes.Black;

            TF_ContentStackPanel.Background = Brushes.Yellow;
            TF_ConvertButton.Background = Brushes.Yellow;
            TF_ContentStackPanel.Background = Brushes.Yellow;
            TF_ConvertButton.Foreground = Brushes.Black;

            TF_YoutubeTags.Foreground = Brushes.Black;
            TF_InstagramTags.Foreground = Brushes.Black;
            TF_TIKTOKTags.Foreground = Brushes.Black;
            TF_FacebookTags.Foreground = Brushes.Black;
            TF_TwitterTags.Foreground = Brushes.Black;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (TF_TextBox.Text == "")
            {
                MessageBox.Show("Please enter search term to convert");
                this.Close();
            }

            else
            {
                SelenuiumScrape();
                TF_ContentStackPanel.IsEnabled = true;
                InputData.input = TF_TextBox.Text;
            }
        }

        private void YoutubeTags_Checked(object sender, RoutedEventArgs e)
        {
            TF_HeadingLabel.Content = "Youtube";
            InputData.trim = InputData.input.Replace("#", "");
            InputData.trim = InputData.trim.Replace(" ", ",");
            TF_TextBox.Text = InputData.trim;
            TF_ContentStackPanel.Background = Brushes.Red;
            TF_ConvertButton.Background = Brushes.Red;
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
                    int index = YS_ListView.ItemContainerGenerator.IndexFromContainer(listViewItem);

                    //Console.WriteLine(TrendsInfo.VideoList[3]);
                    // Display the item and its index
                    //MessageBox.Show($"You double-clicked on item '{clickedItem}' at index {index}");

                    Clipboard.SetData(DataFormats.Text, (Object)clickedItem);
                }
            }
        }

        private void InstagramTags_Checked(object sender, RoutedEventArgs e)
        {
            TF_HeadingLabel.Content = "Instagram";
            TF_ContentStackPanel.Background = Brushes.MediumPurple;
            TF_ConvertButton.Background = Brushes.MediumPurple;
            TF_TextBox.Text = InputData.input;
        }

        private void TIKTOKTags_Checked(object sender, RoutedEventArgs e)
        {
            InputData.inputText1 = InputData.input.Split(' ')[1].Trim();
            InputData.inputText2 = InputData.input.Split(' ')[2].Trim();
            InputData.inputText3 = InputData.input.Split(' ')[3].Trim();

            TF_TextBox.Text = InputData.inputText1 + " " + InputData.inputText2 + " " + InputData.inputText3 + " " + "#fyp";
            TF_ContentStackPanel.Background = Brushes.HotPink;
            TF_ConvertButton.Background = Brushes.HotPink;
        }

        private void FacebookTags_Checked(object sender, RoutedEventArgs e)
        {
            TF_HeadingLabel.Content = "Facebook";

            InputData.inputText1 = InputData.input.Split(' ')[1].Trim();
            InputData.inputText2 = InputData.input.Split(' ')[2].Trim();
            InputData.inputText3 = InputData.input.Split(' ')[3].Trim();
            InputData.inputText4 = InputData.input.Split(' ')[4].Trim();
            InputData.inputText5 = InputData.input.Split(' ')[5].Trim();

            TF_TextBox.Text = InputData.inputText1 + " " + InputData.inputText2 + " " + InputData.inputText3 + " " + InputData.inputText4 + " " + InputData.inputText5;
            TF_ContentStackPanel.Background = Brushes.CornflowerBlue;
            TF_ConvertButton.Background = Brushes.CornflowerBlue;
        }

        private void TwitterTags_Checked(object sender, RoutedEventArgs e)
        {
            TF_HeadingLabel.Content = "Twitter";

            InputData.inputText1 = InputData.input.Split(' ')[1].Trim();
            InputData.inputText2 = InputData.input.Split(' ')[2].Trim();

            TF_TextBox.Text = InputData.inputText1 + " " + InputData.inputText2;

            TF_ContentStackPanel.Background = Brushes.DeepSkyBlue;
            TF_ConvertButton.Background = Brushes.DeepSkyBlue;
        }

        private void SelenuiumScrape()
        {
            Task task = YoutubeSearchForVideos();

            //uses chromedriver class from Selenuim Package to create webdriver.
            using (IWebDriver SelenuimDriver = new ChromeDriver())
            {


                //Navigates to the URL
                SelenuimDriver.Navigate().GoToUrl("https://app.sistrix.com/en/instagram-hashtags");

                //Creating webelement to handing Searchbar and button functionality, via find ID.
                IWebElement WebsiteSearchBar = SelenuimDriver.FindElement(By.Id("html-input-span-1"));

                WebsiteSearchBar.SendKeys(TF_TextBox.Text);

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
                    TF_TextBox.Text += WebsiteResult.Text;

                }

                
            }
        }

        private async Task YoutubeSearchForVideos()
        {

            var YTService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDJYPsoxlVTxws3UeQxB364NMrmrqqm2hc"
            });

            var Videorequest = YTService.Search.List("snippet");
            Videorequest.Q = TF_TextBox.Text;
            Videorequest.MaxResults = 10;

            var Searchresults = await Videorequest.ExecuteAsync();
                       
            

            if (Searchresults.Items.Count <= 0)
            {
                Console.WriteLine("No videos found");
                this.Close();
            }

            foreach (var YTVideos in Searchresults.Items)
            {

                YTSearchInfo.VideoList.Add(YTSearchInfo._Relatedtopics = String.Format(YTVideos.Snippet.Title));

            }

            YS_ListView.ItemsSource = YTSearchInfo.VideoList;
     
        }
    }
}