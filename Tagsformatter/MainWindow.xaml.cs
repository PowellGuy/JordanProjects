using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Net.Http;
using System.Collections.Generic;



namespace Tagsformatter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //bool IsTagsSelected = false;
        string input, trim, inputText1, inputText2, inputText3, inputText4, inputText5, URL = "https://best-hashtags.com/hashtag/deadrising/";



        public MainWindow()
        {
            InitializeComponent();
            BeginScrape();
            ContentType.IsEnabled = false;
            ContentType.Background = Brushes.Yellow;
            ConvertButton.Background = Brushes.Yellow;

        }

        private void ThemeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ContentStack.Background = Brushes.SlateGray;
            MainLayout.Background = Brushes.SlateGray;
            HeadingLabel.Background = Brushes.SlateGray;
            MainTextBox.Background = Brushes.SlateGray;

            HeadingLabel.Foreground = Brushes.White;
            TitleLabel.Foreground = Brushes.White;
            MainTextBox.Foreground = Brushes.White;

            MainTextBox.CaretBrush = Brushes.White;
            ThemeCheckbox.Foreground = Brushes.White;

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

        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            int Hashtagcounter;
            string ConvertToString;

            if (MainTextBox.Text == "" || MainTextBox.Text.Contains("#") != true)
            {
                MessageBox.Show("No Hashtag to convert, exiting program");
                this.Close();
            }

            if (MainTextBox.Text.Contains("#"))
            {
                Hashtagcounter = MainTextBox.Text.Count(x => x == '#');
                ConvertToString = Hashtagcounter.ToString();

                if (Hashtagcounter > 5)
                {
                    input = MainTextBox.Text;
                    ContentType.IsEnabled = true;
                    ContentType.Background = Brushes.Yellow;
                    ConvertButton.Background = Brushes.Yellow;

                }

                else
                {
                    MessageBox.Show("Please convert at least 5 hashtags");
                    this.Close();
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
                //IsTagsSelected = true;
                HeadingLabel.Content = "Youtube";

                //input = MainTextBox.Text;
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
                //IsTagsSelected = true;
                HeadingLabel.Content = "Tiktok";

                //input = MainTextBox.Text;
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
                //IsTagsSelected = true;
                HeadingLabel.Content = "Facebook";

                //input = MainTextBox.Text;
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
                //IsTagsSelected = true;
                HeadingLabel.Content = "Twitter";

                // input = MainTextBox.Text;
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

        //class for attempting to read url and add to a variable.
        private async Task BeginScrape()
        {
            ConnectToURL();

            //Creates variable to handle Url and uses the HttpClient Class request connection to site

            var HttpScrapeClient = new HttpClient();
            var HtmlString = await HttpScrapeClient.GetStringAsync(URL);

            //uses the HtmlDocument to handle the data from the chosen URL.
            var HttpDocHandler = new HtmlDocument();
            HttpDocHandler.LoadHtml(HtmlString);

            //uses the HTMLDocument class to collect the specific tags/keywords and parses them into a list.
            var DivHandler = HttpDocHandler.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("tag-box")).ToList();

            //for each loop created to cycle through all the divs in the HTML page.
            foreach (var div in DivHandler)
            {
                Console.WriteLine(div.InnerText.Trim());
                Console.WriteLine("Hashtags scraped");
                break;
            }
        }

        private async Task ConnectToURL()
        {
            Console.WriteLine("beginning connection to site: " + URL);


            var HttpConnectClient = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "thing1", "hello" },
                { "thing2", "world" }
  };

            var Connectioncontent = new FormUrlEncodedContent(values);

            var response = await HttpConnectClient.PostAsync(URL, Connectioncontent);

            var ConnectionResponseString = await response.Content.ReadAsStringAsync();

            var responseString = await HttpConnectClient.GetStringAsync(URL);

        }








    }
}
