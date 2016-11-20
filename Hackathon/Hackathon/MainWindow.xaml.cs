using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hackathon.Core;

namespace Hackathon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Tile> FeaturedTiles;
        List<Tile> LaughTiles;
        List<Tile> SmileTiles;
        List<Tile> AwwTiles;
        List<Tile> HungryTiles;
        bool isSignedIn = false;
        SignInHelper signInHelper;

        public MainWindow()
        {
            InitializeComponent();
            LoadTiles(FeaturedTiles);
            BuildTiles();
        }

        public void BuildTiles()
        {
            var row = 0;
            var column = 0;
            var tileNum = 0;

            var likeButtonImage = new BitmapImage(new Uri("heart_icon_white.png", UriKind.RelativeOrAbsolute));

            foreach (var tile in Tiles)
            {
                var stack = new StackPanel();
                var image = new Image();
                var likeButton = new Image();

                likeButton.Source = likeButtonImage;
                likeButton.Height = 50;
                likeButton.MouseLeftButtonDown += onLikeButtonClick;
                image.Source = tile.Image;

                stack.Margin = new Thickness(50);
                stack.VerticalAlignment = VerticalAlignment.Center;
                stack.HorizontalAlignment = HorizontalAlignment.Center;
                stack.Background = new SolidColorBrush(Colors.DodgerBlue);

                stack.Children.Add(image);
                stack.Children.Add(likeButton);
                stack.Name = $"tile{tileNum++}";

                mainGrid.Children.Add(stack);
                Grid.SetRow(stack, row);
                Grid.SetColumn(stack, column);

                if (column == 0)
                {
                    column = 1;
                }
                else
                {
                    if (row > 4)
                    {
                        break;
                    }

                    column = 0;
                    row++;
                }
            }
        }

        private void onLoginButtonClick(object sender, MouseButtonEventArgs e)
        {
            if(isSignedIn)
            {
                System.Windows.MessageBox.Show("You have successfully logged out.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                isSignedIn = false;
                loginButton.Content = "Sign In";
                return;
            }

            CollapseAll();
            loginPanel.Visibility = Visibility.Visible;
        }

        private void onConfirmLoginButtonClick(object sender, EventArgs e)
        {
            // Authenticate
            signInHelper = new SignInHelper(usernameBox.Text, passwordBox.Password);
            if (!signInHelper.isValidEntry())
            {
                usernameBox.Clear();
                passwordBox.Clear();
                usernameBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                passwordBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                return;
            }

            if (signInHelper.isValidateUsername())
            {
                if (signInHelper.isValidPassword())
                {
                    CollapseAll();
                    usernameBox.Clear();
                    passwordBox.Clear();

                    isSignedIn = true;
                    loginButton.Content = "Sign Out";

                    mainScroll.Visibility = Visibility.Visible;
                }

                else
                {
                    passwordBox.Clear();
                    passwordBox.Background = new SolidColorBrush(Color.FromRgb(240,152,152));
                    return;
                }
            }

            else
            {
                usernameBox.Clear();
                passwordBox.Clear();
                usernameBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                return;
            }
        }

        private void onLabelMouseOver(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.Foreground = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void onLabelMouseLeaveGrey(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 178, 178, 178));
        }
        private void onLabelMouseLeaveBlack(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            label.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void CollapseAll()
        {
            loginPanel.Visibility = Visibility.Collapsed;
            mainScroll.Visibility = Visibility.Collapsed;
            createPanel.Visibility = Visibility.Collapsed;
        }

        private void onLikeButtonClick(object sender, MouseButtonEventArgs e)
        {
            var button = (Image)sender;
            var tile = (StackPanel)button.Parent;
            var tileNum = -1;

            tileNum = int.Parse(tile.Name.Substring(4));

            if (Tiles[tileNum].isLiked)
            {
                button.Source = new BitmapImage(new Uri("heart_icon_white.png", UriKind.Relative));
                Tiles[tileNum].isLiked = false;
            }
            else
            {
                button.Source = new BitmapImage(new Uri("heart_icon.png", UriKind.Relative));
                Tiles[tileNum].isLiked = true;
            }
        }

        private void onCreateAccountClick(object sender, MouseButtonEventArgs e)
        {
            CollapseAll();
            createPanel.Visibility = Visibility.Visible;
        }

        private void onCreateNewAccount(object sender, MouseButtonEventArgs e)
        {
            //dayton and brianna do this
        }

        private void onPasswordEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                onConfirmLoginButtonClick(sender, EventArgs.Empty);
            }
        }

        private void onLaughClick(object sender, MouseButtonEventArgs e)
        {
            CollapseAll();
        }
    }
}
