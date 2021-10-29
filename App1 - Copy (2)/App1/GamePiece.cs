using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace App1.Assets
{
    public class GamePiece
    {
        public GamePiece()
        {
            Element = new Image();
            Element.Stretch = Stretch.Fill;
            Height = 50;
            Width = 80;
            Init("");
        }
        public string imageUri = "ms-appx:///Assets/right1.gif";
        public Image Element { get; set; }
        public double X
        {
            get
            {
                return Canvas.GetLeft(Element);
            }
            set
            {
                Canvas.SetLeft(Element, value);
            }
        }
        public double Y
        {
            get
            {
                return Canvas.GetTop(Element);
            }
            set
            {
                Canvas.SetTop(Element, value);
            }

        }
        public double Height
        {
            get
            {
                return Element.ActualHeight;
            }
            set
            {
                Element.Height = value;
            }
        }
        public double Width{ get { return Element.ActualWidth; } set { Element.Width = value; }}
        public virtual void Init(string move)
        {
            Element.Source = new BitmapImage(new Uri(imageUri));
        }
    }
}
