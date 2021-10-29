using Windows.UI.Xaml.Controls;
namespace App1.Assets
{
    class GameLogic
    {
        public Board Board;
        public GameLogic(Canvas cnvs, MainPage mainPage)
        {
            Board = new Board(cnvs, mainPage);
        }
    }
}
