using System;
namespace App1.Assets
{
    public class Enemy : GamePiece
    {
        private int step;
        public override void Init(string move)
        {
            imageUri = @"ms-appx:///Assets/cartmen.gif";
            step = new Random().Next(3, 6);
            base.Init(imageUri);
        }
        public void Move(GamePiece player)
        {
            if (X > player.X)
            {
                X -= step;
            }
            else if (Math.Abs(X - player.X) < step) { }
            else
            {
                X += step;
            }
            if (Y > player.Y)
            {
                Y -= step;
            }
            else if (Math.Abs(Y - player.Y) < step) { }
            else
            {
                Y += step;
            }
        }
    }
}
