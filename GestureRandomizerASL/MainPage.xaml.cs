using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ASLGestureRandomizer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<BitmapImage> imagesASL;


        public MainPage()
        {
            this.InitializeComponent();
            ImageGenerator();
        }

        private async Task<int> getTotalNumberOfImages()
        {
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");
            StorageFolder images = await assets.GetFolderAsync("Images");

            var files = await images.GetFilesAsync();
            return files.Count;
        }
        private List<int> generateRandom(int gestureListSize)
        {
            Random rnd = new Random();
            List<int> randomNumbers = Enumerable.Range(1, gestureListSize).OrderBy(x => rnd.Next()).ToList();

            return randomNumbers;
        }

        private async void ImageGenerator()
        {
            imagesASL = new List<BitmapImage>();

            Task<int> getTotalImagesTask = getTotalNumberOfImages();
            int totalNumberOfImages = await getTotalImagesTask;

            List<int> gestureList = generateRandom(totalNumberOfImages);

            foreach (int number in gestureList)
            {
                imagesASL.Add(new BitmapImage(new Uri("ms-appx:///Assets/Images/" + number + ".png")));
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(imagesASL.Count == 0)
            {
                
                textBox.Text = "End of Experiment";
            }
            else
            {
                imgASL.Source = imagesASL.FirstOrDefault();
                imagesASL.RemoveAt(0);
                textBox.Text = "";
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ImageGenerator();
        }
    }
}
