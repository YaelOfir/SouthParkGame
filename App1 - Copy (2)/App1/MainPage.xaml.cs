using App1.Assets;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        GameLogic Logic;
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            Logic = new GameLogic(cnvs, this);
        }
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            cnvs.Height = 650;
            cnvs.Width = 1280;
        }
        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            Logic.Board.KeyUp(sender, args);
        }
        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            Logic.Board.KeyDown(sender, args);
        }
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            startPage.Visibility = Visibility.Collapsed;
            transitionPage.Visibility = Visibility.Visible;
        }
        private void continueBtn_Click(object sender, RoutedEventArgs e)
        {
            transitionPage.Visibility = Visibility.Collapsed;
            cnvs.Visibility = Visibility.Visible;
            Logic.Board.StartGame();
        }
        private void instructionBtn_Click(object sender, RoutedEventArgs e)
        {
            instractionPage.Visibility = Visibility.Visible;
        }
        private void transitionBackBtn_Click(object sender, RoutedEventArgs e)
        {
            transitionPage.Visibility = Visibility.Collapsed;
            startPage.Visibility = Visibility.Visible;
        }
        private void instructionBackBtn_Click(object sender, RoutedEventArgs e)
        {
            instractionPage.Visibility = Visibility.Collapsed;
            transitionPage.Visibility = Visibility.Visible;
        }
        private void startOverBtn_Click(object sender, RoutedEventArgs e)
        {
            endGrid.Visibility = Visibility.Collapsed;
            cnvs.Visibility = Visibility.Visible;
            Logic.Board.StartGame();
        }
        private void startOverBtn2_Click(object sender, RoutedEventArgs e)
        {
            happyEndGrid.Visibility = Visibility.Collapsed;
            cnvs.Visibility = Visibility.Visible;
            Logic.Board.StartGame();
        }
    }
}

