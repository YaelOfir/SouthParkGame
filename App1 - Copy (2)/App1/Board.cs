using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
namespace App1.Assets
{
    public class Board
    {
        private Canvas cnvs1;
        private MainPage mainpage;
        private Player player;
        private Target target;
        private List<Enemy> enemies;
        public DispatcherTimer Timer = new DispatcherTimer();
        public int Score = 0;
        public int Wave = 1;
        public int Limit = 2;
        public int EnemyNum = 2;
        public DateTimeOffset Time;
        public TimeSpan TimeActive;
        public DateTimeOffset LastTime;
        public float FElapsedTime = 0;
        public double Step = 10;
        public bool moveUp;
        public bool moveDown;
        public bool moveRight;
        public bool moveLeft;
        public Board(Canvas cnvs, MainPage mainPage)
        {
            mainpage = mainPage;
            cnvs1 = cnvs;
            player = new Player();
            target = new Target();
            enemies = new List<Enemy>();
            LastTime = DateTimeOffset.Now;
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            Timer.Tick += GameTime;
        }
        ///Game Objects///
        public void PlayerCreated()
        {
            player.X = cnvs1.Width / 2;
            player.Y = cnvs1.Height / 2;
            cnvs1.Children.Add(player.Element);
        }
        public void EnemyCreated(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Enemy e = new Enemy();
                e.X = i * 8;
                e.Y = i * 100;
                enemies.Add(e);
                cnvs1.Children.Add(e.Element);
            }
        }
        public void TargetCreated()
        {
            cnvs1.Children.Add(target.Element);
            target.X = cnvs1.ActualWidth / 4;
            target.Y = cnvs1.ActualHeight / 4;
        }
        private bool PlayerCatched(GamePiece enemy)
        {
            if (Math.Abs(enemy.X - player.X) < player.Width / 2 && Math.Abs(enemy.Y - player.Y) < player.Height / 2)
            {
                ResetGame();
                cnvs1.Visibility = Visibility.Collapsed;
                mainpage.endGrid.Visibility = Visibility.Visible;
                return true;
            }
            return false;
        }
        public void TargetCatched(GamePiece target)
        {
            if (Math.Abs(target.X - player.X) < player.Width / 2 && Math.Abs(target.Y - player.Y) < player.Height / 2)
            {
                target.X = new Random().Next(1000);
                target.Y = new Random().Next(600);
                Score++;
                if (Score == Limit)
                {
                    Score = 0;
                    Wave++;
                    Limit += 1;
                    EnemyNum += 1;
                    EnemyCreated(EnemyNum);
                }
                if (Wave == 3)
                {
                    Score = 0;
                    ResetGame();
                    cnvs1.Visibility = Visibility.Collapsed;
                    mainpage.happyEndGrid.Visibility = Visibility.Visible;
                }
            }
        }
        ///Game Functions///
        public void StartGame()
        {
            ResetGame();
            Timer.Start();
            EnemyCreated(EnemyNum);
            PlayerCreated();
            TargetCreated();
        }
        private void ResetGame()
        {
            Timer.Stop();
            Score = 0;
            Limit = 2;
            Wave = 1;
            cnvs1.Children.Clear();
            enemies.Clear();
            cnvs1.Children.Remove(player.Element);
            EnemyNum = 2;
        }
        ///Game Movment///
        public void KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.Left:
                    moveLeft = true;
                    break;
                case VirtualKey.Up:
                    moveUp = true;
                    break;
                case VirtualKey.Right:
                    moveRight = true;
                    break;
                case VirtualKey.Down:
                    moveDown = true;
                    break;
                case VirtualKey.P:
                    if (Timer.IsEnabled == true)
                        Timer.Stop();
                    else if (Timer.IsEnabled == false)
                        Timer.Start();
                    break;
            }
        }
        public void KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.Left:
                    moveLeft = false;
                    player.imageUri = $@"ms-appx:///Assets/left1.gif";
                    break;
                case VirtualKey.Up:
                    moveUp = false;
                    player.imageUri = $@"ms-appx:///Assets/right1.gif";
                    break;
                case VirtualKey.Right:
                    moveRight = false;
                    player.imageUri = $@"ms-appx:///Assets/right1.gif";
                    break;
                case VirtualKey.Down:
                    moveDown = false;
                    player.imageUri = $@"ms-appx:///Assets/right1.gif";
                    break;
            }
        }
        private void GameTime(object sender, object t)
        {
            Time = DateTimeOffset.Now;
            TimeActive = Time - LastTime;
            LastTime = Time;
            FElapsedTime = (float)TimeActive.TotalSeconds;
            TargetCatched(target);
            foreach (var enemy in enemies)
            {
                enemy.Move(player);
                if (PlayerCatched(enemy))
                    break;
            }
            if (moveDown == true)
            {
                player.Init("moveDown", "down");
            }
            if (moveUp == true)
            {
                player.Init("moveUp", "up");
            }
            if (moveRight == true)
            {
                player.Init("moveRight", "right");
            }
            if (moveLeft == true)
            {
                player.Init("moveleft", "left");
            }
            else
            {
                player.Init("", "");
            }
            MovePlayer(FElapsedTime);
        }
        public void MovePlayer(float FElapsedTime)
        {
            if (moveLeft && player.X > 0) player.X -= Step;
            if (moveUp && player.Y > 0) player.Y -= Step;
            if (moveRight && player.X + player.Width < cnvs1.ActualWidth) player.X += Step;
            if (moveDown && player.Y + player.Height < cnvs1.ActualHeight) player.Y += Step;
        }
    }
}

